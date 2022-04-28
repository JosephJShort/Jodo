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

using Jodo.Extensions.CheckedGeometry.Internals;
using Jodo.Extensions.CheckedNumerics;
using System;
using System.Collections.Generic;

namespace Jodo.Extensions.CheckedGeometry
{
    public readonly struct Circle<T> : IEquatable<Circle<T>> where T : struct, INumeric<T>
    {
        public readonly Vector2<T> Center;
        public readonly T Radius;

        public Circle(Vector2<T> center, T radius)
        {
            Center = center;
            Radius = radius;
        }

        public Circle(T centerX, T centerY, T radius)
        {
            Center = new Vector2<T>(centerX, centerY);
            Radius = radius;
        }

        public T Diameter => 2 * Radius;
        public T Area => Math<T>.Pi * Math<T>.Pow(Radius, 2);
        public T Circumeference => 2 * Math<T>.Pi * Radius;

        public bool Equals(Circle<T> other) => Center.Equals(other.Center) && EqualityComparer<T>.Default.Equals(Radius, other.Radius);
        public override bool Equals(object? obj) => obj is Circle<T> circle && Equals(circle);
        public override int GetHashCode() => HashCode.Combine(Center, Radius);
        public override string ToString() => Utilities.GetString(GetType(), Center, Radius);

        public Circle<T> Translate(in Vector2<T> delta) => new Circle<T>(Center.Translate(delta), Radius);
        public Circle<T> Translate(in T deltaX, in T deltaY) => new Circle<T>(Center.Translate(deltaX, deltaY), Radius);
        public AARectangle<T> GetBounds() => AARectangle<T>.FromCenter(Center, (Diameter, Diameter));
        public bool IntersectsWith(in Circle<T> other) => Center.DistanceFrom(other.Center) < Radius + other.Radius;

        public static bool operator ==(Circle<T> left, Circle<T> right) => left.Equals(right);
        public static bool operator !=(Circle<T> left, Circle<T> right) => !(left == right);
    }
}