using System;
using System.IO;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// File-backed logger for on-device debugging.
    ///
    /// Writes to Application.persistentDataPath/dreamguard.log so logs survive
    /// after logcat scrolls them off.  Each app launch appends a new session
    /// header; the file is not cleared between sessions so you can compare runs.
    ///
    /// Pull the log after a test:
    ///   adb shell run-as com.DefaultCompany.DreamGuard cat files/dreamguard.log
    ///   adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/dreamguard.log
    /// </summary>
    public static class DreamGuardLog
    {
        private static StreamWriter _writer;
        private static bool _initialized;

        private static void EnsureInit()
        {
            if (_initialized) return;
            _initialized = true;

            try
            {
                string path = Path.Combine(Application.persistentDataPath, "dreamguard.log");
                _writer = new StreamWriter(path, append: false) { AutoFlush = true };
                _writer.WriteLine();
                _writer.WriteLine($"══ Session {DateTime.Now:yyyy-MM-dd HH:mm:ss} ══════════════════════════════");
                _writer.WriteLine($"  Device:  {SystemInfo.deviceModel}");
                _writer.WriteLine($"  Unity:   {Application.unityVersion}");
                _writer.WriteLine($"  App:     {Application.identifier} v{Application.version}");
                _writer.WriteLine();

                Debug.Log($"[DreamGuardLog] Logging to: {path}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[DreamGuardLog] Could not open log file: {e.Message}");
            }
        }

        public static void Log(string message)
        {
            EnsureInit();
            string line = $"{Ts()} I  {message}";
            Debug.Log(message);
            _writer?.WriteLine(line);
        }

        public static void LogWarning(string message)
        {
            EnsureInit();
            string line = $"{Ts()} W  {message}";
            Debug.LogWarning(message);
            _writer?.WriteLine(line);
        }

        public static void LogError(string message)
        {
            EnsureInit();
            string line = $"{Ts()} E  {message}";
            Debug.LogError(message);
            _writer?.WriteLine(line);
        }

        private static string Ts() => DateTime.Now.ToString("HH:mm:ss.fff");
    }
}
