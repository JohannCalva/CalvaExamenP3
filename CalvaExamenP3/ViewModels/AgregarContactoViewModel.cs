using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CalvaExamenP3.Models;
using CalvaExamenP3.Repositories;

namespace CalvaExamenP3.ViewModels
{
    public class AgregarContactoViewModel : INotifyPropertyChanged
    {
        private readonly ContactoSQLiteRepository _repo;

        public AgregarContactoViewModel()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "contactos.db3");
            _repo = new ContactoSQLiteRepository(dbPath);
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }

        private string correo;
        public string Correo
        {
            get => correo;
            set => SetProperty(ref correo, value);
        }

        private string telefono;
        public string Telefono
        {
            get => telefono;
            set => SetProperty(ref telefono, value);
        }

        private bool favorito;
        public bool Favorito
        {
            get => favorito;
            set => SetProperty(ref favorito, value);
        }

        public async Task GuardarAsync()
        {
            try
            {
                var nuevo = new Contacto
                {
                    Nombre = this.Nombre,
                    Correo = this.Correo,
                    Telefono = this.Telefono,
                    Favorito = this.Favorito
                };

                _repo.AgregarContacto(nuevo);
                await LogsRepository.AppendLogAsync(nuevo.Nombre);

                await Application.Current.MainPage.DisplayAlert("Éxito", "Contacto guardado", "OK");

                Nombre = Correo = Telefono = string.Empty;
                Favorito = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propName);
            return true;
        }
    }
}
