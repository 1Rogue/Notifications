using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notify;

namespace Notify.Filters
{
    public class SpigotMCFilter : GmailFilter
    {

        public SpigotMCFilter(GmailHandler retriever) : base(retriever) { }

        public override string GetRegex()
        {
            return "";
        }

        public override string GetSender()
        {
            return "no-reply@spigotmc.org";
        }

        protected override string GetUrl(string bodyraw)
        {
            return "http://google.com/";
        }

        protected override string GetBody(string bodyRaw)
        {
            return bodyRaw;
        }

        protected override string GetTitle(string titleRaw)
        {
            return titleRaw;
        }
        

    }
    
}