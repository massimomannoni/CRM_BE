using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Api.Services
{
    public static class ApiBase
    {
        internal const string Version = "v1.0";

        public const string Root = Version;

        public const string Companies = Version + "/companies";

        public const string AddressTypes = Version +  "/addressTypes";

        public const string ContactTypes = Version + "/contactTypes";

        public const string ActivityTypes = Version + "/activityTypes";

        public const string SectorTypes = Version + "/sectorTypes";

        public const string DimensionTypes = Version + "/dimensionTypes";

    }
}
