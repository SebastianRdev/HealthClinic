namespace HealthClinic.interfaces;

/// <summary>
/// Interface that defines the ability to send notifications in the HealthClinic system.
/// </summary>
public interface INotificable
{
    /// <summary>
    /// Send a notification to the relevant user or entity.
    /// </summary>
    void SendNotification();
}
