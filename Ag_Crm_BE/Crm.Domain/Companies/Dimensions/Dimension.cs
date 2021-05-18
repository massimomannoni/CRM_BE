using Crm.Domain.SeedWork;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Domain.Companies.Dimensions
{
    public class Dimension : Entity
    {
        internal DimensionId Id;

        private string _dimensionType;

        [Column(TypeName = "decimal(18,4)")]
        private decimal _fee;

        private DateTime? _expireDate;

        private bool _isRemoved;

        private Dimension() {
            _isRemoved = false;
        }

        private Dimension(string dimensionType, decimal fee)
        {
            Id = new DimensionId(Guid.NewGuid());
            _dimensionType = dimensionType;
            _fee = fee;
            _expireDate = null;
            _isRemoved = false;
        }

        internal static Dimension CreateNew(string dimensionType, decimal fee)
        {
            return new Dimension(dimensionType, fee);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
