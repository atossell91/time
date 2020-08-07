using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class Fixer_Functions
    {
        private readonly static string logfilename = "fixed_data_log.txt";
        private static void saveToLog(string message)
        {
            string outputString = DateTime.Now.ToString() + ": " + message;
            try
            {
                File.AppendAllText(logfilename, outputString + Environment.NewLine);
            }
            catch (Exception e) { };
        }
        public static void SAVEBACKUPFILE(string currentFilePath, string[] filecontents, string backupTag)
        {
            FileInfo f = new FileInfo(currentFilePath);
            string pathname = f.DirectoryName + "\\" + f.Name + "_" + backupTag + "_backup" + f.Extension;
            if (!File.Exists(pathname))
            {
                File.WriteAllLines(pathname, filecontents);
            }
        }
        public static void _CORRECT_WASHUP(DateTime start, DateTime end, ref TimeSpan wash, string word)
        {
            TimeSpan val;
            if (!TimeSpan.TryParse(word, out val))
            {
                saveToLog("Washup time data updated");
                val = ShiftInformation.CalcWashupTime(start, end, ShiftInformation.LunchLength);
            }
            wash = val;
        }
    }
}
