namespace Core.Interfaces.Helpers;

public interface ILogWriter
{
    void Write(string path, params string[] messages);
    void Write(string message);
    void ConsoleWrite(string message, ConsoleColor color = ConsoleColor.Cyan, bool inline = false);
}
