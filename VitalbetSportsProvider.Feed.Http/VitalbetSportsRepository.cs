namespace VitalbetSportsProvider.Feed.Http
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using VitalbetSportsProvider.Feed.Interfaces;
    using VitalbetSportsProvider.Models.Xml;

    public class VitalbetSportsRepository : IVitalbetSportsRepository
    {
        private const string Url = "http://vitalbet.net/sportxml";

        public async Task<XmlSports> RequestSportsAsync()
        {
            var request = WebRequest.CreateHttp(Url);

            request.KeepAlive = false;
            request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
            request.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
            request.Headers.Add("Origin", Url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
            request.Referer = Url;
            request.Method = "GET";
            request.ContentType = "text/html";
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            
            using (var response = await request.GetResponseAsync())
            using (var responseStream = response.GetResponseStream())
            using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
            {
                return (XmlSports)new XmlSerializer(typeof(XmlSports)).Deserialize(streamReader);      
            }
        }
    }
}
