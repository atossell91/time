using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class WorkingDirectory
    {
        public readonly static string DEFAULT_DIR = "P:\\";

        public readonly string SubFolder;
        public static string RootDirectory()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (Directory.Exists(DEFAULT_DIR))
            {
                Debug.WriteLine("Directory " + DEFAULT_DIR + " located.");
                return DEFAULT_DIR;
            }
            else if (Directory.Exists(docs))
            {
                Debug.WriteLine("Could not find " + DEFAULT_DIR + ". Using Documents folder at: " + docs);
                return docs;
            }
            else
            {
                Debug.WriteLine("No directories found");
                return "";
            }
        }

        public WorkingDirectory(string subFolder)
        {
            this.SubFolder = subFolder;
        }
        public string DirectoryPath
        {
            get
            {
                return RootDirectory() + "\\" + SubFolder;
            }
        }
    }
}
