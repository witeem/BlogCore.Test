using AutoMapper;
using BlogCore.Domain.DomainServices.Advertisement;
using BlogCore.Domain.DomainServices.Entitys;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlogCore.Application.Advertisement
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly ILogger<AdvertisementService> _logger;
        private readonly IMapper _mapper;
        private readonly IAdvertisementDomainServices  _advertisementDomainServices;
        public AdvertisementService(ILogger<AdvertisementService> logger, IMapper mapper, IAdvertisementDomainServices advertisementDomainServices)
        {
            _logger = logger;
            _mapper = mapper;
            _advertisementDomainServices = advertisementDomainServices;
        }
        public Task<AdverUserEntity> FindUser(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Sum(int i, int j)
        {
            throw new System.NotImplementedException();
        }

        public void TestInterceptor()
        {
            throw new System.NotImplementedException();
        }
    }
}