using System.IO;
using System.Windows.Forms;

namespace BarometersBRS1M
{
    static class GlobalVariable
    {
        public const string PROVIDER_JET = "Provider=Microsoft.Jet.OLEDB.4.0;";
        public static string gПутьРесурсы;

        public static string BuildCnnStr(string provider, string dataBase)
        {
            //Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source="D:\ПрограммыVBNET\RUD\RUD.NET\bin\Ресурсы\Channels.mdb";Jet OLEDB:Engine Type=5;Provider="Microsoft.Jet.OLEDB.4.0";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1
            //Public Const strProviderJet As String = "Provider=Microsoft.Jet.OLEDB.4.0;"
            string strTemp = string.Format("{0}Data Source={1};", provider, dataBase);
            return strTemp;
        }

        private static bool FileExists(ref string fileName)
        {
            //FileExists = CBool(Dir(FileName) = vbNullString) ' True - файла нет
            return !File.Exists(fileName);
        }

        public static bool ПроверкаСуществованияФайла(string путьФайла)
        {
            if (FileExists(ref путьФайла))
            {
                MessageBox.Show(string.Format("В каталоге нет файла <{0}> !", путьФайла), "Провека существования файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            { return true; }
        }
    }
}
