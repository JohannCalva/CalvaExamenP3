using SQLite;
using CalvaExamenP3.Models;

namespace CalvaExamenP3.Repositories
{
    public class ContactoSQLiteRepository
    {
        SQLiteConnection database;

        public ContactoSQLiteRepository(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Contacto>();
        }

        public List<Contacto> ObtenerContactos() => database.Table<Contacto>().ToList();

        public void AgregarContacto(Contacto contacto) => database.Insert(contacto);
    }
}
