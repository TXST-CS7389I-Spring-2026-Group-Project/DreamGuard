using System;
using System.IO;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Structured CSV logger for user-study data collection.
    ///
    /// Each session creates a new numbered folder under Application.persistentDataPath:
    ///   experiments/study_1/study.csv
    ///   experiments/study_2/study.csv
    ///   ...
    ///
    /// CSV columns: timestamp_iso, event_type, condition, participant_id, detail
    ///
    /// Pull after a session:
    ///   adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/experiments
    ///
    /// Usage:
    ///   StudyLogger.BeginSession("P01", "boundary");
    ///   StudyLogger.Log("TRIGGER", "shield_activated");
    ///   StudyLogger.LogIntrusionMark();
    ///   StudyLogger.EndSession();
    /// </summary>
    public static class StudyLogger
    {
        private static StreamWriter _csv;
        private static StreamWriter _positionCsv;
        private static StreamWriter _rControllerCsv;
        private static StreamWriter _lControllerCsv;
        private static StreamWriter _headsetCsv;
        private static StreamWriter _collectionCsv;
        private static StreamWriter _roomsCsv;

        private static string _participantId;
        private static string _condition;
        private static bool _active;

        // Last-written tracking values — NaN sentinel forces a write on the first frame.
        private static Vector3    _lastPlayerPos = NaNVec3;
        private static Vector3    _lastRCtrlPos  = NaNVec3;
        private static Quaternion _lastRCtrlRot  = NaNQuat;
        private static Vector3    _lastLCtrlPos  = NaNVec3;
        private static Quaternion _lastLCtrlRot  = NaNQuat;
        private static Vector3    _lastHeadPos   = NaNVec3;
        private static Quaternion _lastHeadRot   = NaNQuat;

        private const float kEpsilon = 0.0001f;

        private static Vector3    NaNVec3 => new Vector3(float.NaN, float.NaN, float.NaN);
        private static Quaternion NaNQuat => new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);

        public static bool IsActive => _active;

        /// <summary>
        /// Registers Application.quitting so SESSION_END is always written even if
        /// EndSession() is never called explicitly (e.g. app force-quit or crash after
        /// the OS has already flushed the AutoFlush writers).
        /// </summary>
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RegisterQuitHandler()
        {
            Application.quitting -= EndSession;   // guard against double-registration on domain reload
            Application.quitting += EndSession;
        }

        /// <summary>
        /// Opens a new numbered study folder and writes the CSV header.
        /// Safe to call multiple times — ends any previous session first.
        /// </summary>
        public static void BeginSession(string participantId, string condition)
        {
            if (_active) EndSession();

            _participantId = participantId ?? "";
            _condition     = condition     ?? "";

            try
            {
                string experimentsDir = Path.Combine(Application.persistentDataPath, "experiments");
                int    n              = FindNextStudyNumber(experimentsDir);
                string sessionDir     = Path.Combine(experimentsDir, $"study_{n}");
                Directory.CreateDirectory(sessionDir);

                string csvPath = Path.Combine(sessionDir, "study.csv");
                _csv = new StreamWriter(csvPath, append: false) { AutoFlush = true };
                _csv.WriteLine("timestamp_iso,event_type,condition,participant_id,detail");

                _positionCsv    = OpenTrackingCsv(sessionDir, "position.csv",     "timestamp_iso,x,y,z");
                _rControllerCsv = OpenTrackingCsv(sessionDir, "r_controller.csv", "timestamp_iso,pos_x,pos_y,pos_z,rot_x,rot_y,rot_z,rot_w");
                _lControllerCsv = OpenTrackingCsv(sessionDir, "l_controller.csv", "timestamp_iso,pos_x,pos_y,pos_z,rot_x,rot_y,rot_z,rot_w");
                _headsetCsv     = OpenTrackingCsv(sessionDir, "headset.csv",      "timestamp_iso,pos_x,pos_y,pos_z,rot_x,rot_y,rot_z,rot_w");
                _collectionCsv  = OpenTrackingCsv(sessionDir, "collection.csv",   "timestamp_iso,orb_id");
                _roomsCsv       = OpenTrackingCsv(sessionDir, "rooms.csv",         "timestamp_iso,event,room_id");

                _lastPlayerPos = NaNVec3;
                _lastRCtrlPos  = NaNVec3; _lastRCtrlRot = NaNQuat;
                _lastLCtrlPos  = NaNVec3; _lastLCtrlRot = NaNQuat;
                _lastHeadPos   = NaNVec3; _lastHeadRot  = NaNQuat;

                _active = true;

                DreamGuardLog.Log($"[StudyLogger] Session {n} started — participant={_participantId} condition={_condition} path={csvPath}");
                Log("SESSION_START", $"unity={Application.unityVersion} device={SystemInfo.deviceModel}");
            }
            catch (Exception e)
            {
                DreamGuardLog.LogError($"[StudyLogger] BeginSession failed: {e.Message}");
            }
        }

        /// <summary>Writes a CSV row with the current timestamp, event_type, and optional detail.</summary>
        public static void Log(string eventType, string detail = "")
        {
            if (!_active)
            {
                DreamGuardLog.LogWarning($"[StudyLogger] Log called before BeginSession — event={eventType}");
                return;
            }

            try
            {
                string ts  = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                string row = $"{CsvEscape(ts)},{CsvEscape(eventType)},{CsvEscape(_condition)},{CsvEscape(_participantId)},{CsvEscape(detail)}";
                _csv.WriteLine(row);
            }
            catch (Exception e)
            {
                DreamGuardLog.LogError($"[StudyLogger] Write failed: {e.Message}");
            }
        }

        /// <summary>
        /// Logs an INTRUSION_MARK event — call this the moment the confederate
        /// crosses into the participant's space. Trigger latency =
        /// TRIGGER.timestamp − INTRUSION_MARK.timestamp.
        /// </summary>
        public static void LogIntrusionMark(string detail = "") => Log("INTRUSION_MARK", detail);

        /// <summary>
        /// Logs a TRIGGER event — call this when a passthrough technique first fires
        /// in response to an intrusion or detection.  One event per detection
        /// episode (not per frame); callers are responsible for debouncing.
        /// detail: "technique=X label=Y conf=Z" or similar.
        /// </summary>
        public static void LogTrigger(string technique, string detail = "") =>
            Log("TRIGGER", string.IsNullOrEmpty(detail) ? $"technique={technique}" : $"technique={technique} {detail}");

        /// <summary>
        /// Logs a TECHNIQUE_CHANGE event — call when the experimenter switches
        /// the active passthrough condition via the menu.
        /// </summary>
        public static void LogTechniqueChange(string technique) =>
            Log("TECHNIQUE_CHANGE", $"technique={technique}");

        /// <summary>
        /// Logs an orb collection event to collection.csv.
        /// </summary>
        public static void LogOrbCollected(string orbId)
        {
            if (!_active) return;
            _collectionCsv?.WriteLine($"{Ts()},{CsvEscape(orbId)}");
        }

        // ── room / condition events ──────────────────────────────────────────────

        /// <summary>
        /// Logs a ROOM_ENTER event to study.csv and an ENTER row to rooms.csv.
        /// Call when the participant enters a room (or at session start for Room 1).
        /// </summary>
        public static void LogRoomEnter(string roomId)
        {
            _roomsCsv?.WriteLine($"{Ts()},ENTER,{CsvEscape(roomId)}");
            Log("ROOM_ENTER", $"room_id={roomId}");
        }

        /// <summary>
        /// Logs a ROOM_HALFWAY event to study.csv — fired after the 2nd orb in a 4-orb room.
        /// This is the ground-truth timestamp for trigger-latency analysis:
        ///   latency = TRIGGER.timestamp − ROOM_HALFWAY.timestamp.
        /// </summary>
        public static void LogRoomHalfway(string roomId, int orbsCollected, int totalOrbs) =>
            Log("ROOM_HALFWAY", $"room_id={roomId} orbs_collected={orbsCollected} total_orbs={totalOrbs}");

        /// <summary>
        /// Logs a ROOM_COMPLETE event to study.csv and an EXIT row to rooms.csv.
        /// Call when the participant collects all orbs and the exit door opens.
        /// room_duration = EXIT.timestamp − ENTER.timestamp in rooms.csv.
        /// </summary>
        public static void LogRoomComplete(string roomId)
        {
            _roomsCsv?.WriteLine($"{Ts()},EXIT,{CsvEscape(roomId)}");
            Log("ROOM_COMPLETE", $"room_id={roomId}");
        }

        /// <summary>
        /// Logs a CONDITION_BLOCK_START event — pairs a condition name with the room in the log.
        /// Call immediately after ROOM_ENTER.
        /// </summary>
        public static void LogConditionBlockStart(string roomId, string condition) =>
            Log("CONDITION_BLOCK_START", $"room_id={roomId} condition={condition}");

        /// <summary>
        /// Logs a FALSE_POSITIVE event — call when a passthrough technique fires with no
        /// confederate present. Important for trust/disruption analysis.
        /// </summary>
        public static void LogFalsePositive(string technique, string detail = "") =>
            Log("FALSE_POSITIVE", string.IsNullOrEmpty(detail) ? $"technique={technique}" : $"technique={technique} {detail}");

        /// <summary>
        /// Logs a CONFEDERATE_EXIT event — call when the confederate leaves the participant's
        /// space after an intrusion. Bounds the intrusion episode duration.
        /// </summary>
        public static void LogConfederateExit(string roomId, string detail = "") =>
            Log("CONFEDERATE_EXIT", string.IsNullOrEmpty(detail) ? $"room_id={roomId}" : $"room_id={roomId} {detail}");

        // ── per-frame tracking ───────────────────────────────────────────────────

        /// <summary>
        /// Logs the player's world-space position to position.csv.
        /// Skips the write if the position has not changed beyond kEpsilon.
        /// </summary>
        public static void LogPlayerPosition(Vector3 pos)
        {
            if (!_active) return;
            if (Vec3Close(pos, _lastPlayerPos)) return;
            _lastPlayerPos = pos;
            _positionCsv?.WriteLine($"{Ts()},{pos.x:F4},{pos.y:F4},{pos.z:F4}");
        }

        /// <summary>
        /// Logs the right controller's local position and orientation to r_controller.csv.
        /// Skips the write if neither value has changed beyond kEpsilon.
        /// </summary>
        public static void LogRightController(Vector3 pos, Quaternion rot)
        {
            if (!_active) return;
            if (Vec3Close(pos, _lastRCtrlPos) && QuatClose(rot, _lastRCtrlRot)) return;
            _lastRCtrlPos = pos; _lastRCtrlRot = rot;
            _rControllerCsv?.WriteLine($"{Ts()},{pos.x:F4},{pos.y:F4},{pos.z:F4},{rot.x:F4},{rot.y:F4},{rot.z:F4},{rot.w:F4}");
        }

        /// <summary>
        /// Logs the left controller's local position and orientation to l_controller.csv.
        /// Skips the write if neither value has changed beyond kEpsilon.
        /// </summary>
        public static void LogLeftController(Vector3 pos, Quaternion rot)
        {
            if (!_active) return;
            if (Vec3Close(pos, _lastLCtrlPos) && QuatClose(rot, _lastLCtrlRot)) return;
            _lastLCtrlPos = pos; _lastLCtrlRot = rot;
            _lControllerCsv?.WriteLine($"{Ts()},{pos.x:F4},{pos.y:F4},{pos.z:F4},{rot.x:F4},{rot.y:F4},{rot.z:F4},{rot.w:F4}");
        }

        /// <summary>
        /// Logs the headset (camera) world-space position and orientation to headset.csv.
        /// Skips the write if neither value has changed beyond kEpsilon.
        /// </summary>
        public static void LogHeadset(Vector3 pos, Quaternion rot)
        {
            if (!_active) return;
            if (Vec3Close(pos, _lastHeadPos) && QuatClose(rot, _lastHeadRot)) return;
            _lastHeadPos = pos; _lastHeadRot = rot;
            _headsetCsv?.WriteLine($"{Ts()},{pos.x:F4},{pos.y:F4},{pos.z:F4},{rot.x:F4},{rot.y:F4},{rot.z:F4},{rot.w:F4}");
        }

        /// <summary>Writes SESSION_END and closes the CSV file.</summary>
        public static void EndSession()
        {
            if (!_active) return;

            Log("SESSION_END");
            _active = false;

            try { _csv?.Close(); }            catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close study.csv failed: {e.Message}"); }
            try { _positionCsv?.Close(); }    catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close position.csv failed: {e.Message}"); }
            try { _rControllerCsv?.Close(); } catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close r_controller.csv failed: {e.Message}"); }
            try { _lControllerCsv?.Close(); } catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close l_controller.csv failed: {e.Message}"); }
            try { _headsetCsv?.Close(); }     catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close headset.csv failed: {e.Message}"); }
            try { _collectionCsv?.Close(); }  catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close collection.csv failed: {e.Message}"); }
            try { _roomsCsv?.Close(); }       catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close rooms.csv failed: {e.Message}"); }
            _csv = _positionCsv = _rControllerCsv = _lControllerCsv = _headsetCsv = _collectionCsv = _roomsCsv = null;

            DreamGuardLog.Log("[StudyLogger] Session ended.");
        }

        // ── helpers ─────────────────────────────────────────────────────────────

        private static int FindNextStudyNumber(string experimentsDir)
        {
            int max = 0;

            if (Directory.Exists(experimentsDir))
            {
                foreach (string dir in Directory.GetDirectories(experimentsDir, "study_*"))
                {
                    string name = Path.GetFileName(dir);
                    if (name.Length > 6 && int.TryParse(name.Substring(6), out int n))
                        max = Math.Max(max, n);
                }
            }

            return max + 1;
        }

        private static string Ts() => DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");

        private static StreamWriter OpenTrackingCsv(string dir, string filename, string header)
        {
            string path   = Path.Combine(dir, filename);
            var    writer = new StreamWriter(path, append: false) { AutoFlush = true };
            writer.WriteLine(header);
            return writer;
        }

        private static bool Vec3Close(Vector3 a, Vector3 b) =>
            Mathf.Abs(a.x - b.x) < kEpsilon &&
            Mathf.Abs(a.y - b.y) < kEpsilon &&
            Mathf.Abs(a.z - b.z) < kEpsilon;

        private static bool QuatClose(Quaternion a, Quaternion b) =>
            Mathf.Abs(a.x - b.x) < kEpsilon &&
            Mathf.Abs(a.y - b.y) < kEpsilon &&
            Mathf.Abs(a.z - b.z) < kEpsilon &&
            Mathf.Abs(a.w - b.w) < kEpsilon;

        /// <summary>RFC-4180 CSV escaping: wrap in quotes if the value contains a comma, quote, or newline.</summary>
        private static string CsvEscape(string value)
        {
            if (value == null) return "";
            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0) return value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
