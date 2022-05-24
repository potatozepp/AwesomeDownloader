namespace AwesomeDownloader.Models {
    public class MP3Data {
        public int Author { get; set; }
        
        public string? Title { get; set; }

        public TimeSpan Duration { get; set; }

        public int FileType { get; set; }

        public int FileSize { get; set; }

    }
}