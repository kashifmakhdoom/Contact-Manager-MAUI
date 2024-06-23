using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contman.Infrastructure
{
    public class Config
    {
        public const string DatabaseFileName = "Contman.db3";
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

        public const string ApiBaseUrl = "http://10.0.0.0:5665/api";

    }
}
