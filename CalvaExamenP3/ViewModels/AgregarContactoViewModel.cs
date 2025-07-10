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
                // Validaciones antes de guardar
                if (string.IsNullOrWhiteSpace(Nombre))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El nombre es obligatorio", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Correo))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El correo es obligatorio", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Telefono))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El teléfono es obligatorio", "OK");
                    return;
                }

                // Verificar si el teléfono ya existe
                if (_repo.ExisteTelefono(Telefono))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ya existe un contacto con este número de teléfono", "OK");
                    return;
                }

                // Verificar si el correo ya existe
                if (_repo.ExisteCorreo(Correo))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ya existe un contacto con este correo electrónico", "OK");
                    return;
                }

                var nuevo = new Contacto
                {
                    Nombre = this.Nombre,
                    Correo = this.Correo,
                    Telefono = this.Telefono,
                    Favorito = this.Favorito
                };

                _repo.AgregarContacto(nuevo);
                await LogsRepository.AppendLogAsync(nuevo.Nombre);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Contacto guardado correctamente", "OK");

                // Limpiar los campos
                Nombre = Correo = Telefono = string.Empty;
                Favorito = false;
            }
            catch (ArgumentException ex)
            {
                // Error de validación del teléfono ecuatoriano
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                // Cualquier otro error
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al guardar el contacto: {ex.Message}", "OK");
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