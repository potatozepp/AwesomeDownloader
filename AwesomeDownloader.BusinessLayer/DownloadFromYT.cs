using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using AwesomeDownloader.Models;

namespace AwesomeDownloader.BusinessLayer {
    static public class DownloadFromYT {
        static public async void DownloadMP3Async(string path, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);


            var streamInfo = test.GetAudioOnlyStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}\\{video.Title}.mp3");
        }

        static public async void DownloadMP4Async(string path, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);


            var streamInfo = test.GetMuxedStreams().GetWithHighestVideoQuality();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}\\{video.Title}.mp4");
        }

        static public async void DownloadBothAsync(string path, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);

            var streamInfoAud = test.GetAudioOnlyStreams().GetWithHighestBitrate();
            var streamInfoVid = test.GetMuxedStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfoAud, $"{path}\\{video.Title}.mp3");
            await youtube.Videos.Streams.DownloadAsync(streamInfoVid, $"{path}\\{video.Title}.mp4");
        }


    }
}