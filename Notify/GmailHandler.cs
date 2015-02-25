using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notify.Filters;

namespace Notify
{
    public class GmailHandler
    {
        private readonly HashSet<GmailFilter> filters = new HashSet<GmailFilter>();
        
        public GmailHandler() {
            this.filters.Add(new SpigotMCFilter(this));
        }

        public bool OnEmail(String sender, String title, String email) {
            if (email == null)
            {
                throw new ArgumentNullException();
            }
            bool back = false;
            foreach(GmailFilter filter in filters)
            {
                if (filter.IsSender(sender))
                {
                    Notification n = filter.OnNotify(title, email);
                }
            }
            return back;
        }

        //Potentially cache Form object
        public void DisplayNotification(Notification notif)
        {
            Rectangle workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            Form back = new Form();
            back.StartPosition = FormStartPosition.Manual;
            back.ClientSize = new Size(200, 75);
            int left = workingArea.Width - back.Width;
            int top = workingArea.Height - back.Height;
            back.Location = new Point(left, top);
            back.ShowInTaskbar = false;
            back.ShowIcon = false;
            back.MinimizeBox = false;
            back.MaximizeBox = false;
            back.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            back.TopMost = true;
            back.ControlBox = false;
            back.Text = string.Empty;
            Panel p = new Panel();
            p.Click += notif.handle_click;
            back.Controls.Add(p);
            back.Show();
            this.FadeForm(500, 100, back, true, null);
            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += (arg1, arg2) =>
            {
                this.FadeForm(500, 100, back, false, (form) => {
                    form.Hide();
                    form.Dispose();
                });
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
            Application.EnableVisualStyles();
            Application.Run(back); //form is disposed via delegate timer
        }

        private void FadeForm(int duration, int steps, Form target, bool fadeIn, FormAction action)
        {
            target.Opacity = fadeIn ? 0 : 1;
            Timer timer = new Timer();
            timer.Interval = duration / steps;
            int currentStep = 0;
            timer.Tick += (arg1, arg2) =>
            {
                double magnitude = ((double)currentStep) / steps;
                target.Opacity = fadeIn ? magnitude : 1 - magnitude;
                
                currentStep++;

                if (currentStep >= steps)
                {
                    if (action != null)
                    {
                        action(target);
                    }
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        private delegate void FormAction(Form action);
    }

}
