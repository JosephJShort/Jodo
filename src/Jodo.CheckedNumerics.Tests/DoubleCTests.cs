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

using Jodo.Numerics.Tests;

namespace Jodo.CheckedNumerics.Tests
{
    public static class DoubleCTests
    {
        public sealed class BitConvertTests : BitConvertTests<DoubleC> { }
        public sealed class CastTests : CastTests<DoubleC> { }
        public sealed class CheckedNumeric : CheckedNumericTests<DoubleC> { }
        public sealed class ConvertTests : ConvertTests<DoubleC> { }
        public sealed class MathFloatingPoint : MathTests.FloatingPoint<DoubleC> { }
        public sealed class MathGeneral : MathTests.General<DoubleC> { }
        public sealed class MathReal : MathTests.Real<DoubleC> { }
        public sealed class MathSigned : MathTests.SingedOnly<DoubleC> { }
        public sealed class NumericGeneral : NumericTests.General<DoubleC> { }
        public sealed class NumericNoInfinity : NumericTests.NoInfinity<DoubleC> { }
        public sealed class NumericNoNaN : NumericTests.NoNaN<DoubleC> { }
        public sealed class NumericReal : NumericTests.Real<DoubleC> { }
        public sealed class NumericSigned : NumericTests.SignedOnly<DoubleC> { }
        public sealed class ObjectTests : Primitives.Tests.ObjectTests<DoubleC> { }
        public sealed class ParserGeneral : StringParserTests.General<DoubleC> { }
        public sealed class RandomTests : Primitives.Tests.RandomTests<DoubleC> { }
        public sealed class SerializableTests : Primitives.Tests.SerializableTests<DoubleC> { }
    }
}
