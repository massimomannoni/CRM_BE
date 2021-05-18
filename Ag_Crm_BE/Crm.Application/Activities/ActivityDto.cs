using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Application.Activities
{
    public class ActivityDto
    {
        public Guid Id { get; set; }

        public string SectorType { get; set; }

        public string ActivityType { get; set; }

        public bool Value { get; set; }
    }
}
