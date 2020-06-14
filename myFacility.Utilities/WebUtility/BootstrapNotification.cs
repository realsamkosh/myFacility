using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities.WebUtility
{
    /*                                                                      *
*      This extension was derive from Brad Christie's answer           *
*      on StackOverflow.                                               *
*                                                                      *
*      The original code can be found at:                              *
*      http://stackoverflow.com/a/18338264/998328                      *
*                                                                      */
    public static class BootstrapNotification
    {
        private static readonly IDictionary<String, String> NotificationKey = new Dictionary<String, String>
        {
            { "Error",      "App.Notifications.Error" },
            { "Warning",    "App.Notifications.Warning" },
            { "Success",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };

        public static void AddNotification(this Controller controller, String message, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);

            if (!(controller.TempData[NotificationKey] is ICollection<String> messages))
            {
                controller.TempData[NotificationKey] = (messages = new List<String>());
            }

            messages.Add(message);
        }

        public static IEnumerable<String> GetNotifications(this IHtmlHelper htmlHelper, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            return htmlHelper.ViewContext.TempData[NotificationKey] as ICollection<String> ?? null;
        }

        private static string getNotificationKeyByType(string notificationType)
        {
            try
            {
                return NotificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
    }

    public static class NotificationType
    {
        public const string ERROR = "Error";
        public const string WARNING = "Warning";
        public const string SUCCESS = "Success";
        public const string INFO = "Info";

    }
}
