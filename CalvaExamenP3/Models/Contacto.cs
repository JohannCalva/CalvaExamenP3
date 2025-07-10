using SQLite;

namespace CalvaExamenP3.Models
{
    [Table("Contactos")]
    public class Contacto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100), NotNull]
        public string Nombre { get; set; }
        [MaxLength(100), NotNull, Unique]
        public string Correo { get; set; }
        private string _telefono;
        [MaxLength(15), NotNull, Unique]
        public string Telefono 
        {       
            get => _telefono;
            set
            {
                if (!EsTelefonoEcuatoriano(value))
                    throw new ArgumentException("El numero de telefono debe iniciar con +593.");
                _telefono = value;
            }
        }
        [NotNull]
        public bool Favorito { get; set; }
        private bool EsTelefonoEcuatoriano(string numero)
        {
            return numero.StartsWith("+593") && numero.Length == 13;
        }
    }
}
