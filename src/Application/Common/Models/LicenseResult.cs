using MyCleanApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCleanApp.Application.Common.Models
{
    public class LicenseResult
    {
        public LicenseStatus Status { get; set; }
        public string MyKey { get; set; }
        public DateTime ValidTill { get; set; }
        public string ProductID { get; set; }
        public string Error { get; set; }
    }
}
