using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace IAIP.Testx.ExtensionMethods
{
    public static class ControlExtensions
    {
        private static readonly MethodInfo onValidating = typeof(Control).GetMethod("OnValidating", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo onValidated = typeof(Control).GetMethod("OnValidated", BindingFlags.Instance | BindingFlags.NonPublic);

        public static bool Validate(this Control control)
        {
            CancelEventArgs e = new CancelEventArgs();

            onValidating.Invoke(control, new object[] { e });
            if (e.Cancel) return false;

            onValidated.Invoke(control, new object[] { EventArgs.Empty });
            return true;
        }
    }
}
