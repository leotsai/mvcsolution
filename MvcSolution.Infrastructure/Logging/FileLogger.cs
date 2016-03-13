using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace MvcSolution
{
    public class FileLogger
    {
        private readonly Timer _timer;
        private readonly Dictionary<string, string> _logs;
        private bool _busy = false;

        public FileLogger()
        {
            _timer = new Timer(60000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
            _logs = new Dictionary<string, string>();
        }

        public void Log(string group, string message)
        {
            lock (this)
            {
                if (_logs.ContainsKey(group))
                {
                    _logs[group] += $"{message}\r\n\r\n============================================================\r\n\r\n";
                }
                else
                {
                    _logs[group] = message;
                }
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_busy)
            {
                return;
            }
            lock (_logs)
            {
                _busy = true;
                try
                {
                    string filePath = null;
                    foreach (var log in _logs)
                    {
                        if (filePath == null)
                        {
                            filePath = GetOrCreateFilePath(log.Key);
                        }
                        if (string.IsNullOrEmpty(log.Value))
                        {
                            continue;
                        }
                        File.AppendAllText(filePath, log.Value);
                        _logs.Remove(log.Key);
                    }
                }
                catch (Exception)
                {

                }
                _busy = false;
            }
        }

        public Dictionary<string, string> GetLogs()
        {
            return _logs;
        }

        private string GetOrCreateFilePath(string group)
        {
            var filePath = $"{AppContext.RootFolder}\\_logs\\{@group}\\{DateTime.Now.ToString("yyyy-MM-dd")}.loog";
            var folder = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (File.Exists(filePath) == false)
            {
                File.Create(filePath).Close();
            }
            return filePath;
        }
    }
    
}
