﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application.UserInfo.Dtos
{
    public class AdverUserInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> RoleCodes { get; set; }
    }
}
