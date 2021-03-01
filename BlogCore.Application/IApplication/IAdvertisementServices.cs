using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Application.IApplication
{
    public interface IAdvertisementServices
    {
        Task<int> Sum(int i, int j);
    }
}
