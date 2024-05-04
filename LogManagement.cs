using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogManagement
{
    public enum LogMessageType
    {
        Error,
        Warning,
        Information
    }

    public struct LogMessage
    {
        public LogMessageType Type { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }

    public class LogManager
    {
        private List<LogMessage> messages;

        public LogManager()
        {
            messages = new List<LogMessage>();
        }

        public int Count => messages.Count;

        public LogMessage this[int index]
        {
            get
            {
                if (index < 0 || index >= messages.Count)
                    throw new IndexOutOfRangeException("Index is out of range.");

                return messages[index];
            }
        }

        public IEnumerable<LogMessage> GetMessagesByType(LogMessageType type)
        {
            return messages.Where(m => m.Type == type);
        }

        public IEnumerable<LogMessage> GetMessagesInRange(DateTime start, DateTime end)
        {
            return messages.Where(m => m.DateTime >= start && m.DateTime <= end);
        }

        public void AddMessage(LogMessage message)
        {
            messages.Add(message);
        }

        public void SaveMessagesToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var message in messages)
                {
                    writer.WriteLine($"{message.DateTime} [{message.Type}]: {message.Text}");
                }
            }
        }
    }
}
