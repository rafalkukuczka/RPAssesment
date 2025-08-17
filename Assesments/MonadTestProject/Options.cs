using System;

namespace Monad
{
    
    public static class Option
    {
        public static Option<T> Empty<T>() => default; 
        public static Option<T> FromValue<T>(T value) => Option<T>.FromValue(value);
    }

    public readonly struct Option<T> : IEquatable<Option<T>>
    {
        private readonly T _value;
        public bool HasValue { get; }

        private Option(T value, bool hasValue)
        {
            _value = value;
            HasValue = hasValue;
        }

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("Option has no value");
                return _value;
            }
        }

        public static Option<T> FromValue(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            return new Option<T>(value, true);
        }

        public Option<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (HasValue)
                return Option<TResult>.FromValue(selector(_value));
            return default; 
        }

        public T ValueOr(Func<T> elseFactory)
        {
            if (HasValue)
                return _value;
            return elseFactory();
        }

        public bool Equals(Option<T> other)
        {
            if (HasValue != other.HasValue) return false;
            if (!HasValue) return true; // obie puste
            return Equals(_value, other._value);
        }

        public override bool Equals(object obj) => obj is Option<T> other && Equals(other);
        public override int GetHashCode() => HasValue ? _value?.GetHashCode() ?? 0 : 0;

        public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);
        public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);

        public override string ToString() => HasValue ? $"Some({_value})" : "None";
    }
}
