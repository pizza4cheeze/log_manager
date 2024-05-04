using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using LogManagement;

namespace LogManagement.ViewModel
{
    public class LogManagerViewModel : INotifyPropertyChanged
    {
        private LogManager logManager;
        private string saveFilePath;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LogMessage> Messages { get; private set; }

        public ICommand SaveToFileCommand { get; private set; }

        public LogManagerViewModel(LogManager logManager)
        {
            this.logManager = logManager;
            Messages = new ObservableCollection<LogMessage>(logManager.GetMessagesInRange(DateTime.MinValue, DateTime.MaxValue));

            SaveToFileCommand = new RelayCommand(SaveToFile);
        }

        private void SaveToFile(object obj)
        {
            if (!string.IsNullOrEmpty(saveFilePath))
            {
                logManager.SaveMessagesToFile(saveFilePath);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

