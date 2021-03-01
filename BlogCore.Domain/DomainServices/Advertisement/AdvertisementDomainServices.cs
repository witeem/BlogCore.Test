using BlogCore.Extended;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public class AdvertisementDomainServices : IAdvertisementDomainServices
    {
        private readonly ILogger<AdvertisementDomainServices> _logger;

        public AdvertisementDomainServices(ILogger<AdvertisementDomainServices> logger)
        {
            _logger = logger;
        }

        public async Task<int> Sum(int i, int j)
        {
            await Task.Delay(100);
            string secret = ConfigManagerConf.GetValue("Audience:Secret");
            Console.WriteLine($"Default:{secret}");
            return i * j;
        }
    }
}
