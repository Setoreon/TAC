using System;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Security;
using Sitecore.SecurityModel;

namespace events.tac.local.Business
{
    public class LikeService
    {
        private struct Errors
        {
            public const string CantGetEventItem = "Can't get event item";
            public const string EventIsAlreadyLiked = "Event is already liked";
            public const string EventIsNotLiked = "Event is not liked";
        }
        
        public int Like(string itemId, UserProfile userProfile)
        {
            using (new SecurityDisabler())
            {
                Assert.IsNotNullOrEmpty(itemId, nameof(itemId));
                Assert.IsNotNull(userProfile, nameof(userProfile));
                
                var eventItem = GetEventItem(itemId) ?? throw new Exception(Errors.CantGetEventItem);
                var likeList = userProfile[Constants.UserProfile.Fields.LikedEvents].Split('|').ToList();
                if (likeList.Contains(itemId))
                    throw new Exception(Errors.EventIsAlreadyLiked);
                if (!int.TryParse(eventItem.Fields[Constants.EventDetails.Fields.LikeCount].Value, out var count)) count = 0;
                
                count++;
                likeList.Add(itemId);

                eventItem.Editing.BeginEdit();
                try
                {
                    userProfile[Constants.UserProfile.Fields.LikedEvents] = string.Join("|", likeList);
                    eventItem.Fields[Constants.EventDetails.Fields.LikeCount].Value = count.ToString();
                }
                finally
                {
                    userProfile.Save();
                    eventItem.Editing.EndEdit();
                }

                return count;
            }
        }

        public int Unlike(string itemId, UserProfile userProfile)
        {
            using (new SecurityDisabler())
            {
                Assert.IsNotNullOrEmpty(itemId, nameof(itemId));
                Assert.IsNotNull(userProfile, nameof(userProfile));
                
                var eventItem = GetEventItem(itemId) ?? throw new Exception(Errors.CantGetEventItem);
                var likeList = userProfile[Constants.UserProfile.Fields.LikedEvents].Split('|').ToList();
                if (!likeList.Contains(itemId))
                    throw new Exception(Errors.EventIsNotLiked);
                if (!int.TryParse(eventItem.Fields[Constants.EventDetails.Fields.LikeCount].Value, out var count)) count = 1;
                
                count--;
                likeList.Remove(itemId);

                eventItem.Editing.BeginEdit();
                try
                {
                    userProfile[Constants.UserProfile.Fields.LikedEvents] = string.Join("|", likeList);
                    eventItem.Fields[Constants.EventDetails.Fields.LikeCount].Value = count.ToString();
                }
                finally
                {
                    userProfile.Save();
                    eventItem.Editing.EndEdit();
                }

                return count;
            }
        }

        public bool IsLiked(string itemId, UserProfile userProfile)
        {
            Assert.IsNotNullOrEmpty(itemId, nameof(itemId));
            Assert.IsNotNull(userProfile, nameof(userProfile));

            var likeList = userProfile[Constants.UserProfile.Fields.LikedEvents].Split('|').ToList();
            return likeList.Contains(itemId);
        }

        private static Item GetEventItem(string itemId)
        {
            var item = Context.Database.GetItem(new ID(itemId));
            return item.TemplateID == Templates.EventDetails.ID ? item : null;
        }
    }
}