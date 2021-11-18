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

    public class SecureConsoleLogWriter : ILogger
    {
        private ConsoleLogWriter _consoleLogWriter;

        public SecureConsoleLogWriter(ConsoleLogWriter consoleLogWriter)
        {
            _consoleLogWriter = consoleLogWriter;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                _consoleLogWriter.WriteError(message);
            }
        }
    }

    public class SecureFileLogWriter : ILogger
    {
        private FileLogWriter _fileLogWriter;

        public SecureFileLogWriter(FileLogWriter fileLogWriter)
        {
            _fileLogWriter = fileLogWriter;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                _fileLogWriter.WriteError(message);
            }
        }
    }

    public class SecureFileAndConsoleLogWriter : ILogger
    {
        private ConsoleLogWriter _consoleLogWriter;
        private FileLogWriter _fileLogWriter;

        public SecureFileAndConsoleLogWriter(ConsoleLogWriter consoleLogWriter, FileLogWriter fileLogWriter)
        {
            _consoleLogWriter = consoleLogWriter;
            _fileLogWriter = fileLogWriter;
        }

        public void WriteError(string message)
        {
            _consoleLogWriter.WriteError(message);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _fileLogWriter.WriteError(message);
            }
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
