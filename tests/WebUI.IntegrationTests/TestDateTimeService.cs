using MyCleanApp.Application.Common.Interfaces;
using System;

namespace MyCleanApp.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
