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
using System.Diagnostics.CodeAnalysis;

namespace Jodo.Benchmarking
{
    [ExcludeFromCodeCoverage]
    public static class Benchmark
    {
        public const int DurationInSeconds = 60;

        public static void Run(Func<object> subjectFunction, Func<object> baselineFunction)
        {
            object voidObj = new object();
            Func<object> voidFunction = new Func<object>(() => voidObj);

            TimeSpan trialTime = TimeSpan.FromSeconds(DurationInSeconds / 4.0);
            Measurement subjectMeasurement = Measurer.Measure(subjectFunction, trialTime);
            Measurement baselineMeasurement = Measurer.Measure(baselineFunction, trialTime);
            Measurement voidMeasurement = Measurer.Measure(voidFunction, trialTime);

            subjectMeasurement = Adjust(subjectMeasurement, voidMeasurement);
            baselineMeasurement = Adjust(baselineMeasurement, voidMeasurement);

            Writer.Write(new StackTrace().GetFrame(1).GetMethod().Name, subjectMeasurement, baselineMeasurement);
        }

        private static Measurement Adjust(Measurement measurement, Measurement voidMeasurement)
        {
            double subjectAdjustment = measurement.Count * voidMeasurement.TotalTime.TotalSeconds / (long)voidMeasurement.Count;
            if (double.IsFinite(subjectAdjustment) && subjectAdjustment > 0)
            {
                return new Measurement(measurement.Count, TimeSpan.FromSeconds(measurement.TotalTime.TotalSeconds - subjectAdjustment));
            }
            return measurement;
        }
    }
}
