using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VsmdWorkstation
{
    class Logger
    {
        string file;
        static Logger instance;
        Logger()
        {
            file = GetExeFolder() + "log.txt";
            if (File.Exists(file))
                File.Delete(file);
        }
        string GetExeFolder()
        {
            string s = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return s + "\\";
        }

        public static Logger  Instance
        {
            get {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }

        public void Write(string s)
        {
            File.AppendAllLines(file, new List<string>() { DateTime.Now.ToString("hhmmss") +" : "+ s });
        }

    }
}
