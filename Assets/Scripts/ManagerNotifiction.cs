#if UNITY_ANDROID
using System;

using Unity.Notifications.Android;



     

public static class ManagerNotifiction
{
    public enum TimeScale
    {
        Hours,
        Minutes,
        Seconds

    }
    public static void CreateNotification(string title, string text, int timeValue, TimeScale timeScale)
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = "bolt_icon";

        switch (timeScale)
        {
            case TimeScale.Hours:
                notification.FireTime = DateTime.Now.AddHours(timeValue);

                break;
            case TimeScale.Minutes:
                notification.FireTime = DateTime.Now.AddMinutes(timeValue);

                break;

            case TimeScale.Seconds:
                notification.FireTime = DateTime.Now.AddSeconds(timeValue);

                break;

        }

        SendNotification(notification);
    }

    static void SendNotification(AndroidNotification notification)
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "default Channel",
            Importance = Importance.Default,
            Description = "Generic Notification"
            
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);          
        
        int id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(id);

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }

    }


    //public static int ConvertToSeconds(int value, TimeScale scale)
    //{
    //    int newValue;
    //    switch (scale)
    //    {
    //        case TimeScale.EfromHoursTo:

    //            break;
    //        case TimeScale.EfromMinutesTo:

    //            break;

    //        case TimeScale.EfromSecondsTo:
    //            return value;

    //    }

    //}
}
#endif

