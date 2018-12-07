using System.Collections.Generic;
using accounts.tac.local.Models;
using Sitecore.Security;
using Sitecore.Security.Accounts;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.SecurityModel;

namespace accounts.tac.local.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileProvider _userProfileProvider;
        private readonly IProfileSettingsService _profileSettingsService;

        public UserProfileService(IUserProfileProvider userProfileProvider, IProfileSettingsService profileSettingsService)
        {
            _userProfileProvider = userProfileProvider;
            _profileSettingsService = profileSettingsService;
        }
        //private readonly IUpdateContactFacetsService updateContactFacetsService;
        //private readonly IAccountTrackerService accountTrackerService;
        
        public string GetUserDefaultProfileId()
        {
            return _profileSettingsService.GetUserDefaultProfile()?.ID?.ToString();
        }

        public EditProfile GetEmptyProfile()
        {
            return new EditProfile
            {
                InterestTypes = _profileSettingsService.GetInterests()
            };
        }

        public EditProfile GetProfile(User user)
        {
            SetProfileIfEmpty(user);

            var properties = _userProfileProvider.GetCustomProperties(user.Profile);

            var model = new EditProfile
            {
                Email = user.Profile.Email,
                FirstName = properties.ContainsKey(Constants.UserProfile.Fields.FirstName) ? properties[Constants.UserProfile.Fields.FirstName] : "",
                LastName = properties.ContainsKey(Constants.UserProfile.Fields.LastName) ? properties[Constants.UserProfile.Fields.LastName] : "",
                PhoneNumber = properties.ContainsKey(Constants.UserProfile.Fields.PhoneNumber) ? properties[Constants.UserProfile.Fields.PhoneNumber] : "",
                Interest = properties.ContainsKey(Constants.UserProfile.Fields.Interest) ? properties[Constants.UserProfile.Fields.Interest] : "",
                InterestTypes = _profileSettingsService.GetInterests()
            };

            return model;
        }

        public void SaveProfile(UserProfile userProfile, EditProfile model)
        {
            var properties = new Dictionary<string, string>
            {
                [Constants.UserProfile.Fields.FirstName] = model.FirstName,
                [Constants.UserProfile.Fields.LastName] = model.LastName,
                [Constants.UserProfile.Fields.PhoneNumber] = model.PhoneNumber,
                [Constants.UserProfile.Fields.Interest] = model.Interest,
                [nameof(userProfile.Name)] = model.FirstName,
                [nameof(userProfile.FullName)] = $"{model.FirstName} {model.LastName}".Trim()
            };

            _userProfileProvider.SetCustomProfile(userProfile, properties);
            //_updateContactFacetsService.UpdateContactFacets(userProfile);
            //_accountTrackerService.TrackEditProfile(userProfile);
        }
        
        private void SetProfileIfEmpty(User user)
        {
            if (Context.User.Profile.ProfileItemId != null) return;

            user.Profile.ProfileItemId = GetUserDefaultProfileId();
            user.Profile.Save();
        }
    }
}