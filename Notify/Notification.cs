using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notify
{
    public class Notification
    {

        public string url { get; private set; }
        public string body { get; private set; }
        public string title { get; private set; }

        public Notification(string title, string body, string url)
        {
            this.title = title;
            this.body = body;
            this.url = url;
        }

        public void handle_click(object sender, EventArgs e)
        {
            Process.Start(this.url);
        }
    }
}
