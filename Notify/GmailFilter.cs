using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notify
{
    public abstract class GmailFilter
    {
        private readonly GmailHandler retriever;

        protected GmailFilter(GmailHandler retriever)
        {
            this.retriever = retriever;
        }

        public Notification OnNotify(String title, String body)
        {
            if (new Regex(this.GetRegex(), RegexOptions.IgnoreCase).IsMatch(body))
            {
                return new Notification(this.GetTitle(title), this.GetBody(body), this.GetUrl(body));
            }
            return null;
        }

        public bool IsSender(string sender)
        {
            return this.GetSender().Equals(sender, StringComparison.OrdinalIgnoreCase);
        }

        public abstract string GetRegex();

        public abstract string GetSender();

        protected abstract string GetUrl(string bodyRaw);

        protected abstract string GetBody(string bodyRaw);

        protected abstract string GetTitle(string titleRaw);

    }
}
