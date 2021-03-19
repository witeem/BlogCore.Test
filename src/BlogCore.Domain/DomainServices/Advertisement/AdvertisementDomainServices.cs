using BlogCore.Domain.DomainServices.Entitys;
using BlogCore.Extended;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public class AdvertisementDomainServices : IAdvertisementDomainServices
    {
        private readonly ILogger<AdvertisementDomainServices> _logger;
        private static List<AdverUserEntity> _users = new List<AdverUserEntity>() {
            new AdverUserEntity {  Id=1, Name="alice", Password="alice", Email="alice@gmail.com", PhoneNumber="18800000001", Birthday=DateTime.Now },
            new AdverUserEntity {  Id=1, Name="bob", Password="bob", Email="bob@gmail.com", PhoneNumber="18800000002", Birthday=DateTime.Now.AddDays(1)}
        };

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

        public async Task<AdverUserEntity> FindUser(string userName, string password)
        {
            await Task.Delay(100);
            return _users.FirstOrDefault(_ => _.Name == userName && _.Password == password);
        }
    }
}
