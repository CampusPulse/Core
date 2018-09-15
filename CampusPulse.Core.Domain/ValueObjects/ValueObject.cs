using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusPulse.Core
{
    public abstract class ValueObject<T>
    where T : ValueObject<T>
    {

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            T other = obj as T;
            if (other == null || other.GetType() != GetType())
            {
                return false;
            }
            return EqualsCore(other);


        }

        protected virtual bool EqualsCore(T other)
        {

            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }




        protected static bool EqualOperator(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject<T> left, ValueObject<T> right)
        {
            return !(EqualOperator(left, right));
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }
        // Other utilility methods
    }
}

