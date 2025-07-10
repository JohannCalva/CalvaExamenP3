using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalvaExamenP3.Models;
using CalvaExamenP3.Repositories;

namespace CalvaExamenP3.ViewModels
{
    public class RegistrosViewModel : INotifyPropertyChanged
    {
        private readonly ContactoSQLiteRepository _repo;

        public ObservableCollection<Contacto> Contactos { get; } = new();

        public RegistrosViewModel()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "contactos.db3");
            _repo = new ContactoSQLiteRepository(dbPath);
        }

        public void CargarContactos()
        {
            Contactos.Clear();
            var lista = _repo.ObtenerContactos();
            foreach (var item in lista)
                Contactos.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
