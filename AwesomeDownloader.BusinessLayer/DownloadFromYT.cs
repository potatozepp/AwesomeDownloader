using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using AwesomeDownloader.Models;
using System.Text.RegularExpressions;
using YoutubeExplode.Converter;
using System.Windows;

namespace AwesomeDownloader.BusinessLayer {
    static public class DownloadFromYT {
        static public async void DownloadMP3Async(DownloadSettings settings, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);


            var streamInfo = test.GetAudioOnlyStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{settings.DownloadFolder}\\{video.Title}.mp3");
        }

        static public async void DownloadMP4Async(DownloadSettings settings, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);

            var bitrateString = int.Parse(Regex.Match(settings.Bitrate, @"\d+").Value);
            Bitrate bitrate = new Bitrate(bitrateString * 1024);


            // Select streams (1080p60 / highest bitrate audio)
            var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
            var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == settings.VideoQuality);
            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
            string path = $"{ settings.DownloadFolder }{ video.Title}.mp4";
            await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(path).Build());


            Console.WriteLine("download finished");            
            
            // Download and process them into one file
            //await youtube.Videos.Streams.DownloadAsync(streamInfos, new ConversionRequestBuilder("video.mp4").Build());
        }

        static public async void DownloadBothAsync(DownloadSettings settings, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);

            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);

            var streamInfoAud = test.GetAudioOnlyStreams().GetWithHighestBitrate();
            var streamInfoVid = test.GetMuxedStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfoAud, $"{settings.DownloadFolder}\\{video.Title}.mp3");
            await youtube.Videos.Streams.DownloadAsync(streamInfoVid, $"{settings.DownloadFolder}\\{video.Title}.mp4");
        }


    }
}