using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalvaExamenP3.Repositories
{
    public class LogsRepository
    {
        public static async Task AppendLogAsync(string nombre)
        {
            string logFile = Path.Combine(FileSystem.AppDataDirectory, "Logs_Calva.txt");
            string logEntry = $"Se incluyó el registro [{nombre}] el {DateTime.Now:dd/MM/yyyy HH:mm}\n";
            await File.AppendAllTextAsync(logFile, logEntry);
        }

        public static async Task<List<string>> LeerLogsAsync()
        {
            string logFile = Path.Combine(FileSystem.AppDataDirectory, "Logs_Calva.txt");
            if (File.Exists(logFile))
                return File.ReadAllLines(logFile).ToList();
            return new List<string>();
        }
    }
}
