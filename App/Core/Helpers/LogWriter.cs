namespace Core.Helpers;

public class LogWriter : ILogWriter
{
    private readonly AppSetting _appSetting;

    public LogWriter(AppSetting appSetting)
    {
        _appSetting = appSetting;
    }

    private static readonly object _lock = new();

    public void Write(string path, params string[] messages)
    {
        lock (_lock)
        {
            path = Path.Combine(_appSetting.PathLog, AppSetting.GetApplicationName(), path);

            var directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var now = DateTime.Now;
            path = Path.Combine(directoryPath, $"{now:yy-MM-dd}_{Path.GetFileName(path)}");

            foreach (var message in messages)
            {
                var messageInline = message.Replace("\r", @"\r").Replace("\n", @"\n");
                File.AppendAllText(path, $"[{now:HH:mm:ss}] {messageInline}{Environment.NewLine}");
            }

            if (Debugger.IsAttached)
                ConsoleWrite($"Um log foi salvo em \"{path}\"", ConsoleColor.Green);
        }
    }

    public void Write(string message) => Write("Log.txt", message);

    private static readonly object _lockConsole = new();

    public void ConsoleWrite(string message, ConsoleColor color = ConsoleColor.Cyan, bool inline = false)
    {
        lock (_lockConsole)
        {
            Console.ForegroundColor = color;

            if (inline)
                Console.Write(message);
            else
                Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}
