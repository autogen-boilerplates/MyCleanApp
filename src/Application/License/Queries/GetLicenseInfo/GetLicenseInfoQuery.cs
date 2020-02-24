using MediatR;
using MyCleanApp.Application.Common.Interfaces;
using MyCleanApp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCleanApp.Application.License.Queries.GetLicenseInfo
{
    public class GetLicenseInfoQuery : IRequest<LicenseResult>
    {

        public string enProductID { get; set; }


        public class GetLicenseInfoQueryHandler : IRequestHandler<GetLicenseInfoQuery, LicenseResult>
        {
            private readonly ILicensingService _licensingService;

            public GetLicenseInfoQueryHandler(ILicensingService licensingService)
            {
                _licensingService = licensingService;
            }

            public Task<LicenseResult> Handle(GetLicenseInfoQuery request, CancellationToken cancellationToken)
            {
                return _licensingService.GetLicense(request.enProductID);
            }
        }

    }
}
