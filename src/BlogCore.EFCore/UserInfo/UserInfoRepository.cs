using BlogCore.Core.UserInfo;
using BlogCore.Core.UserInfo.Respoitorys;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.EFCore.UserInfo
{
    public class UserInfoRepository : RepositoryBase<AdverUserInfo,long>, IUserInfoRepository
    {
        public UserInfoRepository(DefaultContext defaultContext):base(defaultContext)
        { 
            
        }
    }
}
