using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NameSearch.Tests
{
    class Global
    {
        public static void SetDataDirectory()
        {
            string dataPath = Path.GetFullPath(@"..\..\..\NameSearch\App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", dataPath);
        }
    }
}
