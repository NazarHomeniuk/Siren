﻿using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace Siren.Models.Navigation
{
    /// <summary>
    ///Model for suggestion Page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Suggestion
    {
        #region Field

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the imagepath.
        /// </summary>
        [DataMember(Name = "imagePath")]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the suggestion name.
        /// </summary>
        [DataMember(Name = "suggestionName")]
        public string SuggestionName { get; set; }

        /// <summary>
        /// Gets or sets the suggestion id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        #endregion

    }
}
