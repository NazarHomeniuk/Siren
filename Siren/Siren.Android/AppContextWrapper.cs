using Android.Content;

namespace Siren.Droid
{
    public class AppContextWrapper
    {
        public Context AppContext { get; private set; }

        public void UseContext(Context context)
        {
            AppContext = context;
        }
    }
}