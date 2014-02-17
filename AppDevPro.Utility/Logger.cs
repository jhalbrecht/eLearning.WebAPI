using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDevPro.Utility
{
    public class Logger
    {
        public void Log(string msg)
        {
            Debug.WriteLine("AdpLogger Log: {0} {1} ", DateTime.Now, msg);
        }

        /// <summary>
        /// Display console debugging info. DateTime.Now, calling this. , string msg
        /// </summary>
        /// <param name="from"></param>
        /// <param name="msg"></param>
        public void Log(object from, string msg)
        {
            Debug.WriteLine("AdpLogger Log: {0} {1} {2}", DateTime.Now, @from, msg);
        }

        /// <summary>
        /// Display console debuggin information. DateTime.Now, From(generally this), stringMsg, anotheStringrMsg
        /// </summary>
        /// <param name="from"></param>
        /// <param name="msg"></param>
        /// <param name="anotherMsg"></param>
        public void Log(object from, string msg, string anotherMsg)
        {
            Debug.WriteLine("AdpLogger Log: {0} {1} {2} {3}", DateTime.Now, @from, msg, anotherMsg);
        }
    }
}