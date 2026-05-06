using System;
using System.IO;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Captures every Unity log message (the full logcat stream) and writes it
    /// to Application.persistentDataPath/logcat.log so the complete log history
    /// survives after the logcat buffer rolls over.
    ///
    /// Call LogcatLog.Initialize() once at startup (e.g. from DreamGuardBootstrap).
    ///
    /// Pull the log after a test:
    ///   adb shell "cat /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/logcat.log"
    ///   adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/logcat.log
    /// </summary>
    public static class LogcatLog
    {
        private static StreamWriter _writer;
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            try
            {
                string path = Path.Combine(Application.persistentDataPath, "logcat.log");
                _writer = new StreamWriter(path, append: false) { AutoFlush = true };
                _writer.WriteLine();
                _writer.WriteLine($"══ Session {DateTime.Now:yyyy-MM-dd HH:mm:ss} ══════════════════════════════");
                _writer.WriteLine($"  Device:  {SystemInfo.deviceModel}");
                _writer.WriteLine($"  Unity:   {Application.unityVersion}");
                _writer.WriteLine($"  App:     {Application.identifier} v{Application.version}");
                _writer.WriteLine();

                Application.logMessageReceived += OnLogMessage;

                Debug.Log($"[LogcatLog] Capturing all Unity logs to: {path}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[LogcatLog] Could not open log file: {e.Message}");
            }
        }

        private static void OnLogMessage(string condition, string stackTrace, LogType type)
        {
            string level = type switch
            {
                LogType.Warning   => "W",
                LogType.Error     => "E",
                LogType.Exception => "E",
                LogType.Assert    => "E",
                _                 => "I"
            };

            _writer?.WriteLine($"{Ts()} {level}  {condition}");

            if ((type == LogType.Error || type == LogType.Exception) &&
                !string.IsNullOrEmpty(stackTrace))
            {
                foreach (string line in stackTrace.Split('\n'))
                {
                    string trimmed = line.TrimEnd();
                    if (trimmed.Length > 0)
                        _writer?.WriteLine($"             {trimmed}");
                }
            }
        }

        private static string Ts() => DateTime.Now.ToString("HH:mm:ss.fff");
    }
}
