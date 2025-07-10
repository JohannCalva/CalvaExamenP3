using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalvaExamenP3.Repositories;

namespace CalvaExamenP3.ViewModels
{
    public class LogsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Logs { get; } = new();

        public async Task CargarLogsAsync()
        {
            Logs.Clear();
            var lines = await LogsRepository.LeerLogsAsync();
            foreach (var line in lines)
                Logs.Add(line);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
