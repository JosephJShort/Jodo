﻿// Copyright (c) 2022 Joseph J. Short
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

using Jodo.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace Jodo.Extensions.Numerics
{
    [Serializable]
    [DebuggerDisplay("{ToString(),nq}")]
    [SuppressMessage("Style", "IDE1006:Naming Styles")]
    [SuppressMessage("csharpsquid", "S101")]
    public readonly struct xint : INumeric<xint>
    {
        public static readonly xint MaxValue = new xint(int.MaxValue);
        public static readonly xint MinValue = new xint(int.MinValue);

        private readonly int _value;

        private xint(int value)
        {
            _value = value;
        }

        private xint(SerializationInfo info, StreamingContext context) : this(info.GetInt32(nameof(xint))) { }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue(nameof(xint), _value);

        public int CompareTo(xint other) => _value.CompareTo(other._value);
        public int CompareTo(object? obj) => obj is xint other ? CompareTo(other) : 1;
        public bool Equals(xint other) => _value == other._value;
        public override bool Equals(object? obj) => obj is xint other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();
        public string ToString(IFormatProvider formatProvider) => _value.ToString(formatProvider);
        public string ToString(string format) => _value.ToString(format);
        public string ToString(string format, IFormatProvider formatProvider) => _value.ToString(format, formatProvider);

        public static bool TryParse(string s, IFormatProvider? provider, out xint result) => Try.Run(() => Parse(s, provider), out result);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider? provider, out xint result) => Try.Run(() => Parse(s, style, provider), out result);
        public static bool TryParse(string s, NumberStyles style, out xint result) => Try.Run(() => Parse(s, style), out result);
        public static bool TryParse(string s, out xint result) => Try.Run(() => Parse(s), out result);
        public static xint Parse(string s) => int.Parse(s);
        public static xint Parse(string s, IFormatProvider? provider) => int.Parse(s, provider);
        public static xint Parse(string s, NumberStyles style) => int.Parse(s, style);
        public static xint Parse(string s, NumberStyles style, IFormatProvider? provider) => int.Parse(s, style, provider);

        public static explicit operator xint(decimal value) => new xint((int)value);
        public static explicit operator xint(double value) => new xint((int)value);
        public static explicit operator xint(float value) => new xint((int)value);
        public static explicit operator xint(long value) => new xint((int)value);
        public static explicit operator xint(uint value) => new xint((int)value);
        public static explicit operator xint(ulong value) => new xint((int)value);
        public static implicit operator xint(byte value) => new xint(value);
        public static implicit operator xint(int value) => new xint(value);
        public static implicit operator xint(sbyte value) => new xint(value);
        public static implicit operator xint(short value) => new xint(value);
        public static implicit operator xint(ushort value) => new xint(value);

        public static explicit operator byte(xint value) => (byte)value._value;
        public static explicit operator sbyte(xint value) => (sbyte)value._value;
        public static explicit operator short(xint value) => (short)value._value;
        public static explicit operator uint(xint value) => (uint)value._value;
        public static explicit operator ulong(xint value) => (ulong)value._value;
        public static explicit operator ushort(xint value) => (ushort)value._value;
        public static implicit operator decimal(xint value) => value._value;
        public static implicit operator double(xint value) => value._value;
        public static implicit operator float(xint value) => value._value;
        public static implicit operator int(xint value) => value._value;
        public static implicit operator long(xint value) => value._value;

        public static bool operator !=(xint left, xint right) => left._value != right._value;
        public static bool operator <(xint left, xint right) => left._value < right._value;
        public static bool operator <=(xint left, xint right) => left._value <= right._value;
        public static bool operator ==(xint left, xint right) => left._value == right._value;
        public static bool operator >(xint left, xint right) => left._value > right._value;
        public static bool operator >=(xint left, xint right) => left._value >= right._value;
        public static xint operator %(xint left, xint right) => left._value % right._value;
        public static xint operator &(xint left, xint right) => left._value & right._value;
        public static xint operator -(xint left, xint right) => left._value - right._value;
        public static xint operator --(xint value) => value._value - 1;
        public static xint operator -(xint value) => -value._value;
        public static xint operator *(xint left, xint right) => left._value * right._value;
        public static xint operator /(xint left, xint right) => left._value / right._value;
        public static xint operator ^(xint left, xint right) => left._value ^ right._value;
        public static xint operator |(xint left, xint right) => left._value | right._value;
        public static xint operator ~(xint value) => ~value._value;
        public static xint operator +(xint left, xint right) => left._value + right._value;
        public static xint operator +(xint value) => value;
        public static xint operator ++(xint value) => value._value + 1;
        public static xint operator <<(xint left, int right) => left._value << right;
        public static xint operator >>(xint left, int right) => left._value >> right;

        bool INumeric<xint>.IsGreaterThan(xint value) => this > value;
        bool INumeric<xint>.IsGreaterThanOrEqualTo(xint value) => this >= value;
        bool INumeric<xint>.IsLessThan(xint value) => this < value;
        bool INumeric<xint>.IsLessThanOrEqualTo(xint value) => this <= value;
        xint INumeric<xint>.Add(xint value) => this + value;
        xint INumeric<xint>.BitwiseComplement() => ~this;
        xint INumeric<xint>.Divide(xint value) => this / value;
        xint INumeric<xint>.LeftShift(int count) => this << count;
        xint INumeric<xint>.LogicalAnd(xint value) => this & value;
        xint INumeric<xint>.LogicalExclusiveOr(xint value) => this ^ value;
        xint INumeric<xint>.LogicalOr(xint value) => this | value;
        xint INumeric<xint>.Multiply(xint value) => this * value;
        xint INumeric<xint>.Negative() => -this;
        xint INumeric<xint>.Positive() => +this;
        xint INumeric<xint>.Remainder(xint value) => this % value;
        xint INumeric<xint>.RightShift(int count) => this >> count;
        xint INumeric<xint>.Subtract(xint value) => this - value;

        IBitConverter<xint> IProvider<IBitConverter<xint>>.GetInstance() => Utilities.Instance;
        ICast<xint> IProvider<ICast<xint>>.GetInstance() => Utilities.Instance;
        IConvert<xint> IProvider<IConvert<xint>>.GetInstance() => Utilities.Instance;
        IMath<xint> IProvider<IMath<xint>>.GetInstance() => Utilities.Instance;
        INumericStatic<xint> IProvider<INumericStatic<xint>>.GetInstance() => Utilities.Instance;
        IRandom<xint> IProvider<IRandom<xint>>.GetInstance() => Utilities.Instance;
        IStringParser<xint> IProvider<IStringParser<xint>>.GetInstance() => Utilities.Instance;

        private sealed class Utilities :
            IBitConverter<xint>,
            ICast<xint>,
            IConvert<xint>,
            IMath<xint>,
            INumericStatic<xint>,
            IRandom<xint>,
            IStringParser<xint>
        {
            public readonly static Utilities Instance = new Utilities();

            bool INumericStatic<xint>.HasFloatingPoint { get; } = false;
            bool INumericStatic<xint>.HasInfinity { get; } = false;
            bool INumericStatic<xint>.HasNaN { get; } = false;
            bool INumericStatic<xint>.IsFinite(xint x) => true;
            bool INumericStatic<xint>.IsInfinity(xint x) => false;
            bool INumericStatic<xint>.IsNaN(xint x) => false;
            bool INumericStatic<xint>.IsNegative(xint x) => x._value < 0;
            bool INumericStatic<xint>.IsNegativeInfinity(xint x) => false;
            bool INumericStatic<xint>.IsNormal(xint x) => false;
            bool INumericStatic<xint>.IsPositiveInfinity(xint x) => false;
            bool INumericStatic<xint>.IsReal { get; } = false;
            bool INumericStatic<xint>.IsSigned { get; } = true;
            bool INumericStatic<xint>.IsSubnormal(xint x) => false;
            xint INumericStatic<xint>.Epsilon { get; } = 1;
            xint INumericStatic<xint>.MaxUnit { get; } = 1;
            xint INumericStatic<xint>.MaxValue => MaxValue;
            xint INumericStatic<xint>.MinUnit { get; } = -1;
            xint INumericStatic<xint>.MinValue => MinValue;
            xint INumericStatic<xint>.One { get; } = 1;
            xint INumericStatic<xint>.Ten { get; } = 10;
            xint INumericStatic<xint>.Two { get; } = 2;
            xint INumericStatic<xint>.Zero { get; } = 0;

            int IMath<xint>.Sign(xint x) => Math.Sign(x._value);
            xint IMath<xint>.Abs(xint x) => Math.Abs(x._value);
            xint IMath<xint>.Acos(xint x) => (int)Math.Acos(x._value);
            xint IMath<xint>.Acosh(xint x) => (int)Math.Acosh(x._value);
            xint IMath<xint>.Asin(xint x) => (int)Math.Asin(x._value);
            xint IMath<xint>.Asinh(xint x) => (int)Math.Asinh(x._value);
            xint IMath<xint>.Atan(xint x) => (int)Math.Atan(x._value);
            xint IMath<xint>.Atan2(xint x, xint y) => (int)Math.Atan2(x._value, y._value);
            xint IMath<xint>.Atanh(xint x) => (int)Math.Atanh(x._value);
            xint IMath<xint>.Cbrt(xint x) => (int)Math.Cbrt(x._value);
            xint IMath<xint>.Ceiling(xint x) => x;
            xint IMath<xint>.Clamp(xint x, xint bound1, xint bound2) => bound1 > bound2 ? Math.Min(bound1._value, Math.Max(bound2._value, x._value)) : Math.Min(bound2._value, Math.Max(bound1._value, x._value));
            xint IMath<xint>.Cos(xint x) => (int)Math.Cos(x._value);
            xint IMath<xint>.Cosh(xint x) => (int)Math.Cosh(x._value);
            xint IMath<xint>.DegreesToRadians(xint x) => (int)(x * Trig.RadiansPerDegree);
            xint IMath<xint>.E { get; } = 2;
            xint IMath<xint>.Exp(xint x) => (int)Math.Exp(x._value);
            xint IMath<xint>.Floor(xint x) => x;
            xint IMath<xint>.IEEERemainder(xint x, xint y) => (int)Math.IEEERemainder(x._value, y._value);
            xint IMath<xint>.Log(xint x) => (int)Math.Log(x._value);
            xint IMath<xint>.Log(xint x, xint y) => (int)Math.Log(x._value, y._value);
            xint IMath<xint>.Log10(xint x) => (int)Math.Log10(x._value);
            xint IMath<xint>.Max(xint x, xint y) => Math.Max(x._value, y._value);
            xint IMath<xint>.Min(xint x, xint y) => Math.Min(x._value, y._value);
            xint IMath<xint>.PI { get; } = 3;
            xint IMath<xint>.Pow(xint x, xint y) => (int)Math.Pow(x._value, y._value);
            xint IMath<xint>.RadiansToDegrees(xint x) => (int)(x * Trig.DegreesPerRadian);
            xint IMath<xint>.Round(xint x) => x;
            xint IMath<xint>.Round(xint x, int digits) => x;
            xint IMath<xint>.Round(xint x, int digits, MidpointRounding mode) => x;
            xint IMath<xint>.Round(xint x, MidpointRounding mode) => x;
            xint IMath<xint>.Sin(xint x) => (int)Math.Sin(x._value);
            xint IMath<xint>.Sinh(xint x) => (int)Math.Sinh(x._value);
            xint IMath<xint>.Sqrt(xint x) => (int)Math.Sqrt(x._value);
            xint IMath<xint>.Tan(xint x) => (int)Math.Tan(x._value);
            xint IMath<xint>.Tanh(xint x) => (int)Math.Tanh(x._value);
            xint IMath<xint>.Tau { get; } = 6;
            xint IMath<xint>.Truncate(xint x) => x;

            xint IBitConverter<xint>.Read(IReadOnlyStream<byte> stream) => BitConverter.ToInt32(stream.Read(sizeof(int)));
            void IBitConverter<xint>.Write(xint value, IWriteOnlyStream<byte> stream) => stream.Write(BitConverter.GetBytes(value._value));

            xint IRandom<xint>.Next(Random random) => random.NextInt32();
            xint IRandom<xint>.Next(Random random, xint bound1, xint bound2) => random.NextInt32(bound1._value, bound2._value);

            bool IConvert<xint>.ToBoolean(xint value) => Convert.ToBoolean(value._value);
            byte IConvert<xint>.ToByte(xint value) => Convert.ToByte(value._value);
            decimal IConvert<xint>.ToDecimal(xint value) => Convert.ToDecimal(value._value);
            double IConvert<xint>.ToDouble(xint value) => Convert.ToDouble(value._value);
            float IConvert<xint>.ToSingle(xint value) => Convert.ToSingle(value._value);
            int IConvert<xint>.ToInt32(xint value) => Convert.ToInt32(value._value);
            long IConvert<xint>.ToInt64(xint value) => Convert.ToInt64(value._value);
            sbyte IConvert<xint>.ToSByte(xint value) => Convert.ToSByte(value._value);
            short IConvert<xint>.ToInt16(xint value) => Convert.ToInt16(value._value);
            string IConvert<xint>.ToString(xint value) => Convert.ToString(value._value);
            uint IConvert<xint>.ToUInt32(xint value) => Convert.ToUInt32(value._value);
            ulong IConvert<xint>.ToUInt64(xint value) => Convert.ToUInt64(value._value);
            ushort IConvert<xint>.ToUInt16(xint value) => Convert.ToUInt16(value._value);

            xint IConvert<xint>.ToNumeric(bool value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(byte value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(decimal value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(double value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(float value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(int value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(long value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(sbyte value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(short value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(string value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(uint value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(ulong value) => Convert.ToInt32(value);
            xint IConvert<xint>.ToNumeric(ushort value) => Convert.ToInt32(value);

            xint IStringParser<xint>.Parse(string s) => Parse(s);
            xint IStringParser<xint>.Parse(string s, NumberStyles style, IFormatProvider? provider) => Parse(s, style, provider);

            byte ICast<xint>.ToByte(xint value) => (byte)value;
            decimal ICast<xint>.ToDecimal(xint value) => (decimal)value;
            double ICast<xint>.ToDouble(xint value) => (double)value;
            float ICast<xint>.ToSingle(xint value) => (float)value;
            int ICast<xint>.ToInt32(xint value) => (int)value;
            long ICast<xint>.ToInt64(xint value) => (long)value;
            sbyte ICast<xint>.ToSByte(xint value) => (sbyte)value;
            short ICast<xint>.ToInt16(xint value) => (short)value;
            uint ICast<xint>.ToUInt32(xint value) => (uint)value;
            ulong ICast<xint>.ToUInt64(xint value) => (ulong)value;
            ushort ICast<xint>.ToUInt16(xint value) => (ushort)value;

            xint ICast<xint>.ToNumeric(byte value) => (xint)value;
            xint ICast<xint>.ToNumeric(decimal value) => (xint)value;
            xint ICast<xint>.ToNumeric(double value) => (xint)value;
            xint ICast<xint>.ToNumeric(float value) => (xint)value;
            xint ICast<xint>.ToNumeric(int value) => (xint)value;
            xint ICast<xint>.ToNumeric(long value) => (xint)value;
            xint ICast<xint>.ToNumeric(sbyte value) => (xint)value;
            xint ICast<xint>.ToNumeric(short value) => (xint)value;
            xint ICast<xint>.ToNumeric(uint value) => (xint)value;
            xint ICast<xint>.ToNumeric(ulong value) => (xint)value;
            xint ICast<xint>.ToNumeric(ushort value) => (xint)value;
        }
    }
}
