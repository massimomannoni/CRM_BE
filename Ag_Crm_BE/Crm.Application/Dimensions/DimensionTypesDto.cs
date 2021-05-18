using System;

namespace Crm.Application.Dimensions
{
    public class DimensionTypesDto
    {
        public Guid Id { get; set; }

        public string ContractType { get; set; }

        public string Name { get; set; }

        public decimal Fee { get; set; }
    }
}
