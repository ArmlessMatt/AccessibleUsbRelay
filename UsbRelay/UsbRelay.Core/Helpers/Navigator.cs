using System;

namespace UsbRelay.Core.Helpers
{
    public static class Navigator
    {
        private static Action<string> navMethod = null;

        public static void Init(Action<string> navigationMethod)
        {
            navMethod = navigationMethod;
        }

        public static void Navigate(string URI)
        {
            navMethod(URI);
        } 
    }
}
