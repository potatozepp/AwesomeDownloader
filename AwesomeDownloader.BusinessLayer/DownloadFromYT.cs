using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using AwesomeDownloader.Models;

namespace AwesomeDownloader.BusinessLayer {
    public class DownloadFromYT {
        public async void DownloadMP3Async(string path, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=hhxjSaF0Ek4
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);


            var streamInfo = test.GetAudioOnlyStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}\\{video.Title}.mp3");
        }

        public async void DownloadMP4Async(string path, string url) {
            var youtube = new YoutubeClient();

            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);

            var streamInfo = test.GetMuxedStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}\\{video.Title}.mp4");
        }

        public async void DownloadMP3AndMP4Async(string path, string url) {
            DownloadMP3Async(path, url);
            DownloadMP4Async(path, url);
        }

    }
}