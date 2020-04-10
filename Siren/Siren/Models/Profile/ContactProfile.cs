using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;

namespace Siren.Models
{
    [Preserve(AllMembers = true)]
    public class ContactProfile : INotifyPropertyChanged
    {
        private string imagePath;
        private string status;
        private string name;
        private string email;

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

        public string Status
        {
            get => status;
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}