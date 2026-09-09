using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using SistemaCalificaciones.Data.Config; // <-- Importante: Añadir el using de tu capa Data
using SistemaCalificaciones.UI.Views;

namespace SistemaCalificaciones.UI
{
    static class Program
    {
        public static IConfiguration Configuration;

        [STAThread]
        static void Main()
        {
            // 1. Leer el archivo appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // 2. Extraer la cadena de conexión del JSON
            string cadenaConexion = Configuration.GetConnectionString("PostgresConnection");

            // 3. ¡AQUÍ ESTÁ LA MAGIA!
            // Le pasamos la cadena a la capa de Datos (Data)
            ConexionDB.Inicializar(cadenaConexion);

            // 4. Preparar y lanzar la interfaz gráfica
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }
    }
}