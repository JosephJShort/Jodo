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
using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {

        public static N Average<N>(this IEnumerable<N> source) where N : struct, INumeric<N>
        {
            N sum = Constants<N>.Zero;
            N count = Constants<N>.Zero;
            foreach (var item in source)
            {
                sum += item;
                count += 1;
            }
            return sum / count;
        }

        public static N Sum<N>(this IEnumerable<N> source) where N : struct, INumeric<N>
        {
            N sum = Constants<N>.Zero;
            foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }

    }
}