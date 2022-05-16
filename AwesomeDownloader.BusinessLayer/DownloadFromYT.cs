using VideoLibrary;
using MediaToolkit;

namespace AwesomeDownloader.BusinessLayer {
    static public class DownloadFromYT {
        static public void DownloadMP3(string path, string url) {
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            File.WriteAllBytes(path + vid.FullName, vid.GetBytes());

            var inputFile = new MediaToolkit.Model.MediaFile { Filename = path + vid.FullName };
            var outputFile = new MediaToolkit.Model.MediaFile { Filename = $"{path + vid.FullName}.mp3" };

            using(var engine = new Engine()) {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }
    }
}