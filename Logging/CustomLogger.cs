namespace ApiCatalogo.Logging;

public class CustomerLogger: ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }
    
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        WriteTextInFile(message);
    }

    private void WriteTextInFile(string mensagem)
    {
        string logFilePath = @"/Users/alanrolim/Documents/backend/ApiCatalogo/Log/log.txt";

        using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } 
    }
}