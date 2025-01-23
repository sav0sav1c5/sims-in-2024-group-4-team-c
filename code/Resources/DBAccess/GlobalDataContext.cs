using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public static class GlobalDataContext
    {
        public static DataContext DataContext{ get; private set; } = new DataContext();
    }
}
