// Copyright (c) 2022 Joseph J. Short
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using Jodo.Numerics;
using Jodo.Primitives;
using Jodo.Primitives.Compatibility;

namespace Jodo.CheckedNumerics
{
    /// <summary>
    /// Represents a 32-bit signed integer that cannot overflow.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly struct Int32C : INumericExtended<Int32C>
    {
        public static readonly Int32C MaxValue = new Int32C(int.MaxValue);
        public static readonly Int32C MinValue = new Int32C(int.MinValue);

        private readonly int _value;

        private Int32C(int value)
        {
            _value = value;
        }

        private Int32C(SerializationInfo info, StreamingContext context) : this(info.GetInt32(nameof(Int32C))) { }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue(nameof(Int32C), _value);

        public int CompareTo(Int32C other) => _value.CompareTo(other._value);
        public int CompareTo(object? obj) => obj is Int32C other ? CompareTo(other) : 1;
        public bool Equals(Int32C other) => _value == other._value;
        public override bool Equals(object? obj) => obj is Int32C other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();
        public string ToString(IFormatProvider formatProvider) => _value.ToString(formatProvider);
        public string ToString(string format) => _value.ToString(format);
        public string ToString(string? format, IFormatProvider? formatProvider) => _value.ToString(format, formatProvider);

        public static bool TryParse(string s, IFormatProvider? provider, out Int32C result) => TryHelper.Run(() => Parse(s, provider), out result);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider? provider, out Int32C result) => TryHelper.Run(() => Parse(s, style, provider), out result);
        public static bool TryParse(string s, NumberStyles style, out Int32C result) => TryHelper.Run(() => Parse(s, style), out result);
        public static bool TryParse(string s, out Int32C result) => TryHelper.Run(() => Parse(s), out result);
        public static Int32C Parse(string s) => int.Parse(s);
        public static Int32C Parse(string s, IFormatProvider? provider) => int.Parse(s, provider);
        public static Int32C Parse(string s, NumberStyles style) => int.Parse(s, style);
        public static Int32C Parse(string s, NumberStyles style, IFormatProvider? provider) => int.Parse(s, style, provider);

        [CLSCompliant(false)] public static explicit operator Int32C(uint value) => new Int32C(NumericConvert.ToInt32(value, Conversion.CastClamp));
        [CLSCompliant(false)] public static explicit operator Int32C(ulong value) => new Int32C(NumericConvert.ToInt32(value, Conversion.CastClamp));
        [CLSCompliant(false)] public static implicit operator Int32C(sbyte value) => new Int32C(value);
        [CLSCompliant(false)] public static implicit operator Int32C(ushort value) => new Int32C(value);
        public static explicit operator Int32C(decimal value) => new Int32C(NumericConvert.ToInt32(value, Conversion.CastClamp));
        public static explicit operator Int32C(double value) => new Int32C(NumericConvert.ToInt32(value, Conversion.CastClamp));
        public static explicit operator Int32C(float value) => new Int32C(NumericConvert.ToInt32(value, Conversion.CastClamp));
        public static explicit operator Int32C(long value) => new Int32C(NumericConvert.ToInt32(value, Conversion.CastClamp));
        public static implicit operator Int32C(byte value) => new Int32C(value);
        public static implicit operator Int32C(int value) => new Int32C(value);
        public static implicit operator Int32C(short value) => new Int32C(value);

        [CLSCompliant(false)] public static explicit operator sbyte(Int32C value) => NumericConvert.ToSByte(value._value, Conversion.CastClamp);
        [CLSCompliant(false)] public static explicit operator uint(Int32C value) => NumericConvert.ToUInt32(value._value, Conversion.CastClamp);
        [CLSCompliant(false)] public static explicit operator ulong(Int32C value) => NumericConvert.ToUInt64(value._value, Conversion.CastClamp);
        [CLSCompliant(false)] public static explicit operator ushort(Int32C value) => NumericConvert.ToUInt16(value._value, Conversion.CastClamp);
        public static explicit operator byte(Int32C value) => NumericConvert.ToByte(value._value, Conversion.CastClamp);
        public static explicit operator short(Int32C value) => NumericConvert.ToInt16(value._value, Conversion.CastClamp);
        public static implicit operator decimal(Int32C value) => value._value;
        public static implicit operator double(Int32C value) => value._value;
        public static implicit operator float(Int32C value) => value._value;
        public static implicit operator int(Int32C value) => value._value;
        public static implicit operator long(Int32C value) => value._value;

        public static bool operator !=(Int32C left, Int32C right) => left._value != right._value;
        public static bool operator <(Int32C left, Int32C right) => left._value < right._value;
        public static bool operator <=(Int32C left, Int32C right) => left._value <= right._value;
        public static bool operator ==(Int32C left, Int32C right) => left._value == right._value;
        public static bool operator >(Int32C left, Int32C right) => left._value > right._value;
        public static bool operator >=(Int32C left, Int32C right) => left._value >= right._value;
        public static Int32C operator %(Int32C left, Int32C right) => CheckedArithmetic.Remainder(left._value, right._value);
        public static Int32C operator &(Int32C left, Int32C right) => left._value & right._value;
        public static Int32C operator -(Int32C left, Int32C right) => CheckedArithmetic.Subtract(left._value, right._value);
        public static Int32C operator --(Int32C value) => value - 1;
        public static Int32C operator -(Int32C value) => -value._value;
        public static Int32C operator *(Int32C left, Int32C right) => CheckedArithmetic.Multiply(left._value, right._value);
        public static Int32C operator /(Int32C left, Int32C right) => CheckedArithmetic.Divide(left._value, right._value);
        public static Int32C operator ^(Int32C left, Int32C right) => left._value ^ right._value;
        public static Int32C operator |(Int32C left, Int32C right) => left._value | right._value;
        public static Int32C operator ~(Int32C value) => ~value._value;
        public static Int32C operator +(Int32C left, Int32C right) => CheckedArithmetic.Add(left._value, right._value);
        public static Int32C operator +(Int32C value) => value;
        public static Int32C operator ++(Int32C value) => value + 1;
        public static Int32C operator <<(Int32C left, int right) => left._value << right;
        public static Int32C operator >>(Int32C left, int right) => left._value >> right;

        TypeCode IConvertible.GetTypeCode() => _value.GetTypeCode();
        bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)_value).ToBoolean(provider);
        char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)_value).ToChar(provider);
        sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)_value).ToSByte(provider);
        byte IConvertible.ToByte(IFormatProvider provider) => ((IConvertible)_value).ToByte(provider);
        short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)_value).ToInt16(provider);
        ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)_value).ToUInt16(provider);
        int IConvertible.ToInt32(IFormatProvider provider) => ((IConvertible)_value).ToInt32(provider);
        uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)_value).ToUInt32(provider);
        long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)_value).ToInt64(provider);
        ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)_value).ToUInt64(provider);
        float IConvertible.ToSingle(IFormatProvider provider) => ((IConvertible)_value).ToSingle(provider);
        double IConvertible.ToDouble(IFormatProvider provider) => ((IConvertible)_value).ToDouble(provider);
        decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)_value).ToDecimal(provider);
        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)_value).ToDateTime(provider);
        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)_value).ToType(conversionType, provider);

        bool INumeric<Int32C>.IsGreaterThan(Int32C value) => this > value;
        bool INumeric<Int32C>.IsGreaterThanOrEqualTo(Int32C value) => this >= value;
        bool INumeric<Int32C>.IsLessThan(Int32C value) => this < value;
        bool INumeric<Int32C>.IsLessThanOrEqualTo(Int32C value) => this <= value;
        Int32C INumeric<Int32C>.Add(Int32C value) => this + value;
        Int32C INumeric<Int32C>.BitwiseComplement() => ~this;
        Int32C INumeric<Int32C>.Divide(Int32C value) => this / value;
        Int32C INumeric<Int32C>.LeftShift(int count) => this << count;
        Int32C INumeric<Int32C>.LogicalAnd(Int32C value) => this & value;
        Int32C INumeric<Int32C>.LogicalExclusiveOr(Int32C value) => this ^ value;
        Int32C INumeric<Int32C>.LogicalOr(Int32C value) => this | value;
        Int32C INumeric<Int32C>.Multiply(Int32C value) => this * value;
        Int32C INumeric<Int32C>.Negative() => -this;
        Int32C INumeric<Int32C>.Positive() => +this;
        Int32C INumeric<Int32C>.Remainder(Int32C value) => this % value;
        Int32C INumeric<Int32C>.RightShift(int count) => this >> count;
        Int32C INumeric<Int32C>.Subtract(Int32C value) => this - value;

        IBitConverter<Int32C> IProvider<IBitConverter<Int32C>>.GetInstance() => Utilities.Instance;
        IConvert<Int32C> IProvider<IConvert<Int32C>>.GetInstance() => Utilities.Instance;
        IConvertExtended<Int32C> IProvider<IConvertExtended<Int32C>>.GetInstance() => Utilities.Instance;
        IMath<Int32C> IProvider<IMath<Int32C>>.GetInstance() => Utilities.Instance;
        INumericStatic<Int32C> IProvider<INumericStatic<Int32C>>.GetInstance() => Utilities.Instance;
        IRandom<Int32C> IProvider<IRandom<Int32C>>.GetInstance() => Utilities.Instance;
        IStringParser<Int32C> IProvider<IStringParser<Int32C>>.GetInstance() => Utilities.Instance;

        private sealed class Utilities :
            IBitConverter<Int32C>,
            IConvert<Int32C>,
            IConvertExtended<Int32C>,
            IMath<Int32C>,
            INumericStatic<Int32C>,
            IRandom<Int32C>,
            IStringParser<Int32C>
        {
            public static readonly Utilities Instance = new Utilities();

            bool INumericStatic<Int32C>.HasFloatingPoint => false;
            bool INumericStatic<Int32C>.HasInfinity => false;
            bool INumericStatic<Int32C>.HasNaN => false;
            bool INumericStatic<Int32C>.IsFinite(Int32C x) => true;
            bool INumericStatic<Int32C>.IsInfinity(Int32C x) => false;
            bool INumericStatic<Int32C>.IsNaN(Int32C x) => false;
            bool INumericStatic<Int32C>.IsNegative(Int32C x) => x._value < 0;
            bool INumericStatic<Int32C>.IsNegativeInfinity(Int32C x) => false;
            bool INumericStatic<Int32C>.IsNormal(Int32C x) => false;
            bool INumericStatic<Int32C>.IsPositiveInfinity(Int32C x) => false;
            bool INumericStatic<Int32C>.IsReal => false;
            bool INumericStatic<Int32C>.IsSigned => true;
            bool INumericStatic<Int32C>.IsSubnormal(Int32C x) => false;
            Int32C INumericStatic<Int32C>.Epsilon => 1;
            Int32C INumericStatic<Int32C>.MaxUnit => 1;
            Int32C INumericStatic<Int32C>.MaxValue => MaxValue;
            Int32C INumericStatic<Int32C>.MinUnit => -1;
            Int32C INumericStatic<Int32C>.MinValue => MinValue;
            Int32C INumericStatic<Int32C>.One => 1;
            Int32C INumericStatic<Int32C>.Ten => 10;
            Int32C INumericStatic<Int32C>.Two => 2;
            Int32C INumericStatic<Int32C>.Zero => 0;

            Int32C IMath<Int32C>.Abs(Int32C value) => Math.Abs(value._value);
            Int32C IMath<Int32C>.Acos(Int32C x) => (Int32C)Math.Acos(x._value);
            Int32C IMath<Int32C>.Acosh(Int32C x) => (Int32C)MathCompat.Acosh(x._value);
            Int32C IMath<Int32C>.Asin(Int32C x) => (Int32C)Math.Asin(x._value);
            Int32C IMath<Int32C>.Asinh(Int32C x) => (Int32C)MathCompat.Asinh(x._value);
            Int32C IMath<Int32C>.Atan(Int32C x) => (Int32C)Math.Atan(x._value);
            Int32C IMath<Int32C>.Atan2(Int32C x, Int32C y) => (Int32C)Math.Atan2(x._value, y._value);
            Int32C IMath<Int32C>.Atanh(Int32C x) => (Int32C)MathCompat.Atanh(x._value);
            Int32C IMath<Int32C>.Cbrt(Int32C x) => (Int32C)MathCompat.Cbrt(x._value);
            Int32C IMath<Int32C>.Ceiling(Int32C x) => x;
            Int32C IMath<Int32C>.Clamp(Int32C x, Int32C bound1, Int32C bound2) => bound1 > bound2 ? Math.Min(bound1._value, Math.Max(bound2._value, x._value)) : Math.Min(bound2._value, Math.Max(bound1._value, x._value));
            Int32C IMath<Int32C>.Cos(Int32C x) => (Int32C)Math.Cos(x._value);
            Int32C IMath<Int32C>.Cosh(Int32C x) => (Int32C)Math.Cosh(x._value);
            Int32C IMath<Int32C>.DegreesToRadians(Int32C x) => (Int32C)CheckedArithmetic.Multiply(x, NumericUtilities.RadiansPerDegree);
            Int32C IMath<Int32C>.E { get; } = 2;
            Int32C IMath<Int32C>.Exp(Int32C x) => (Int32C)Math.Exp(x._value);
            Int32C IMath<Int32C>.Floor(Int32C x) => x;
            Int32C IMath<Int32C>.IEEERemainder(Int32C x, Int32C y) => (Int32C)Math.IEEERemainder(x._value, y._value);
            Int32C IMath<Int32C>.Log(Int32C x) => (Int32C)Math.Log(x._value);
            Int32C IMath<Int32C>.Log(Int32C x, Int32C y) => (Int32C)Math.Log(x._value, y._value);
            Int32C IMath<Int32C>.Log10(Int32C x) => (Int32C)Math.Log10(x._value);
            Int32C IMath<Int32C>.Max(Int32C x, Int32C y) => Math.Max(x._value, y._value);
            Int32C IMath<Int32C>.Min(Int32C x, Int32C y) => Math.Min(x._value, y._value);
            Int32C IMath<Int32C>.PI { get; } = 3;
            Int32C IMath<Int32C>.Pow(Int32C x, Int32C y) => CheckedArithmetic.Pow(x._value, y._value);
            Int32C IMath<Int32C>.RadiansToDegrees(Int32C x) => (Int32C)CheckedArithmetic.Multiply(x, NumericUtilities.DegreesPerRadian);
            Int32C IMath<Int32C>.Round(Int32C x) => x;
            Int32C IMath<Int32C>.Round(Int32C x, int digits) => x;
            Int32C IMath<Int32C>.Round(Int32C x, int digits, MidpointRounding mode) => x;
            Int32C IMath<Int32C>.Round(Int32C x, MidpointRounding mode) => x;
            Int32C IMath<Int32C>.Sin(Int32C x) => (Int32C)Math.Sin(x._value);
            Int32C IMath<Int32C>.Sinh(Int32C x) => (Int32C)Math.Sinh(x._value);
            Int32C IMath<Int32C>.Sqrt(Int32C x) => (Int32C)Math.Sqrt(x._value);
            Int32C IMath<Int32C>.Tan(Int32C x) => (Int32C)Math.Tan(x._value);
            Int32C IMath<Int32C>.Tanh(Int32C x) => (Int32C)Math.Tanh(x._value);
            Int32C IMath<Int32C>.Tau { get; } = 6;
            Int32C IMath<Int32C>.Truncate(Int32C x) => x;
            int IMath<Int32C>.Sign(Int32C x) => Math.Sign(x._value);

            Int32C IBitConverter<Int32C>.Read(IReader<byte> stream) => BitConverter.ToInt32(stream.Read(sizeof(int)), 0);
            void IBitConverter<Int32C>.Write(Int32C value, IWriter<byte> stream) => stream.Write(BitConverter.GetBytes(value._value));

            Int32C IRandom<Int32C>.Next(Random random) => random.NextInt32();
            Int32C IRandom<Int32C>.Next(Random random, Int32C bound1, Int32C bound2) => random.NextInt32(bound1._value, bound2._value);

            bool IConvert<Int32C>.ToBoolean(Int32C value) => value._value != 0;
            byte IConvert<Int32C>.ToByte(Int32C value, Conversion mode) => NumericConvert.ToByte(value._value, mode.Clamped());
            decimal IConvert<Int32C>.ToDecimal(Int32C value, Conversion mode) => NumericConvert.ToDecimal(value._value, mode.Clamped());
            double IConvert<Int32C>.ToDouble(Int32C value, Conversion mode) => NumericConvert.ToDouble(value._value, mode.Clamped());
            float IConvert<Int32C>.ToSingle(Int32C value, Conversion mode) => NumericConvert.ToSingle(value._value, mode.Clamped());
            int IConvert<Int32C>.ToInt32(Int32C value, Conversion mode) => value._value;
            long IConvert<Int32C>.ToInt64(Int32C value, Conversion mode) => NumericConvert.ToInt64(value._value, mode.Clamped());
            sbyte IConvertExtended<Int32C>.ToSByte(Int32C value, Conversion mode) => NumericConvert.ToSByte(value._value, mode.Clamped());
            short IConvert<Int32C>.ToInt16(Int32C value, Conversion mode) => NumericConvert.ToInt16(value._value, mode.Clamped());
            string IConvert<Int32C>.ToString(Int32C value) => Convert.ToString(value._value);
            uint IConvertExtended<Int32C>.ToUInt32(Int32C value, Conversion mode) => NumericConvert.ToUInt32(value._value, mode.Clamped());
            ulong IConvertExtended<Int32C>.ToUInt64(Int32C value, Conversion mode) => NumericConvert.ToUInt64(value._value, mode.Clamped());
            ushort IConvertExtended<Int32C>.ToUInt16(Int32C value, Conversion mode) => NumericConvert.ToUInt16(value._value, mode.Clamped());

            Int32C IConvert<Int32C>.ToNumeric(bool value) => value ? 1 : 0;
            Int32C IConvert<Int32C>.ToNumeric(byte value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvert<Int32C>.ToNumeric(decimal value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvert<Int32C>.ToNumeric(double value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvert<Int32C>.ToNumeric(float value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvert<Int32C>.ToNumeric(int value, Conversion mode) => value;
            Int32C IConvert<Int32C>.ToNumeric(long value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvertExtended<Int32C>.ToValue(sbyte value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvert<Int32C>.ToNumeric(short value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvert<Int32C>.ToNumeric(string value) => Convert.ToInt32(value);
            Int32C IConvertExtended<Int32C>.ToNumeric(uint value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvertExtended<Int32C>.ToNumeric(ulong value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());
            Int32C IConvertExtended<Int32C>.ToNumeric(ushort value, Conversion mode) => NumericConvert.ToInt32(value, mode.Clamped());

            Int32C IStringParser<Int32C>.Parse(string s, NumberStyles? style, IFormatProvider? provider)
                => Parse(s, style ?? NumberStyles.Integer, provider);
        }
    }
}
