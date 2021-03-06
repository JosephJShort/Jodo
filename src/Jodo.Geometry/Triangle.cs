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

namespace Jodo.Geometry
{
    [Serializable]
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly struct Triangle<TNumeric> :
            IEquatable<Triangle<TNumeric>>,
            IFormattable,
            IProvider<IBitConverter<Triangle<TNumeric>>>,
            IProvider<IRandom<Triangle<TNumeric>>>,
            IProvider<IStringParser<Triangle<TNumeric>>>,
            ITwoDimensional<Triangle<TNumeric>, TNumeric>,
            ISerializable
        where TNumeric : struct, INumeric<TNumeric>
    {
        private const string Symbol = "△";

        public readonly Vector2<TNumeric> A;
        public readonly Vector2<TNumeric> B;
        public readonly Vector2<TNumeric> C;

        public Triangle(Vector2<TNumeric> a, Vector2<TNumeric> b, Vector2<TNumeric> c)
        {
            A = a;
            B = b;
            C = c;
        }

        private Triangle(SerializationInfo info, StreamingContext context) : this(
            (Vector2<TNumeric>)info.GetValue(nameof(A), typeof(Vector2<TNumeric>)),
            (Vector2<TNumeric>)info.GetValue(nameof(B), typeof(Vector2<TNumeric>)),
            (Vector2<TNumeric>)info.GetValue(nameof(C), typeof(Vector2<TNumeric>)))
        { }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(A), A, typeof(Vector2<TNumeric>));
            info.AddValue(nameof(B), B, typeof(Vector2<TNumeric>));
            info.AddValue(nameof(C), C, typeof(Vector2<TNumeric>));
        }

        public AARectangle<TNumeric> GetBounds()
        {
            throw new NotImplementedException();
        }

        public TNumeric GetArea()
        {
            throw new NotImplementedException();
        }

        public Vector2<TNumeric>[] GetVertices()
        {
            throw new NotImplementedException();
        }

        public Vector2<TNumeric> GetCenter()
        {
            throw new NotImplementedException();
        }

        public Triangle<TNumeric> Translate(Vector2<TNumeric> delta) => new Triangle<TNumeric>(A + delta, B + delta, C + delta);

        public bool Contains(Vector2<TNumeric> point) => throw new NotImplementedException();
        public bool Contains(TNumeric pointX, TNumeric pointY) => Contains(new Vector2<TNumeric>(pointX, pointY));

        public bool Contains(Triangle<TNumeric> other) => throw new NotImplementedException();
        public bool IntersectsWith(Triangle<TNumeric> other) => throw new NotImplementedException();

        public Triangle<TNumeric> Rotate90() => throw new NotImplementedException();
        public Rectangle<TNumeric> Rotate(Angle<TNumeric> angle) => throw new NotImplementedException();
        public Rectangle<TNumeric> RotateAround(Vector2<TNumeric> pivot, Angle<TNumeric> angle) => throw new NotImplementedException();

        public Triangle<TResult> Convert<TResult>(Func<TNumeric, TResult> converter) where TResult : struct, INumeric<TResult>
            => new Triangle<TResult>(A.Convert(converter), B.Convert(converter), C.Convert(converter));
        public bool Equals(Triangle<TNumeric> other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        public override bool Equals(object? obj) => obj is Triangle<TNumeric> fix && Equals(fix);
        public override int GetHashCode() => HashCode.Combine(A, B, C);
        public override string ToString() => $"{Symbol}({A}, {B}, {C})";
        public string ToString(string? format, IFormatProvider? formatProvider) => $"{Symbol}({A}, {B}, {C})";

        public static bool operator ==(Triangle<TNumeric> left, Triangle<TNumeric> right) => left.Equals(right);
        public static bool operator !=(Triangle<TNumeric> left, Triangle<TNumeric> right) => !(left == right);

#if NETSTANDARD2_0_OR_GREATER
        public static implicit operator Triangle<TNumeric>((Vector2<TNumeric>, Vector2<TNumeric>, Vector2<TNumeric>) value) => new Triangle<TNumeric>(value.Item1, value.Item2, value.Item3);
        public static implicit operator (Vector2<TNumeric>, Vector2<TNumeric>, Vector2<TNumeric>)(Triangle<TNumeric> value) => (value.A, value.B, value.C);
#endif

        Vector2<TNumeric>[] ITwoDimensional<Triangle<TNumeric>, TNumeric>.GetVertices(int circumferenceDivisor) => GetVertices();
        IBitConverter<Triangle<TNumeric>> IProvider<IBitConverter<Triangle<TNumeric>>>.GetInstance() => Utilities.Instance;
        IRandom<Triangle<TNumeric>> IProvider<IRandom<Triangle<TNumeric>>>.GetInstance() => Utilities.Instance;
        IStringParser<Triangle<TNumeric>> IProvider<IStringParser<Triangle<TNumeric>>>.GetInstance() => Utilities.Instance;

        private sealed class Utilities :
           IBitConverter<Triangle<TNumeric>>,
           IRandom<Triangle<TNumeric>>,
           IStringParser<Triangle<TNumeric>>
        {
            public static readonly Utilities Instance = new Utilities();

            Triangle<TNumeric> IRandom<Triangle<TNumeric>>.Next(Random random)
            {
                throw new NotImplementedException();
            }

            Triangle<TNumeric> IRandom<Triangle<TNumeric>>.Next(Random random, Triangle<TNumeric> bound1, Triangle<TNumeric> bound2)
            {
                throw new NotImplementedException();
            }

            Triangle<TNumeric> IStringParser<Triangle<TNumeric>>.Parse(string s, NumberStyles? style, IFormatProvider? provider)
            {
                throw new NotImplementedException();
            }

            Triangle<TNumeric> IBitConverter<Triangle<TNumeric>>.Read(IReader<byte> stream)
            {
                throw new NotImplementedException();
            }

            void IBitConverter<Triangle<TNumeric>>.Write(Triangle<TNumeric> value, IWriter<byte> stream)
            {
                throw new NotImplementedException();
            }
        }
    }
}
