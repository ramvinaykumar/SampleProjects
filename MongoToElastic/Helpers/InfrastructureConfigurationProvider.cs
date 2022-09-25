using Microsoft.Extensions.Configuration.Json;
using Microsoft.IO;
//using MongoToElastic.Models.Enums;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
//using Microsoft.Extensions.Configuration;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;


namespace MongoToElastic.Helpers
{
    internal class InfrastructureConfigurationProvider : JsonStreamConfigurationProvider
    {
        private readonly RecyclableMemoryStreamManager _streamManager;
        private Stream currentStream;
        private System.Timers.Timer _reloadTimer;

        public IConfiguration Configuration { get; set; }
        public IDictionary<string, string> nConfiguration { get; set; }
        public bool UseSecure { get; set; }

        public InfrastructureConfigurationProvider(InfrastructureConfigurationSource source) : base((JsonStreamConfigurationSource)source)
          => _streamManager = new RecyclableMemoryStreamManager();

        private void StartTimer()
        {
            double num = Configuration.GetValue<double>("SolutionsConfigurationReloadTtl", 60.0);
            _reloadTimer = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = TimeSpan.FromMinutes(num).TotalMilliseconds
            };
            _reloadTimer.Elapsed += (ElapsedEventHandler)((s, e) => GetData());
            _reloadTimer.Start();
        }

        public override void Load()
        {
            GetData();
            StartTimer();
        }

        public void GetData(string INFRASTRUCTURE_API = null)
        {
            string baseUri = string.IsNullOrEmpty(INFRASTRUCTURE_API) ? Configuration.GetValue<string>("INFRASTRUCTURE_API") : INFRASTRUCTURE_API;
            Stream stream = (Stream)null;
            try
            {
                stream = GetFromApi(baseUri).GetAwaiter().GetResult();
                if (InfrastructureConfigurationProvider.HasDelta(stream, currentStream))
                {
                    stream.Position = 0L;
                    currentStream = (Stream)_streamManager.GetStream();
                    stream.CopyTo(currentStream);
                    stream.Position = 0L;
                    Load(stream);
                    Decrypt();
                    OnReload();
                }
            }
            finally
            {
                stream?.Dispose();
            }
            nConfiguration = Data;
        }

        private void Decrypt()
        {
            foreach (KeyValuePair<string, string> keyValuePair in Data.ToList<KeyValuePair<string, string>>())
            {
                if (keyValuePair.Value != null)
                {
                    try
                    {
                        Data[keyValuePair.Key] = Crypto.SimpleDecrypt(keyValuePair.Value);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static bool HasDelta(Stream newStream, Stream currentStream)
        {
            if (newStream == null)
                return false;
            if (currentStream == null)
                return true;
            if (newStream == currentStream)
                return false;
            if (newStream.Length != currentStream.Length)
                return true;
            currentStream.Position = 0L;
            for (int index = 0; (long)index < newStream.Length; ++index)
            {
                if (newStream.ReadByte().CompareTo(currentStream.ReadByte()) != 0)
                    return true;
            }
            return false;
        }

        private async Task<Stream> GetFromApi(string baseUri)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>)((message, cert, chain, errors) => true)
            };
            string components = new UriBuilder(baseUri)
            {
                Scheme = "https",
                Path = (UseSecure ? "api/secure" : "api/legacy")
            }.Uri.GetComponents(UriComponents.PathAndQuery | UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Fragment, UriFormat.UriEscaped);
            Stream stream;
            using (HttpClient client = new HttpClient((HttpMessageHandler)httpClientHandler))
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(components).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
                stream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            }
            return stream;
        }
    }

    internal class InfrastructureConfigurationSource : JsonStreamConfigurationSource
    {
        public Action<InfrastructureConfigurationProvider> Setup { get; set; }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            InfrastructureConfigurationProvider configurationProvider = new InfrastructureConfigurationProvider(this);
            Action<InfrastructureConfigurationProvider> setup = Setup;
            if (setup != null)
                setup(configurationProvider);
            return (IConfigurationProvider)configurationProvider;
        }
    }
}
