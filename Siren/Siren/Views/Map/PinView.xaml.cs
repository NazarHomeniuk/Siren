using Xamarin.Forms;

namespace Siren.Views.Map
{
    public partial class PinView : StackLayout
    {
        public PinView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}