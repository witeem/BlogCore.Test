using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models.UserInfo
{
    public class AdverUserInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> RoleCodes { get; set; }
    }
}
