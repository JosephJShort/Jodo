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

using Jodo.Extensions.Benchmarking;
using System;
using System.Threading.Tasks;

namespace Jodo.Extensions.Numerics.Benchmarks
{
    public static class IntxBenchmarks
    {
        private static readonly Random Random = new Random();

        [Benchmark]
        public static void Intx_Versus_Int32_Negation()
        {
            var baselineInput = Random.NextInt32(100, 1000);
            var subjectInput = (shortx)baselineInput;

            Benchmark.Run(
                () => -subjectInput,
                () => -baselineInput);
        }

        [Benchmark]
        public static void Intx_Versus_Int32_Division()
        {
            var baselineInput1 = Random.NextInt32(100, 1000);
            var baselineInput2 = Random.NextInt32(2, 10);
            var subjectInput1 = (shortx)baselineInput1;
            var subjectInput2 = (shortx)baselineInput2;

            Benchmark.Run(
                () => subjectInput1 / subjectInput2,
                () => baselineInput1 / baselineInput2);
        }

        [Benchmark]
        public static void Intx_Versus_Int32_ConversionToFloat()
        {
            var baseline = Random.NextInt32(100, 1000);
            var sut = (shortx)baseline;

            Benchmark.Run(
                () => (float)sut,
                () => (float)baseline);
        }

        [Benchmark]
        public static void Intx_Versus_Int32_StringParsing()
        {
            var stringInput = Random.NextInt32(-100, 100).ToString();

            Benchmark.Run(
                () => shortx.Parse(stringInput),
                () => short.Parse(stringInput));
        }

        [Benchmark]
        public static void Intx_Versus_Int32_Overflow()
        {
            var subjectInput = shortx.MaxValue;
            var baselineInput = short.MaxValue;

            Benchmark.Run(
                () => { Task.Delay(1).Wait(); return subjectInput * subjectInput; },
                () => baselineInput * baselineInput);
        }
    }
}