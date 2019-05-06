namespace CheckLinksConsole
{
    using System.IO;

    public class OutputSettings
    {
        public string Folder { get; set; }
        public string File { get; set; }

        public string ReportFilePath
        {
            get => Path.Combine(Directory.GetCurrentDirectory(), Folder, File);
        }

        public string ReportDirectory
        {
            get => Path.GetDirectoryName(ReportFilePath);
        }
    }
}