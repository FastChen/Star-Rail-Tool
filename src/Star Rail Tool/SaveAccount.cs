using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Rail_Tool
{
    public class SaveAccount
    {
        const string MIHOYOSDK_ADL_PROD_CN_h3123967166 = "MIHOYOSDK_ADL_PROD_CN_h3123967166";
        const string App_LastUserID_h2841727341 = "App_LastUserID_h2841727341";
        static RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\miHoYo\崩坏：星穹铁道", true);
        byte[] ADL_PROD;
        int App_LastUserID;

        string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NullCraft\Star Rail Tool\Account";

        public SaveAccount()
        {
            CheckDirExists(savePath);
        }

        private void CheckDirExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool HasRegKey()
        {
            return regKey != null;
        }

        private void GetData()
        {
            if (!HasRegKey())
            {
                return;
            }

            ADL_PROD = (byte[])regKey.GetValue(MIHOYOSDK_ADL_PROD_CN_h3123967166);
            App_LastUserID = (int)regKey.GetValue(App_LastUserID_h2841727341);
            //DATA = (byte[])regKey.GetValue(GENERAL_DATA_h2389025596);
        }

        public bool WriteAccountRegKey(string name)
        {
            if (!HasRegKey())
            {
                return false;
            }

            string path = savePath + $@"\{name}";
            ADL_PROD = File.ReadAllBytes(path + @"\MIHOYOSDK_ADL_PROD_CN_h3123967166");
            App_LastUserID = Convert.ToInt32(File.ReadAllText(path + @"\App_LastUserID_h2841727341"));

            regKey.SetValue("MIHOYOSDK_ADL_PROD_CN_h3123967166", ADL_PROD);
            regKey.SetValue("App_LastUserID_h2841727341", App_LastUserID);

            return true;
        }

        public bool SaveAccountFile(string name)
        {
            GetData();

            bool canSave = false;
            if (ADL_PROD != null)
            {
                string path = savePath + $@"\{name}";

                CheckDirExists(path);

                File.WriteAllText(path + @"\MIHOYOSDK_ADL_PROD_CN_h3123967166", Encoding.UTF8.GetString(ADL_PROD));
                File.WriteAllText(path + @"\App_LastUserID_h2841727341", App_LastUserID.ToString());

                canSave = true;
            }
            return canSave;
        }
    }
}
