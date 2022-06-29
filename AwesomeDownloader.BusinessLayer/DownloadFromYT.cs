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

            var title = video.Title.Replace("|", "_").Replace(":", "_");

            var streamInfo = test.GetAudioOnlyStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{settings.DownloadFolder}\\{title}.mp3");

            
        }

        static public async void DownloadMP4Async(DownloadSettings settings, string url) {
            var youtube = new YoutubeClient();

            // You can specify both video ID or URL https://www.youtube.com/watch?v=DjbVh4i0DIU
            var video = await youtube.Videos.GetAsync(url);
            var titleBad = video.Title;
            var title = titleBad.Replace("|", "_");
            var test = await youtube.Videos.Streams.GetManifestAsync(video.Id);
            var streamInfoVid = test.GetMuxedStreams().GetWithHighestBitrate();
            await youtube.Videos.Streams.DownloadAsync(streamInfoVid, $"{settings.DownloadFolder}\\{title}.mp4");





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
            var titleBad = video.Title;
            var title = titleBad.Replace("|", "_");

            await youtube.Videos.Streams.DownloadAsync(streamInfoAud, $"{settings.DownloadFolder}\\{title}.mp3");
            await youtube.Videos.Streams.DownloadAsync(streamInfoVid, $"{settings.DownloadFolder}\\{title}.mp4");
        }


    }
}