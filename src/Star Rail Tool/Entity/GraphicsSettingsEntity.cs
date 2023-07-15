using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Star_Rail_Tool
{
    public class GraphicsSettingsEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool enabled
        {
            get
            {
                return IsEnabled;
            }
            set
            {
                IsEnabled = value;
            }
        }
    }
}
