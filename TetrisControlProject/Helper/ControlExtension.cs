using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisControlProject.Helper
{
    public static class ControlExtension
    {
        public static void SafeInvoke(this Control uiElement, Action updater, bool forceSynchronous)
        {
            if (uiElement == null)
            {
                throw new ArgumentNullException("uiElement");
            }

            if (uiElement.InvokeRequired)
            {
                if (forceSynchronous)
                {
                    uiElement.Invoke((Action)delegate { SafeInvoke(uiElement, updater, forceSynchronous); });
                }
                else
                {
                    uiElement.BeginInvoke((Action)delegate { SafeInvoke(uiElement, updater, forceSynchronous); });
                }
            }
            else
            {
                if (uiElement.IsDisposed)
                {
                    return;
                }

                updater();
            }
        }
    }
}
