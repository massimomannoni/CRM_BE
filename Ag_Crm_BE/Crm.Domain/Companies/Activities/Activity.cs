using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Companies.Activities
{
    public class Activity : Entity
    {
        internal ActivityId Id;

        private string _sectorType;

        private string _activityType;

        private bool _value;

        private bool _isRemoved;

        private Activity() {
            _isRemoved = false;
        }

        private Activity(string sectorType, string activityType, bool value)
        {
            Id = new ActivityId(Guid.NewGuid());
            _sectorType = sectorType;
            _activityType = activityType;
            _value = value;
            _isRemoved = false;
        }

        internal static Activity CreateNew(string sectorType, string activityType, bool value)
        {
            return new Activity(sectorType, activityType, value);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
