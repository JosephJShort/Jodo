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

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using Jodo.Numerics;
using Jodo.Primitives;

namespace Jodo.Geometry
{
    [Serializable]
    [DebuggerDisplay("{ToString(),nq}")]
    [CLSCompliant(false)]
    public readonly struct Angle<N> :
            IComparable,
            IComparable<Angle<N>>,
            IEquatable<Angle<N>>,
            IFormattable,
            IProvider<IBitConverter<Angle<N>>>,
            IProvider<IRandom<Angle<N>>>,
            IProvider<IParser<Angle<N>>>,
            ISerializable
        where N : struct, INumeric<N>
    {
        private const string Symbol = "∠";

        public static readonly Angle<N> Zero = default;

        public readonly N Degrees;

        public N GetRadians() => Math<N>.DegreesToRadians(Degrees);
        public N Cos() => Math<N>.Cos(GetRadians());
        public N Cosh() => Math<N>.Cosh(GetRadians());
        public N Sinh() => Math<N>.Sinh(GetRadians());
        public N Tanh() => Math<N>.Tanh(GetRadians());
        public N Sin() => Math<N>.Sin(GetRadians());
        public N Tan() => Math<N>.Tan(GetRadians());

        public Angle(N degrees)
        {
            Degrees = degrees;
        }

        private Angle(SerializationInfo info, StreamingContext context)
        {
            Degrees = (N)info.GetValue(nameof(Degrees), typeof(N));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Degrees), Degrees, typeof(N));
        }

        public Angle<N> Normalise()
        {
            return new Angle<N>(Degrees.Remainder(Cast<N>.ToNumeric(360)));
        }

        public int CompareTo(object? obj) => obj is Angle<N> other ? CompareTo(other) : 1;
        public int CompareTo(Angle<N> other) => Degrees.CompareTo(other.Degrees);
        public bool Equals(Angle<N> other) => Degrees.Equals(other.Degrees);
        public override bool Equals(object? obj) => obj is Angle<N> angle && Equals(angle);
        public override int GetHashCode() => Degrees.GetHashCode();
        public override string ToString() => $"{Symbol}{Degrees}°";
        public string ToString(string? format, IFormatProvider? formatProvider) => $"{Symbol}{Degrees.ToString(format, formatProvider)}°";

        public static Angle<N> FromDegrees(N degrees) => new Angle<N>(degrees);
        public static Angle<N> FromDegrees(byte degrees) => new Angle<N>(Convert<N>.ToNumeric(degrees));
        public static Angle<N> FromRadians(N radians) => new Angle<N>(Math<N>.RadiansToDegrees(radians));
        public static Angle<N> FromRadians(byte radians) => new Angle<N>(Math<N>.RadiansToDegrees(Convert<N>.ToNumeric(radians)));

        public static bool TryParse(string value, out Angle<N> result) => Try.Run(() => Parse(value), out result);
        public static bool TryParse(string value, NumberStyles numberStyles, IFormatProvider? formatProvider, out Angle<N> result)
            => Try.Run(() => Parse(value, numberStyles, formatProvider), out result);

        public static Angle<N> Parse(string value)
        {
            string? trimmed = value.Trim().Replace(Symbol, string.Empty);

            if (TryParse(trimmed, "°", out N number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Degrees", out number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Degree", out number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Deg", out number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Radians", out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "Radian", out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "Rad", out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "C", out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "R", out number)) return Angle<N>.FromRadians(number);

            return Angle<N>.FromDegrees(Parser<N>.Parse(trimmed));
        }

        public static Angle<N> Parse(string value, NumberStyles numberStyles, IFormatProvider? formatProvider)
        {
            string? trimmed = value.Trim();

            if (TryParse(trimmed, "°", numberStyles, formatProvider, out N number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Degrees", numberStyles, formatProvider, out number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Degree", numberStyles, formatProvider, out number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Deg", numberStyles, formatProvider, out number)) return Angle<N>.FromDegrees(number);
            if (TryParse(trimmed, "Radians", numberStyles, formatProvider, out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "Radian", numberStyles, formatProvider, out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "Rad", numberStyles, formatProvider, out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "C", numberStyles, formatProvider, out number)) return Angle<N>.FromRadians(number);
            if (TryParse(trimmed, "R", numberStyles, formatProvider, out number)) return Angle<N>.FromRadians(number);

            return Angle<N>.FromDegrees(Parser<N>.Parse(trimmed));
        }

        private static bool TryParse(string value, string unit, out N number)
        {
            if (value.EndsWith(unit, StringComparison.InvariantCultureIgnoreCase))
            {
                number = Parser<N>.Parse(value.Substring(0, value.Length - unit.Length));
                return true;
            }
            number = default;
            return false;
        }

        private static bool TryParse(string value, string unit, NumberStyles numberStyles, IFormatProvider? formatProvider, out N number)
        {
            if (value.EndsWith(unit, StringComparison.InvariantCultureIgnoreCase))
            {
                number = Parser<N>.Parse(value.Substring(0, value.Length - unit.Length), numberStyles, formatProvider);
                return true;
            }
            number = default;
            return false;
        }

        public static implicit operator N(Angle<N> unit) => unit.Degrees;
        public static explicit operator Angle<N>(N value) => new Angle<N>(value);

        public static bool operator !=(Angle<N> left, Angle<N> right) => !left.Equals(right);
        public static bool operator <(Angle<N> left, Angle<N> right) => left.Degrees.IsLessThan(right.Degrees);
        public static bool operator <=(Angle<N> left, Angle<N> right) => left.Degrees.IsLessThanOrEqualTo(right.Degrees);
        public static bool operator ==(Angle<N> left, Angle<N> right) => left.Equals(right);
        public static bool operator >(Angle<N> left, Angle<N> right) => left.Degrees.IsGreaterThan(right.Degrees);
        public static bool operator >=(Angle<N> left, Angle<N> right) => left.Degrees.IsGreaterThanOrEqualTo(right.Degrees);
        public static Angle<N> operator %(Angle<N> left, Angle<N> right) => new Angle<N>(left.Degrees.Remainder(right.Degrees));
        public static Angle<N> operator -(Angle<N> left, Angle<N> right) => new Angle<N>(left.Degrees.Subtract(right.Degrees));
        public static Angle<N> operator -(Angle<N> value) => new Angle<N>(value.Degrees.Negative());
        public static Angle<N> operator *(Angle<N> left, Angle<N> right) => new Angle<N>(left.Degrees.Multiply(right.Degrees));
        public static Angle<N> operator /(Angle<N> left, Angle<N> right) => new Angle<N>(left.Degrees.Divide(right.Degrees));
        public static Angle<N> operator +(Angle<N> left, Angle<N> right) => new Angle<N>(left.Degrees.Add(right.Degrees));
        public static Angle<N> operator +(Angle<N> value) => new Angle<N>(value.Degrees);

        public static bool operator !=(Angle<N> left, N right) => !left.Degrees.Equals(right);
        public static bool operator <(Angle<N> left, N right) => left.Degrees.IsLessThan(right);
        public static bool operator <=(Angle<N> left, N right) => left.Degrees.IsLessThanOrEqualTo(right);
        public static bool operator ==(Angle<N> left, N right) => left.Degrees.Equals(right);
        public static bool operator >(Angle<N> left, N right) => left.Degrees.IsGreaterThan(right);
        public static bool operator >=(Angle<N> left, N right) => left.Degrees.IsGreaterThanOrEqualTo(right);
        public static Angle<N> operator %(Angle<N> left, N right) => new Angle<N>(left.Degrees.Remainder(right));
        public static Angle<N> operator -(Angle<N> left, N right) => new Angle<N>(left.Degrees.Subtract(right));
        public static Angle<N> operator *(Angle<N> left, N right) => new Angle<N>(left.Degrees.Multiply(right));
        public static Angle<N> operator /(Angle<N> left, N right) => new Angle<N>(left.Degrees.Divide(right));
        public static Angle<N> operator +(Angle<N> left, N right) => new Angle<N>(left.Degrees.Add(right));

        public static bool operator !=(N left, Angle<N> right) => !left.Equals(right.Degrees);
        public static bool operator <(N left, Angle<N> right) => left.IsLessThan(right.Degrees);
        public static bool operator <=(N left, Angle<N> right) => left.IsLessThanOrEqualTo(right.Degrees);
        public static bool operator ==(N left, Angle<N> right) => left.Equals(right.Degrees);
        public static bool operator >(N left, Angle<N> right) => left.IsGreaterThan(right.Degrees);
        public static bool operator >=(N left, Angle<N> right) => left.IsGreaterThanOrEqualTo(right.Degrees);
        public static Angle<N> operator %(N left, Angle<N> right) => new Angle<N>(left.Remainder(right.Degrees));
        public static Angle<N> operator -(N left, Angle<N> right) => new Angle<N>(left.Subtract(right.Degrees));
        public static Angle<N> operator *(N left, Angle<N> right) => new Angle<N>(left.Multiply(right.Degrees));
        public static Angle<N> operator /(N left, Angle<N> right) => new Angle<N>(left.Divide(right.Degrees));
        public static Angle<N> operator +(N left, Angle<N> right) => new Angle<N>(left.Add(right.Degrees));

        IBitConverter<Angle<N>> IProvider<IBitConverter<Angle<N>>>.GetInstance() => Utilities.Instance;
        IRandom<Angle<N>> IProvider<IRandom<Angle<N>>>.GetInstance() => Utilities.Instance;
        IParser<Angle<N>> IProvider<IParser<Angle<N>>>.GetInstance() => Utilities.Instance;

        private sealed class Utilities :
            IBitConverter<Angle<N>>,
            IRandom<Angle<N>>,
            IParser<Angle<N>>
        {
            public static readonly Utilities Instance = new Utilities();

            Angle<N> IRandom<Angle<N>>.Next(Random random) => new Angle<N>(random.NextNumeric<N>());
            Angle<N> IRandom<Angle<N>>.Next(Random random, Angle<N> bound1, Angle<N> bound2) => new Angle<N>(random.NextNumeric(bound1.Degrees, bound2.Degrees));

            Angle<N> IParser<Angle<N>>.Parse(string s) => Parse(s);
            Angle<N> IParser<Angle<N>>.Parse(string s, NumberStyles style, IFormatProvider? provider) => new Angle<N>(Parser<N>.Parse(s, style, provider));

            Angle<N> IBitConverter<Angle<N>>.Read(IReadOnlyStream<byte> stream) => new Angle<N>(BitConverter<N>.Read(stream));
            void IBitConverter<Angle<N>>.Write(Angle<N> value, IWriteOnlyStream<byte> stream) => BitConverter<N>.Write(stream, value.Degrees);
        }
    }
}