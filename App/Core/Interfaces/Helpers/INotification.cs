namespace Core.Interfaces.Helpers;

public interface INotification
{
    void Add(string message);
    void Add(string message, NotificationType type);
    bool Any();
    bool Any(NotificationType type);
    IEnumerable<string> Get();
    IEnumerable<string> Get(NotificationType type);
    string First();
    string First(NotificationType type);
}
