using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp
{
    public static class GlobalConfig
    {
        public static string DbPath { get; private set; }

        static GlobalConfig()
        {
            InitializeDbPath();
        }

        private static void InitializeDbPath()
        {
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;

            if (projectRoot != null)
            {
                DbPath = Path.Combine(projectRoot, "Resources", "Data");
            }
            else
            {
                throw new InvalidOperationException("Unable to determine project root directory.");
            }
        }

        public static string CnnVal(String name)
        {
            return $"Data Source={DbPath}\\{name}";
        }
    }
}
