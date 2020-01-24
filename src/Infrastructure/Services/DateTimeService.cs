using MyCleanApp.Application.Common.Interfaces;
using System;

namespace MyCleanApp.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
