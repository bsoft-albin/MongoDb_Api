﻿namespace MongoDb_Api.Frameworks.CommonMeths
{
    public static class ErrorLogger
    {
        static private String _contentRootPath = String.Empty;
        public static void Initialize(IHostEnvironment hostingEnvironment)
        {
            _contentRootPath = hostingEnvironment.ContentRootPath;
        }

        /// <summary>
        /// Custom ErrorLog Method for writing Exceptions in the text file.
        /// </summary>
        /// <param name="message">Takes the message from the Exception cause.</param>
        /// <param name="exception">Gets the Actaul Exception message</param>
        public static async Task WriteLog(Exception exception, String message = "")
        {
            String logFolderPathWithMonthAndyear = Path.Combine(Path.Combine(_contentRootPath, "ErrorLogsDirectory"), DateTime.UtcNow.ToString("yyyy-MM"));
            // Check if the subfolder exists, create it if not
            if (!Directory.Exists(logFolderPathWithMonthAndyear))
            {
                Directory.CreateDirectory(logFolderPathWithMonthAndyear);
            }
            String FinallogFilePath = Path.Combine(logFolderPathWithMonthAndyear, $"errorlog-{DateTime.UtcNow:dd-MM-yyyy}.log");

            try
            {
                using StreamWriter writer = new(FinallogFilePath, append: true);
                await writer.WriteLineAsync($"[Error Timestamp: {DateTime.UtcNow:dd-MM-yyyy} {DateTime.UtcNow.Hour.ToString() + ":" + DateTime.UtcNow.Minute.ToString() + ":" + DateTime.UtcNow.Minute.ToString() + " " + DateTime.UtcNow.ToString("tt", System.Globalization.CultureInfo.InvariantCulture)}]");
                await writer.WriteLineAsync($"Message: {message}");
                await writer.WriteLineAsync($"Exception: {exception.Message}");
                await writer.WriteLineAsync($"Source: {exception.Source}");
                await writer.WriteLineAsync($"StackTrace: {exception.StackTrace}");
                await writer.WriteLineAsync($"TargetSite: {exception.TargetSite}");

                await writer.WriteLineAsync(new String('-', 200)); // Separator for readability
            }
            catch (Exception ex)
            {
                await Task.Run(() => Console.WriteLine(ex));
            }
        }
    }
}
