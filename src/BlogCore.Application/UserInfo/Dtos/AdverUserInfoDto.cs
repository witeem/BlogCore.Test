using AutoMapper;
using BlogCore.Core.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.Application.UserInfo.Dtos
{
    public class AdverUserInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RoleCodes { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> RoleCodeList
        {
            get
            {
                if (!string.IsNullOrEmpty(RoleCodes))
                {
                    return RoleCodes.Split(',').ToList();
                }
                return new List<string>();
            }
        }
    }
}
