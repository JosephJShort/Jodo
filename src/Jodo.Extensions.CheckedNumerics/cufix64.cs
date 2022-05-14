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

using Jodo.Extensions.Numerics;
using Jodo.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace Jodo.Extensions.CheckedNumerics
{
    [Serializable]
    [DebuggerDisplay("{ToString(),nq}")]
    [SuppressMessage("Style", "IDE1006:Naming Styles")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")]
    [SuppressMessage("csharpsquid", "S101")]
    public readonly struct cufix64 : INumeric<cufix64>
    {
        public static readonly cufix64 Epsilon = new cufix64(1);
        public static readonly cufix64 MaxValue = new cufix64(ulong.MaxValue);
        public static readonly cufix64 MinValue = new cufix64(ulong.MinValue);

        private const ulong ScalingFactor = 0b1000_0000_0000_0000_0000_0000;

        private readonly ulong _scaledValue;

        private cufix64(ulong scaledValue)
        {
            _scaledValue = scaledValue;
        }

        private cufix64(SerializationInfo info, StreamingContext context) : this(info.GetUInt64(nameof(cufix64))) { }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue(nameof(cufix64), _scaledValue);

        public int CompareTo(object? obj) => obj is cufix64 other ? CompareTo(other) : 1;
        public int CompareTo(cufix64 other) => _scaledValue.CompareTo(other._scaledValue);
        public bool Equals(cufix64 other) => _scaledValue == other._scaledValue;
        public override bool Equals(object? obj) => obj is cufix64 other && Equals(other);
        public override int GetHashCode() => _scaledValue.GetHashCode();
        public override string ToString() => ((double)this).ToString();
        public string ToString(IFormatProvider formatProvider) => ((double)this).ToString(formatProvider);
        public string ToString(string format, IFormatProvider formatProvider) => ((double)this).ToString(format, formatProvider);

        public static bool TryParse(string s, IFormatProvider provider, out cufix64 result) => Try.Run(() => Parse(s, provider), out result);
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out cufix64 result) => Try.Run(() => Parse(s, style, provider), out result);
        public static bool TryParse(string s, NumberStyles style, out cufix64 result) => Try.Run(() => Parse(s, style), out result);
        public static bool TryParse(string s, out cufix64 result) => Try.Run(() => Parse(s), out result);
        public static cufix64 Parse(string s) => (cufix64)double.Parse(s);
        public static cufix64 Parse(string s, IFormatProvider provider) => (cufix64)double.Parse(s, provider);
        public static cufix64 Parse(string s, NumberStyles style) => (cufix64)double.Parse(s, style);
        public static cufix64 Parse(string s, NumberStyles style, IFormatProvider provider) => (cufix64)double.Parse(s, style, provider);

        public static explicit operator cufix64(in decimal value) => new cufix64(CheckedConvert.ToUInt64(value * ScalingFactor));
        public static explicit operator cufix64(in double value) => new cufix64(CheckedConvert.ToUInt64(value * ScalingFactor));
        public static explicit operator cufix64(in int value) => new cufix64(CheckedConvert.ToUInt64(value) * ScalingFactor);
        public static explicit operator cufix64(in long value) => new cufix64(CheckedConvert.ToUInt64(value) * ScalingFactor);
        public static explicit operator cufix64(in sbyte value) => new cufix64(Convert.ToUInt64(value) * ScalingFactor);
        public static explicit operator cufix64(in short value) => new cufix64(CheckedConvert.ToUInt64(value) * ScalingFactor);
        public static explicit operator cufix64(in ulong value) => new cufix64(value * ScalingFactor);
        public static implicit operator cufix64(in byte value) => new cufix64(value * ScalingFactor);
        public static implicit operator cufix64(in char value) => new cufix64(value * ScalingFactor);
        public static implicit operator cufix64(in float value) => new cufix64(CheckedConvert.ToUInt64(value * ScalingFactor));
        public static implicit operator cufix64(in uint value) => new cufix64(CheckedConvert.ToUInt64(value) * ScalingFactor);
        public static implicit operator cufix64(in ushort value) => new cufix64(CheckedConvert.ToUInt64(value) * ScalingFactor);

        public static explicit operator byte(in cufix64 value) => CheckedConvert.ToByte(value._scaledValue / ScalingFactor);
        public static explicit operator char(in cufix64 value) => CheckedConvert.ToChar(value._scaledValue / ScalingFactor);
        public static explicit operator decimal(in cufix64 value) => (decimal)value._scaledValue / ScalingFactor;
        public static explicit operator double(in cufix64 value) => (double)value._scaledValue / ScalingFactor;
        public static explicit operator float(in cufix64 value) => (float)value._scaledValue / ScalingFactor;
        public static explicit operator int(in cufix64 value) => CheckedConvert.ToInt32(value._scaledValue / ScalingFactor);
        public static explicit operator long(in cufix64 value) => CheckedConvert.ToInt64(value._scaledValue / ScalingFactor);
        public static explicit operator sbyte(in cufix64 value) => CheckedConvert.ToSByte(value._scaledValue / ScalingFactor);
        public static explicit operator short(in cufix64 value) => CheckedConvert.ToInt16(value._scaledValue / ScalingFactor);
        public static explicit operator uint(in cufix64 value) => CheckedConvert.ToUInt32(value._scaledValue / ScalingFactor);
        public static explicit operator ulong(in cufix64 value) => value._scaledValue / ScalingFactor;
        public static explicit operator ushort(in cufix64 value) => CheckedConvert.ToUInt16(value._scaledValue / ScalingFactor);

        public static bool operator !=(in cufix64 left, in cufix64 right) => left._scaledValue != right._scaledValue;
        public static bool operator <(in cufix64 left, in cufix64 right) => left._scaledValue < right._scaledValue;
        public static bool operator <=(in cufix64 left, in cufix64 right) => left._scaledValue <= right._scaledValue;
        public static bool operator ==(in cufix64 left, in cufix64 right) => left._scaledValue == right._scaledValue;
        public static bool operator >(in cufix64 left, in cufix64 right) => left._scaledValue > right._scaledValue;
        public static bool operator >=(in cufix64 left, in cufix64 right) => left._scaledValue >= right._scaledValue;
        public static cufix64 operator %(in cufix64 left, in cufix64 right) => new cufix64(CheckedArithmetic.ScaledRemainder(left._scaledValue, right._scaledValue, ScalingFactor));
        public static cufix64 operator &(in cufix64 left, in cufix64 right) => new cufix64(left._scaledValue & right._scaledValue);
        public static cufix64 operator -(in cufix64 _) => 0;
        public static cufix64 operator -(in cufix64 left, in cufix64 right) => new cufix64(CheckedArithmetic.Subtract(left._scaledValue, right._scaledValue));
        public static cufix64 operator --(in cufix64 value) => new cufix64(value._scaledValue - ScalingFactor);
        public static cufix64 operator *(in cufix64 left, in cufix64 right) => new cufix64(CheckedArithmetic.ScaledMultiply(left._scaledValue, right._scaledValue, ScalingFactor));
        public static cufix64 operator /(in cufix64 left, in cufix64 right) => new cufix64(CheckedArithmetic.ScaledDivide(left._scaledValue, right._scaledValue, ScalingFactor));
        public static cufix64 operator ^(in cufix64 left, in cufix64 right) => new cufix64(left._scaledValue ^ right._scaledValue);
        public static cufix64 operator |(in cufix64 left, in cufix64 right) => new cufix64(left._scaledValue | right._scaledValue);
        public static cufix64 operator ~(in cufix64 value) => new cufix64(~value._scaledValue);
        public static cufix64 operator +(in cufix64 left, in cufix64 right) => new cufix64(CheckedArithmetic.Add(left._scaledValue, right._scaledValue));
        public static cufix64 operator +(in cufix64 value) => value;
        public static cufix64 operator ++(in cufix64 value) => new cufix64(value._scaledValue + ScalingFactor);
        public static cufix64 operator <<(in cufix64 left, in int right) => new cufix64(left._scaledValue << right);
        public static cufix64 operator >>(in cufix64 left, in int right) => new cufix64(left._scaledValue >> right);

        IBitConverter<cufix64> IBitConvertible<cufix64>.BitConverter => Utilities.Instance;
        IMath<cufix64> INumeric<cufix64>.Math => Utilities.Instance;
        IRandom<cufix64> IRandomisable<cufix64>.Random => Utilities.Instance;
        IStringParser<cufix64> IStringRepresentable<cufix64>.StringParser => Utilities.Instance;

        private sealed class Utilities : IMath<cufix64>, IBitConverter<cufix64>, IRandom<cufix64>, IStringParser<cufix64>
        {
            public readonly static Utilities Instance = new Utilities();

            cufix64 IMath<cufix64>.E { get; } = (cufix64)Math.E;
            cufix64 IMath<cufix64>.PI { get; } = (cufix64)Math.PI;
            cufix64 IMath<cufix64>.Epsilon { get; } = 1;
            cufix64 IMath<cufix64>.MaxValue => MaxValue;
            cufix64 IMath<cufix64>.MinValue => MinValue;
            cufix64 IMath<cufix64>.MaxUnit { get; } = ScalingFactor;
            cufix64 IMath<cufix64>.MinUnit { get; } = 0;
            cufix64 IMath<cufix64>.Zero { get; } = 0;
            cufix64 IMath<cufix64>.One { get; } = ScalingFactor;
            bool IMath<cufix64>.IsSigned { get; } = false;
            bool IMath<cufix64>.IsReal { get; } = true;

            bool IMath<cufix64>.IsGreaterThan(in cufix64 x, in cufix64 y) => x > y;
            bool IMath<cufix64>.IsGreaterThanOrEqualTo(in cufix64 x, in cufix64 y) => x >= y;
            bool IMath<cufix64>.IsLessThan(in cufix64 x, in cufix64 y) => x < y;
            bool IMath<cufix64>.IsLessThanOrEqualTo(in cufix64 x, in cufix64 y) => x <= y;
            double IMath<cufix64>.ToDouble(in cufix64 x, in double offset) => CheckedArithmetic.Add((double)x, offset);
            float IMath<cufix64>.ToSingle(in cufix64 x, in float offset) => CheckedArithmetic.Add((float)x, offset);
            int IMath<cufix64>.Sign(in cufix64 x) => x._scaledValue == 0 ? 0 : 1;
            cufix64 IMath<cufix64>.Abs(in cufix64 x) => x;
            cufix64 IMath<cufix64>.Acos(in cufix64 x) => (cufix64)Math.Acos((double)x);
            cufix64 IMath<cufix64>.Acosh(in cufix64 x) => (cufix64)Math.Acosh((double)x);
            cufix64 IMath<cufix64>.Add(in cufix64 x, in cufix64 y) => x + y;
            cufix64 IMath<cufix64>.Asin(in cufix64 x) => (cufix64)Math.Asin((double)x);
            cufix64 IMath<cufix64>.Asinh(in cufix64 x) => (cufix64)Math.Asinh((double)x);
            cufix64 IMath<cufix64>.Atan(in cufix64 x) => (cufix64)Math.Atan((double)x);
            cufix64 IMath<cufix64>.Atan2(in cufix64 x, in cufix64 y) => (cufix64)Math.Atan2((double)x, (double)y);
            cufix64 IMath<cufix64>.Atanh(in cufix64 x) => (cufix64)Math.Atanh((double)x);
            cufix64 IMath<cufix64>.Cbrt(in cufix64 x) => (cufix64)Math.Cbrt((double)x);
            cufix64 IMath<cufix64>.Ceiling(in cufix64 x) => x._scaledValue % ScalingFactor != 0 ? new cufix64((x._scaledValue / ScalingFactor * ScalingFactor) + ScalingFactor) : new cufix64(x._scaledValue / ScalingFactor * ScalingFactor);
            cufix64 IMath<cufix64>.Clamp(in cufix64 x, in cufix64 bound1, in cufix64 bound2) => bound1 > bound2 ? Math.Min(bound1._scaledValue, Math.Max(bound2._scaledValue, x._scaledValue)) : Math.Min(bound2._scaledValue, Math.Max(bound1._scaledValue, x._scaledValue));
            cufix64 IMath<cufix64>.Cos(in cufix64 x) => (cufix64)Math.Cos((double)x);
            cufix64 IMath<cufix64>.Cosh(in cufix64 x) => (cufix64)Math.Cosh((double)x);
            cufix64 IMath<cufix64>.DegreesToRadians(in cufix64 x) => (cufix64)CheckedArithmetic.Multiply((double)x, AngleConstants.RadiansPerDegree);
            cufix64 IMath<cufix64>.DegreesToTurns(in cufix64 x) => (cufix64)CheckedArithmetic.Multiply((double)x, AngleConstants.TurnsPerDegree);
            cufix64 IMath<cufix64>.Divide(in cufix64 x, in cufix64 y) => x / y;
            cufix64 IMath<cufix64>.Exp(in cufix64 x) => (cufix64)Math.Exp((double)x);
            cufix64 IMath<cufix64>.Floor(in cufix64 x) => new cufix64(x._scaledValue / ScalingFactor * ScalingFactor);
            cufix64 IMath<cufix64>.IEEERemainder(in cufix64 x, in cufix64 y) => (cufix64)Math.IEEERemainder((double)x, (double)y);
            cufix64 IMath<cufix64>.Log(in cufix64 x) => (cufix64)Math.Log((double)x);
            cufix64 IMath<cufix64>.Log(in cufix64 x, in cufix64 y) => (cufix64)Math.Log((double)x, (double)y);
            cufix64 IMath<cufix64>.Log10(in cufix64 x) => (cufix64)Math.Log10((double)x);
            cufix64 IMath<cufix64>.Max(in cufix64 x, in cufix64 y) => new cufix64(Math.Max(x._scaledValue, y._scaledValue));
            cufix64 IMath<cufix64>.Min(in cufix64 x, in cufix64 y) => new cufix64(Math.Min(x._scaledValue, y._scaledValue));
            cufix64 IMath<cufix64>.Multiply(in cufix64 x, in cufix64 y) => x * y;
            cufix64 IMath<cufix64>.Negative(in cufix64 x) => -x;
            cufix64 IMath<cufix64>.Positive(in cufix64 x) => +x;
            cufix64 IMath<cufix64>.Pow(in cufix64 x, in byte y) => (cufix64)Math.Pow((double)x, y);
            cufix64 IMath<cufix64>.Pow(in cufix64 x, in cufix64 y) => (cufix64)Math.Pow((double)x, (double)y);
            cufix64 IMath<cufix64>.RadiansToDegrees(in cufix64 x) => (cufix64)CheckedArithmetic.Multiply((double)x, AngleConstants.DegreesPerRadian);
            cufix64 IMath<cufix64>.RadiansToTurns(in cufix64 x) => (cufix64)CheckedArithmetic.Multiply((double)x, AngleConstants.TurnsPerRadian);
            cufix64 IMath<cufix64>.Remainder(in cufix64 x, in cufix64 y) => x % y;
            cufix64 IMath<cufix64>.Round(in cufix64 x) => (cufix64)Math.Round((double)x);
            cufix64 IMath<cufix64>.Round(in cufix64 x, in int digits) => (cufix64)Math.Round((double)x, digits);
            cufix64 IMath<cufix64>.Round(in cufix64 x, in int digits, in MidpointRounding mode) => (cufix64)Math.Round((double)x, digits, mode);
            cufix64 IMath<cufix64>.Round(in cufix64 x, in MidpointRounding mode) => (cufix64)Math.Round((double)x, mode);
            cufix64 IMath<cufix64>.Sin(in cufix64 x) => (cufix64)Math.Sin((double)x);
            cufix64 IMath<cufix64>.Sinh(in cufix64 x) => (cufix64)Math.Sinh((double)x);
            cufix64 IMath<cufix64>.Sqrt(in cufix64 x) => (cufix64)Math.Sqrt((double)x);
            cufix64 IMath<cufix64>.Subtract(in cufix64 x, in cufix64 y) => x - y;
            cufix64 IMath<cufix64>.Tan(in cufix64 x) => (cufix64)Math.Tan((double)x);
            cufix64 IMath<cufix64>.Tanh(in cufix64 x) => (cufix64)Math.Tanh((double)x);
            cufix64 IMath<cufix64>.Truncate(in cufix64 x) => new cufix64(x._scaledValue / ScalingFactor * ScalingFactor);
            cufix64 IMath<cufix64>.TurnsToDegrees(in cufix64 x) => (cufix64)CheckedArithmetic.Multiply((double)x, AngleConstants.DegreesPerTurn);
            cufix64 IMath<cufix64>.TurnsToRadians(in cufix64 x) => (cufix64)CheckedArithmetic.Multiply((double)x, AngleConstants.DegreesPerRadian);

            cufix64 IBitConverter<cufix64>.Read(in IReadOnlyStream<byte> stream) => new cufix64(BitConverter.ToUInt64(stream.Read(sizeof(ulong))));
            void IBitConverter<cufix64>.Write(cufix64 value, in IWriteOnlyStream<byte> stream) => stream.Write(BitConverter.GetBytes(value._scaledValue));

            cufix64 IRandom<cufix64>.GetNext(Random random) => new cufix64(random.NextUInt64());
            cufix64 IRandom<cufix64>.GetNext(Random random, in cufix64 bound1, in cufix64 bound2) => new cufix64(random.NextUInt64(bound1._scaledValue, bound2._scaledValue));

            cufix64 IStringParser<cufix64>.Parse(in string s) => Parse(s);
            cufix64 IStringParser<cufix64>.Parse(in string s, in NumberStyles numberStyles, in IFormatProvider formatProvider) => Parse(s, numberStyles, formatProvider);
        }

        IConvert<cufix64> IConvertible<cufix64>.Convert => _Convert.Instance;
        private sealed class _Convert : IConvert<cufix64>
        {
            public readonly static _Convert Instance = new _Convert();

            bool IConvert<cufix64>.ToBoolean(cufix64 value) => value._scaledValue != 0;
            byte IConvert<cufix64>.ToByte(cufix64 value) => CheckedConvert.ToByte(value._scaledValue / ScalingFactor);
            char IConvert<cufix64>.ToChar(cufix64 value) => CheckedConvert.ToChar(value._scaledValue / ScalingFactor);
            decimal IConvert<cufix64>.ToDecimal(cufix64 value) => (decimal)value._scaledValue / ScalingFactor;
            double IConvert<cufix64>.ToDouble(cufix64 value) => (double)value._scaledValue / ScalingFactor;
            float IConvert<cufix64>.ToSingle(cufix64 value) => (float)value._scaledValue / ScalingFactor;
            int IConvert<cufix64>.ToInt32(cufix64 value) => CheckedConvert.ToInt32(value._scaledValue / ScalingFactor);
            long IConvert<cufix64>.ToInt64(cufix64 value) => CheckedConvert.ToInt64(value._scaledValue / ScalingFactor);
            sbyte IConvert<cufix64>.ToSByte(cufix64 value) => CheckedConvert.ToSByte(value._scaledValue / ScalingFactor);
            short IConvert<cufix64>.ToInt16(cufix64 value) => CheckedConvert.ToInt16(value._scaledValue / ScalingFactor);
            string IConvert<cufix64>.ToString(cufix64 value) => value.ToString();
            string IConvert<cufix64>.ToString(cufix64 value, IFormatProvider provider) => value.ToString(provider);
            uint IConvert<cufix64>.ToUInt32(cufix64 value) => CheckedConvert.ToUInt32(value._scaledValue / ScalingFactor);
            ulong IConvert<cufix64>.ToUInt64(cufix64 value) => value._scaledValue / ScalingFactor;
            ushort IConvert<cufix64>.ToUInt16(cufix64 value) => CheckedConvert.ToUInt16(value._scaledValue / ScalingFactor);

            cufix64 IConvert<cufix64>.ToValue(bool value) => value ? ScalingFactor : 0;
            cufix64 IConvert<cufix64>.ToValue(byte value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(char value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(decimal value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(double value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(float value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(int value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(long value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(sbyte value) => value;
            cufix64 IConvert<cufix64>.ToValue(short value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(string value) => Convert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(string value, IFormatProvider provider) => Convert.ToSByte(value, provider);
            cufix64 IConvert<cufix64>.ToValue(uint value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(ulong value) => CheckedConvert.ToSByte(value);
            cufix64 IConvert<cufix64>.ToValue(ushort value) => CheckedConvert.ToSByte(value);
        }
    }
}