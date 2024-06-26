﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.ConfigModels
{
    public class JwtConfig
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string ExpirationTime { get; set; } = string.Empty;
        public string RefreshTokenExpiration { get; set; } = string.Empty;
    }
}
