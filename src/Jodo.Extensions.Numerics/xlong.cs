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
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("csharpsquid", "S101")]
    public readonly struct xlong : INumeric<xlong>
    {
        public static readonly xlong MaxValue = new xlong(long.MaxValue);
        public static readonly xlong MinValue = new xlong(long.MinValue);

        private readonly long _value;

        private xlong(long value)
        {
            _value = value;
        }

        private xlong(SerializationInfo info, StreamingContext context) : this(info.GetInt64(nameof(xlong))) { }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue(nameof(xlong), _value);

        public int CompareTo(xlong other) => _value.CompareTo(other._value);
        public int CompareTo(object? obj) => obj is xlong other ? CompareTo(other) : 1;
        public bool Equals(xlong other) => _value == other._value;
        public override bool Equals(object? obj) => obj is xlong other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();
        public string ToString(IFormatProvider formatProvider) => _value.ToString(formatProvider);
        public string ToString(string format, IFormatProvider formatProvider) => _value.ToString(format, formatProvider);

        public static bool TryParse(string s, IFormatProvider provider, out xlong result) => Try.Run(() => Parse(s, provider), out result);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out xlong result) => Try.Run(() => Parse(s, style, provider), out result);
        public static bool TryParse(string s, NumberStyles style, out xlong result) => Try.Run(() => Parse(s, style), out result);
        public static bool TryParse(string s, out xlong result) => Try.Run(() => Parse(s), out result);
        public static xlong Parse(string s) => long.Parse(s);
        public static xlong Parse(string s, IFormatProvider provider) => long.Parse(s, provider);
        public static xlong Parse(string s, NumberStyles style) => long.Parse(s, style);
        public static xlong Parse(string s, NumberStyles style, IFormatProvider provider) => long.Parse(s, style, provider);

        public static explicit operator xlong(decimal value) => new xlong((long)value);
        public static explicit operator xlong(double value) => new xlong((long)value);
        public static explicit operator xlong(float value) => new xlong((long)value);
        public static explicit operator xlong(ulong value) => new xlong((long)value);
        public static implicit operator xlong(byte value) => new xlong(value);
        public static implicit operator xlong(char value) => new xlong(value);
        public static implicit operator xlong(int value) => new xlong(value);
        public static implicit operator xlong(long value) => new xlong(value);
        public static implicit operator xlong(sbyte value) => new xlong(value);
        public static implicit operator xlong(short value) => new xlong(value);
        public static implicit operator xlong(uint value) => new xlong(value);
        public static implicit operator xlong(ushort value) => new xlong(value);

        public static explicit operator byte(xlong value) => (byte)value._value;
        public static explicit operator char(xlong value) => (char)value._value;
        public static explicit operator int(xlong value) => (int)value._value;
        public static explicit operator sbyte(xlong value) => (sbyte)value._value;
        public static explicit operator short(xlong value) => (short)value._value;
        public static explicit operator uint(xlong value) => (uint)value._value;
        public static explicit operator ulong(xlong value) => (ulong)value._value;
        public static explicit operator ushort(xlong value) => (ushort)value._value;
        public static implicit operator decimal(xlong value) => value._value;
        public static implicit operator double(xlong value) => value._value;
        public static implicit operator float(xlong value) => value._value;
        public static implicit operator long(xlong value) => value._value;

        public static bool operator !=(xlong left, xlong right) => left._value != right._value;
        public static bool operator <(xlong left, xlong right) => left._value < right._value;
        public static bool operator <=(xlong left, xlong right) => left._value <= right._value;
        public static bool operator ==(xlong left, xlong right) => left._value == right._value;
        public static bool operator >(xlong left, xlong right) => left._value > right._value;
        public static bool operator >=(xlong left, xlong right) => left._value >= right._value;
        public static xlong operator %(xlong left, xlong right) => left._value % right._value;
        public static xlong operator &(xlong left, xlong right) => left._value & right._value;
        public static xlong operator -(xlong left, xlong right) => left._value - right._value;
        public static xlong operator --(xlong value) => value._value - 1;
        public static xlong operator -(xlong value) => -value._value;
        public static xlong operator *(xlong left, xlong right) => left._value * right._value;
        public static xlong operator /(xlong left, xlong right) => left._value / right._value;
        public static xlong operator ^(xlong left, xlong right) => left._value ^ right._value;
        public static xlong operator |(xlong left, xlong right) => left._value | right._value;
        public static xlong operator ~(xlong value) => ~value._value;
        public static xlong operator +(xlong left, xlong right) => left._value + right._value;
        public static xlong operator +(xlong value) => value;
        public static xlong operator ++(xlong value) => value._value + 1;
        public static xlong operator <<(xlong left, int right) => left._value << right;
        public static xlong operator >>(xlong left, int right) => left._value >> right;

        bool INumeric<xlong>.IsGreaterThan(xlong value) => this > value;
        bool INumeric<xlong>.IsGreaterThanOrEqualTo(xlong value) => this >= value;
        bool INumeric<xlong>.IsLessThan(xlong value) => this < value;
        bool INumeric<xlong>.IsLessThanOrEqualTo(xlong value) => this <= value;
        xlong INumeric<xlong>.Add(xlong value) => this + value;
        xlong INumeric<xlong>.Divide(xlong value) => this / value;
        xlong INumeric<xlong>.Multiply(xlong value) => this * value;
        xlong INumeric<xlong>.Negative() => -this;
        xlong INumeric<xlong>.Positive() => +this;
        xlong INumeric<xlong>.Remainder(xlong value) => this % value;
        xlong INumeric<xlong>.Subtract(xlong value) => this - value;

        IBitConverter<xlong> IBitConvertible<xlong>.BitConverter => Utilities.Instance;
        IConstants<xlong> INumeric<xlong>.Constants => Utilities.Instance;
        IConvert<xlong> IConvertible<xlong>.Convert => Utilities.Instance;
        IMath<xlong> INumeric<xlong>.Math => Utilities.Instance;
        IRandom<xlong> IRandomisable<xlong>.Random => Utilities.Instance;
        IStringParser<xlong> IStringParsable<xlong>.StringParser => Utilities.Instance;

        private sealed class Utilities :
            IBitConverter<xlong>,
            IConstants<xlong>,
            IConvert<xlong>,
            IMath<xlong>,
            IRandom<xlong>,
            IStringParser<xlong>
        {
            public readonly static Utilities Instance = new Utilities();

            bool IConstants<xlong>.IsReal { get; } = false;
            bool IConstants<xlong>.IsSigned { get; } = true;
            xlong IConstants<xlong>.Epsilon { get; } = 1L;
            xlong IConstants<xlong>.MaxUnit { get; } = 1L;
            xlong IConstants<xlong>.MaxValue => MaxValue;
            xlong IConstants<xlong>.MinUnit { get; } = -1L;
            xlong IConstants<xlong>.MinValue => MinValue;
            xlong IConstants<xlong>.One { get; } = 1L;
            xlong IConstants<xlong>.Zero { get; } = 0;
            xlong IMath<xlong>.E { get; } = 2L;
            xlong IMath<xlong>.PI { get; } = 3L;
            xlong IMath<xlong>.Tau { get; } = 6L;

            int IMath<xlong>.Sign(xlong x) => Math.Sign(x._value);
            xlong IMath<xlong>.Abs(xlong x) => Math.Abs(x._value);
            xlong IMath<xlong>.Acos(xlong x) => (long)Math.Acos(x._value);
            xlong IMath<xlong>.Acosh(xlong x) => (long)Math.Acosh(x._value);
            xlong IMath<xlong>.Asin(xlong x) => (long)Math.Asin(x._value);
            xlong IMath<xlong>.Asinh(xlong x) => (long)Math.Asinh(x._value);
            xlong IMath<xlong>.Atan(xlong x) => (long)Math.Atan(x._value);
            xlong IMath<xlong>.Atan2(xlong x, xlong y) => (long)Math.Atan2(x._value, y._value);
            xlong IMath<xlong>.Atanh(xlong x) => (long)Math.Atanh(x._value);
            xlong IMath<xlong>.Cbrt(xlong x) => (long)Math.Cbrt(x._value);
            xlong IMath<xlong>.Ceiling(xlong x) => x;
            xlong IMath<xlong>.Clamp(xlong x, xlong bound1, xlong bound2) => bound1 > bound2 ? Math.Min(bound1._value, Math.Max(bound2._value, x._value)) : Math.Min(bound2._value, Math.Max(bound1._value, x._value));
            xlong IMath<xlong>.Cos(xlong x) => (long)Math.Cos(x._value);
            xlong IMath<xlong>.Cosh(xlong x) => (long)Math.Cosh(x._value);
            xlong IMath<xlong>.DegreesToRadians(xlong x) => (long)(x * Trig.RadiansPerDegree);
            xlong IMath<xlong>.Exp(xlong x) => (long)Math.Exp(x._value);
            xlong IMath<xlong>.Floor(xlong x) => x;
            xlong IMath<xlong>.IEEERemainder(xlong x, xlong y) => (long)Math.IEEERemainder(x._value, y._value);
            xlong IMath<xlong>.Log(xlong x) => (long)Math.Log(x._value);
            xlong IMath<xlong>.Log(xlong x, xlong y) => (long)Math.Log(x._value, y._value);
            xlong IMath<xlong>.Log10(xlong x) => (long)Math.Log10(x._value);
            xlong IMath<xlong>.Max(xlong x, xlong y) => Math.Max(x._value, y._value);
            xlong IMath<xlong>.Min(xlong x, xlong y) => Math.Min(x._value, y._value);
            xlong IMath<xlong>.Pow(xlong x, byte y) => (long)Math.Pow(x._value, y);
            xlong IMath<xlong>.Pow(xlong x, xlong y) => (long)Math.Pow(x._value, y._value);
            xlong IMath<xlong>.RadiansToDegrees(xlong x) => (long)(x * Trig.DegreesPerRadian);
            xlong IMath<xlong>.Round(xlong x) => x;
            xlong IMath<xlong>.Round(xlong x, int digits) => x;
            xlong IMath<xlong>.Round(xlong x, int digits, MidpointRounding mode) => x;
            xlong IMath<xlong>.Round(xlong x, MidpointRounding mode) => x;
            xlong IMath<xlong>.Sin(xlong x) => (long)Math.Sin(x._value);
            xlong IMath<xlong>.Sinh(xlong x) => (long)Math.Sinh(x._value);
            xlong IMath<xlong>.Sqrt(xlong x) => (long)Math.Sqrt(x._value);
            xlong IMath<xlong>.Tan(xlong x) => (long)Math.Tan(x._value);
            xlong IMath<xlong>.Tanh(xlong x) => (long)Math.Tanh(x._value);
            xlong IMath<xlong>.Truncate(xlong x) => x;

            xlong IBitConverter<xlong>.Read(IReadOnlyStream<byte> stream) => BitConverter.ToInt64(stream.Read(sizeof(long)));
            void IBitConverter<xlong>.Write(xlong value, IWriteOnlyStream<byte> stream) => stream.Write(BitConverter.GetBytes(value._value));

            xlong IRandom<xlong>.Next(Random random) => random.NextInt64();
            xlong IRandom<xlong>.Next(Random random, xlong bound1, xlong bound2) => random.NextInt64(bound1._value, bound2._value);

            xlong IStringParser<xlong>.Parse(string s) => Parse(s);
            xlong IStringParser<xlong>.Parse(string s, NumberStyles numberStyles, IFormatProvider formatProvider) => Parse(s, numberStyles, formatProvider);

            bool IConvert<xlong>.ToBoolean(xlong value) => Convert.ToBoolean(value._value);
            byte IConvert<xlong>.ToByte(xlong value) => Convert.ToByte(value._value);
            char IConvert<xlong>.ToChar(xlong value) => Convert.ToChar(value._value);
            decimal IConvert<xlong>.ToDecimal(xlong value) => Convert.ToDecimal(value._value);
            double IConvert<xlong>.ToDouble(xlong value) => Convert.ToDouble(value._value);
            float IConvert<xlong>.ToSingle(xlong value) => Convert.ToSingle(value._value);
            int IConvert<xlong>.ToInt32(xlong value) => Convert.ToInt32(value._value);
            long IConvert<xlong>.ToInt64(xlong value) => Convert.ToInt64(value._value);
            sbyte IConvert<xlong>.ToSByte(xlong value) => Convert.ToSByte(value._value);
            short IConvert<xlong>.ToInt16(xlong value) => Convert.ToInt16(value._value);
            string IConvert<xlong>.ToString(xlong value) => Convert.ToString(value._value);
            string IConvert<xlong>.ToString(xlong value, IFormatProvider provider) => Convert.ToString(value._value, provider);
            uint IConvert<xlong>.ToUInt32(xlong value) => Convert.ToUInt32(value._value);
            ulong IConvert<xlong>.ToUInt64(xlong value) => Convert.ToUInt64(value._value);
            ushort IConvert<xlong>.ToUInt16(xlong value) => Convert.ToUInt16(value._value);

            xlong IConvert<xlong>.ToValue(bool value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(byte value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(char value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(decimal value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(double value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(float value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(int value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(long value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(sbyte value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(short value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(string value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(string value, IFormatProvider provider) => Convert.ToInt64(value, provider);
            xlong IConvert<xlong>.ToValue(uint value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(ulong value) => Convert.ToInt64(value);
            xlong IConvert<xlong>.ToValue(ushort value) => Convert.ToInt64(value);
        }
    }
}
