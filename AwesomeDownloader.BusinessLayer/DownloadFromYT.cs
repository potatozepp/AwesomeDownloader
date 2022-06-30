using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using AwesomeDownloader.Models;

namespace AwesomeDownloader.BusinessLayer {
    public class DownloadFromYT {
        public async Task DownloadMP3Async(string path, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=hhxjSaF0Ek4
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);


            var streamInfo = test.GetAudioOnlyStreams().GetWithHighestBitrate();

            string title = video.Title.Replace("|", "");
            title = title.Replace("<", "");
            title = title.Replace(">", "");
            title = title.Replace("!", "");
            title = title.Replace("?", "");
            title = title.Replace(":", "");
            title = title.Replace("\\", "");
            title = title.Replace("/", "");
            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}\\{title}.mp3"); 
        }

        public async Task DownloadMP4Async(string path, string url) {
            var youtube = new YoutubeClient();

            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);

            var streamInfo = test.GetMuxedStreams().GetWithHighestBitrate();
            string title = video.Title.Replace("|", "");
            title = title.Replace("<", "");
            title = title.Replace(">", "");
            title = title.Replace("!", "");
            title = title.Replace("?", "");
            title = title.Replace(":", "");
            title = title.Replace("\\", "");
            title = title.Replace("/", "");
            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}\\{title}.mp4");
        }

        public async Task DownloadMP3AndMP4(string path, string url) {
            await DownloadMP3Async(path, url);
            await DownloadMP4Async(path, url);
        }

    }
}