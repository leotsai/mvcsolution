using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;

namespace MvcSolution
{
    public class FileLogger : DisposableBase, ILogger
    {
        private const int IntervalSeconds = 1;
        private const long MaxPerFileBytes = 10240;
        private readonly Dictionary<string, LoggingGroup> _dict;
        private readonly Timer _timer;
        private bool _busy = false;

        public FileLogger()
        {
            this._dict = new Dictionary<string, LoggingGroup>();
            this._timer = new Timer(IntervalSeconds * 1000);
            this._timer.Elapsed += TimerElapsed;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_busy)
            {
                return;
            }
            try
            {
                _busy = true;
                this.DoWork();
            }
            catch (Exception)
            {

            }
            finally
            {
                _busy = false;
            }
        }

        private void DoWork()
        {
            var items = new List<WritingItem>();
            lock (_dict)
            {
                foreach (var key in _dict.Keys)
                {
                    var group = this._dict[key];
                    if (group.Sb.Length == 0)
                    {
                        continue;
                    }
                    items.Add(new WritingItem(group));
                    group.Sb.Clear();
                }
            }
            if (items.Count == 0)
            {
                return;
            }
            this.WriteToFile(items);
            lock (_dict)
            {
                foreach (var item in items)
                {
                    var group = this._dict[item.Group];
                    group.LastDate = item.LastDate;
                    group.LastFilePath = item.LastFilePath;
                }
            }
        }

        public void Entry(string group, string message)
        {
            lock (this._dict)
            {
                if (!this._dict.ContainsKey(group))
                {
                    this._dict[group] = new LoggingGroup(group);
                }
                this._dict[group].Sb.Append("\r\n" + message + "\r\n\r\n");
            }
        }

        private void WriteToFile(List<WritingItem> items)
        {
            lock (this)
            {
                foreach (var item in items)
                {
                    try
                    {
                        var date = DateTime.Today.ToString("yyyy-MM-dd");
                        FileInfo file;
                        if (item.LastDate == date)
                        {
                            file = new FileInfo(item.LastFilePath);
                            var parent = file.Directory;
                            if (parent.Exists == false)
                            {
                                Directory.CreateDirectory(parent.FullName);
                            }
                            if (file.Exists && file.Length > MaxPerFileBytes)
                            {
                                var yearMonth = DateTime.Today.ToString("yyyy-MM");
                                var date2 = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
                                var relativePath = $"\\_logs\\{item.Group}\\{yearMonth}\\{date2}.log";
                                file = new FileInfo(AppContext.RootFolder + relativePath);
                            }
                        }
                        else
                        {
                            var yearMonth = DateTime.Today.ToString("yyyy-MM");
                            var relativePath = $"\\_logs\\{item.Group}\\{yearMonth}\\{date}.log";
                            file = new FileInfo(AppContext.RootFolder + relativePath);
                            var parent = file.Directory;
                            if (parent.Exists == false)
                            {
                                Directory.CreateDirectory(parent.FullName);
                            }
                        }
                        File.AppendAllText(file.FullName, item.Text);

                        item.LastDate = date;
                        item.LastFilePath = file.FullName;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        
        private class WritingItem
        {
            public string Group { get; }
            public string Text { get; }
            public string LastDate { get; set; }
            public string LastFilePath { get; set; }

            public WritingItem(LoggingGroup group)
            {
                this.Group = group.Key;
                this.Text = group.Sb.ToString();
                this.LastDate = group.LastDate;
                this.LastFilePath = group.LastFilePath;
            }
        }


        private class LoggingGroup
        {
            public string Key { get; }
            public StringBuilder Sb { get; }
            public string LastDate { get; set; }
            public string LastFilePath { get; set; }

            public LoggingGroup(string key)
            {
                this.Key = key;
                this.Sb = new StringBuilder();
                this.LastDate = "";
                this.LastFilePath = "";
            }
        }


        protected override void DisposeInternal()
        {
            _timer.Dispose();
        }

        ~FileLogger()
        {
            base.MarkDisposed();
        }
    }
    
}
