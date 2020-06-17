using Grundfos.GiC.Shared.Defense;
using Grundfos.GiC.Shared.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableStorage.ValueObjects
{
    public class InvertedTicksRowkey : ValueObject
    {
        [JsonConstructor]
        private InvertedTicksRowkey(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public DateTimeOffset ToDateTimeOffset()
        {
            if (long.TryParse(Value.TrimStart('0'), out var ticks))
                return new DateTimeOffset(DateTimeOffset.MaxValue.Ticks - ticks, TimeSpan.Zero);

            throw new InvalidOperationException($"The value: {Value} is not a valid DateTimeOffset");
        }

        public static Result<InvertedTicksRowkey> Create(DateTimeOffset when)
        {
            if (when == DateTimeOffset.MinValue)
                return Result.Fail<InvertedTicksRowkey>("when must have a valid value");

            if (when == DateTimeOffset.MaxValue)
                return Result.Fail<InvertedTicksRowkey>("when must have a valid value");

            string invertedTicks = string.Format("{0:D19}", DateTimeOffset.MaxValue.Ticks - when.UtcTicks);
            invertedTicks = invertedTicks.PadLeft(24, '0');
            return Result.Ok(new InvertedTicksRowkey(invertedTicks));
        }

        public static Result<InvertedTicksRowkey> Create(string invertedTicksRowkey)
        {
            if (invertedTicksRowkey.Length != 24)
                return Result.Fail<InvertedTicksRowkey>("ticks do not seem to be valid");

            if (long.TryParse(invertedTicksRowkey.TrimStart('0'), out var ticks))
                return Create(new DateTimeOffset(DateTimeOffset.MaxValue.Ticks - ticks, TimeSpan.Zero));

            return Result.Fail<InvertedTicksRowkey>("ticks do not seem to be a valid date");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(InvertedTicksRowkey ticks)
        {
            if (ticks == null)
                return default(string);

            return ticks.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
