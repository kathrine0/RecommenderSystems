using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Common.Logger
{
    public class Logger
    {

        public ConcurrentLog<LogItem> Logs { get; private set; }

        public Logger()
        {
            Logs = new ConcurrentLog<LogItem>();
        }

        public void AddProgressReport(ProgressState progress)
        {
            Logs.Add(new LogItem(LogType.ProgressReport, progress));
        }
        
        public void AddWarningReport(WarningReport report)
        {
            Logs.Add(new LogItem(LogType.WarningReport, report));
        }

        public void AddErrorReport(int progress)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(int progress)
        {
            throw new NotImplementedException();
        }
    }


    public class ConcurrentLog<T> : ConcurrentBag<T> where T : LogItem
    {
        public event EventHandler<LogItemAddedEventArgs> OnAdd;

        public new void Add(T item)
        {
            base.Add(item);

            OnAdd?.Invoke(this, new LogItemAddedEventArgs(item));
        }

    }

    public class LogItem
    {

        public LogItem(LogType type, object value)
        {
            Type = type;
            Value = value;
        }

        public LogType Type{ get; private set; }

        public object Value { get; private set; }
    }

    public enum LogType
    {
        ProgressReport,
        WarningReport,
        ErrorReport,
        Message
    }

    public class LogItemAddedEventArgs : EventArgs
    {
        public LogItemAddedEventArgs(LogItem logItem)
        {
            LogItem = logItem;
        }

        public LogItem LogItem { get; set; }
    }
}
