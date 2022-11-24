using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionAndInstallationWorksBayramov.ClassFolder
{
    class GlobalClass
    {
        public static string SqlConnection
        {
            get
            {
                return @"Data Source=K218PC\SQLEXPRESS;
                Initial Catalog=ConstructionAndInstallationWorksBayramov;
                Integrated Security=True";
            }
        }
    }
}
