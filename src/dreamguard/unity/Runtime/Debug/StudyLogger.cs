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
        private static string _participantId;
        private static string _condition;
        private static bool _active;

        public static bool IsActive => _active;

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

        /// <summary>Writes SESSION_END and closes the CSV file.</summary>
        public static void EndSession()
        {
            if (!_active) return;

            Log("SESSION_END");
            _active = false;

            try { _csv?.Close(); }
            catch (Exception e) { DreamGuardLog.LogError($"[StudyLogger] Close failed: {e.Message}"); }
            finally { _csv = null; }

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

        /// <summary>RFC-4180 CSV escaping: wrap in quotes if the value contains a comma, quote, or newline.</summary>
        private static string CsvEscape(string value)
        {
            if (value == null) return "";
            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0) return value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
