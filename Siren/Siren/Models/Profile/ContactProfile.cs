﻿using Xamarin.Forms.Internals;

namespace Siren.Models.Profile
{
    [Preserve(AllMembers = true)]
    public class ContactProfile
    {
        #region Public Property

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        #endregion
    }
}