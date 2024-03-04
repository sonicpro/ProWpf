using System;
using System.Globalization;

namespace SampleApplicationModel
{
    public struct Money
    {
        public Money(decimal amount)
        {
            Amount = amount;
            Currency = CultureInfo.CurrentCulture;
        }

        public Money(decimal amount, CultureInfo currency) : this(amount)
        {
            Currency = currency;
        }

        #region Properties
        public decimal Amount { get; private set; }

        public CultureInfo Currency { get; }

        public static Money Undefined => new Money(-1, null);
        #endregion

        public static Money operator+ (Money lhs, Money rhs)
        {
            if (lhs.Currency.LCID != rhs.Currency.LCID)
            {
                return Undefined;
            }

            return new Money(lhs.Amount + rhs.Amount);
        }

        public static Money operator- (Money lhs, Money rhs)
        {
            if (lhs.Currency.LCID != rhs.Currency.LCID)
            {
                return Undefined;
            }

            return new Money(lhs.Amount - rhs.Amount);
        }

        public static implicit operator Money(decimal amount)
        {
            return new Money(amount);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Money))
            {
                return false;
            }

            var other = (Money) obj;
            return (Currency == null && other.Currency == null || Currency?.LCID == other.Currency?.LCID) &&
                   Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode() ^ Currency.GetHashCode();
        }

        public static bool operator== (Money lhs, Money rhs)
        {
            if (lhs.Currency != null && rhs.Currency != null && lhs.Currency.LCID != rhs.Currency.LCID)
            {
                throw new InvalidOperationException($"Cannot compare \"{lhs.Currency}\" money with " +
                                                    $"\"{rhs.Currency}\" money.");
            }

            return rhs.Equals(lhs);
        }

        public static bool operator!= (Money lhs, Money rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator> (Money lhs, Money rhs)
        {
            if (lhs.Currency.LCID != rhs.Currency.LCID)
            {
                throw new InvalidOperationException($"Cannot compare \"{lhs.Currency}\" money with " +
                                                    $"\"{rhs.Currency}\" money.");
            }

            return lhs.Amount > rhs.Amount;
        }

        public static bool operator <(Money lhs, Money rhs)
        {
            return !(lhs > rhs);
        }

        public static Money Zero => new Money(0m);
    }
}
