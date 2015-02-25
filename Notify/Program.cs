using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notify
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            GmailHandler retriever = new GmailHandler();
            //testing
            retriever.DisplayNotification(new Notification("Example Subject", "Test body sdiufdasfikujasd fkjasdf sakjld asjbknd asjkbdn askjd asjknbd ", "http://google.com/"));
        }
    }
}
