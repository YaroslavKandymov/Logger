using System;
using System.IO;

namespace Logging
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class ConsoleLogWriter : ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileLogWriter : ILogger
    {
        private string _nameFile = "";

        public FileLogWriter(string nameFile)
        {
            _nameFile = nameFile;
        }

        public void WriteError(string message)
        {
            File.WriteAllText(_nameFile, message);
        }
    }

    public class SecureLogWriter : ILogger
    {
        private ILogger _logger;

        public SecureLogWriter(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _logger.WriteError(message);
            }
        }
    }

    public class SecureConsoleLogWriter : SecureLogWriter
    {
        public SecureConsoleLogWriter(ConsoleLogWriter consoleLogWriter) : base(consoleLogWriter)
        {
        }
    }

    public class SecureFileLogWriter : SecureLogWriter
    {
        public SecureFileLogWriter(FileLogWriter fileLogWriter) : base(fileLogWriter)
        {
        }
    }

    public class SecureFileAndConsoleLogWriter : SecureLogWriter
    {
        private ConsoleLogWriter _consoleLogWriter;

        public SecureFileAndConsoleLogWriter(ConsoleLogWriter consoleLogWriter, FileLogWriter fileLogWriter) : base(fileLogWriter)
        {
            _consoleLogWriter = consoleLogWriter;
        }

        public new void WriteError(string message)
        {
            base.WriteError(message);

            _consoleLogWriter.WriteError(message);
        }
    }

    public class Pathfinder : ILogger
    {
        public Pathfinder(ILogger logger, string message)
        {
            logger.WriteError(message);
        }

        public void Find(ILogger logger, string message)
        {
            logger.WriteError(message);
        }

        public void WriteError(string message)
        {
            Console.Write(message);
        }
    }

    public interface ILogger
    {
        void WriteError(string message);
    }
}
