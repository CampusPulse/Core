using System.Collections.Generic;

namespace CampusPulse.Core.ValueObjects
{
    public class Money : ValueObject<Money>
    {
        private readonly double value;
        private readonly string currencyType;

        public Money(double value, string currencyType)
        {
            this.value = value;
            this.currencyType = currencyType;
        }

        public string CurrencyType => currencyType;

        public double Value => value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new List<object>() { this.currencyType, this.value }.AsReadOnly();
        }
    }
}
