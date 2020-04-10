using Siren.Models.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Siren.Annotations;
using Siren.Contracts.Services;
using Siren.ViewModels.Social;
using Siren.Views.Navigation;
using Siren.Views.Social;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace Siren.ViewModels.Navigation
{
    /// <summary>
    /// ViewModel for suggestion page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class SuggestionViewModel : INotifyPropertyChanged
    {
        private readonly SuggestionPage page;
        private readonly IUserService userService;

        #region Fields

        private Command<object> itemTappedCommand;

        private Command suggestionCommand;

        private ObservableCollection<Suggestion> suggestionList;

        private string searchText;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public SuggestionViewModel(IUserService userService, SuggestionPage page)
        {
            this.userService = userService;
            this.page = page;
            InitPeople();
        }

        #region Properties

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the suggestion page.
        /// </summary>
        [DataMember(Name = "suggestionList")]
        public ObservableCollection<Suggestion> SuggestionList
        {
            get => suggestionList;
            set
            {
                suggestionList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>   
        public Command SuggestionCommand
        {
            get
            {
                return this.suggestionCommand ?? (this.suggestionCommand = new Command(this.SuggestionClicked));
            }
        }

        #endregion

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the suggestion page.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private async void NavigateToNextPage(object selectedItem)
        {
            var item = selectedItem as ItemTappedEventArgs;
            var suggestion = item?.ItemData as Suggestion;
            var profilePage = new SocialProfileWithInterestsPage(suggestion.Id);
            await page.Navigation.PushAsync(profilePage);
        }

        /// <summary>
        /// Invoked when the suggestion button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SuggestionClicked(object obj)
        {
            //Do something
        }

        private async void InitPeople()
        {
            var suggestions = await userService.GetUserSuggestions();
            SuggestionList = new ObservableCollection<Suggestion>(suggestions.Select(s => new Suggestion
            {
                Id = s.UserId,
                ImagePath = s.UserImage,
                SuggestionName = s.UserName
            }));
        }

        #endregion
    }
}
