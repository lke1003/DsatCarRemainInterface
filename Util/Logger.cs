using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Util
{
    public class Logger
    {
        string _logFileName;

        #region Singleton Constructor

        public static Logger Instance
        {
            get
            {
                return loggerCreator.CreatorInstance;
            }
        }

        private sealed class loggerCreator
        {
            //-- Retrieve a single instance of a File//logger
            private static readonly Logger _instance = new Logger();

            //-- Return an instance of the class
            public static Logger CreatorInstance
            {
                get { return _instance; }
            }
        }

        #endregion

        public bool InitFilelogger(string inlogFileName)
        {
            if (!File.Exists(inlogFileName))
            {
                try
                {
                    StreamWriter sw = File.CreateText(inlogFileName);
                    sw.Close();
                    _logFileName = inlogFileName;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                _logFileName = inlogFileName;
            }

            return true;
        }

        public bool Writelog(string inlogFileName, string inlog)
        {
            if (!File.Exists(_logFileName))
            {
                InitFilelogger(inlogFileName);
            }

            try
            {
                StringBuilder strlog = new StringBuilder();
                strlog.Append(DateTime.Now.ToString());
                strlog.Append(" # ");
                strlog.Append(inlog);

                StreamWriter tw = File.AppendText(inlogFileName);
                tw.WriteLine(strlog.ToString());
                tw.Flush();
                tw.Close();

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
