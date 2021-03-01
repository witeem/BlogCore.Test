using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public interface IAdvertisementDomainServices
    {
        Task<int> Sum(int i, int j);
    }
}
