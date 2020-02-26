using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;

namespace Siren.Models
{
    [Preserve(AllMembers = true)]
    public class ContactProfile : INotifyPropertyChanged
    {
        private string imagePath;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Property

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                NotifyPropertyChanged();
            }
        }

        public string Name { get; set; }
        public string Email { get; set; }

        #endregion

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}