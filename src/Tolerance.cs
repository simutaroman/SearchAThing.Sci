﻿#region SearchAThing.Sci, Copyright(C) 2016 Lorenzo Delana, License under MIT
/*
* The MIT License(MIT)
* Copyright(c) 2016 Lorenzo Delana, https://searchathing.com
*
* Permission is hereby granted, free of charge, to any person obtaining a
* copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation
* the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
* DEALINGS IN THE SOFTWARE.
*/
#endregion

using static System.Math;
using System.Collections.Generic;

namespace SearchAThing
{

    namespace Sci
    {

        public class DoubleEqualityComparer : IEqualityComparer<double>
        {

            double tol;
            double tolHc;

            public DoubleEqualityComparer(double _tol)
            {
                tol = _tol;
                tolHc = 10 * tol; // to avoid rounding
            }

            public bool Equals(double x, double y)
            {
                return x.EqualsTol(tol, y);
            }

            public int GetHashCode(double obj)
            {
                return (int)(obj / tolHc);
            }
        }


    }

    public static partial class Extensions
    {

        public static bool EqualsTol(this double x, double tol, double y)
        {
            return Abs(x - y) <= tol;
        }

        public static bool EqualsAutoTol(this double x, double y)
        {
            return x.EqualsTol(y, Abs(x * 1e-6));
        }

        public static bool GreatThanTol(this double x, double tol, double y)
        {
            return x > y && !x.EqualsTol(tol, y);
        }

        public static bool GreatThanOrEqualsTol(this double x, double tol, double y)
        {
            return x > y || x.EqualsTol(tol, y);
        }

        public static bool LessThanTol(this double x, double tol, double y)
        {
            return x < y && !x.EqualsTol(tol, y);
        }

        public static bool LessThanOrEqualsTol(this double x, double tol, double y)
        {
            return x < y || x.EqualsTol(tol, y);
        }

        public static int CompareTol(this double x, double tol, double y)
        {
            if (x.EqualsTol(tol, y)) return 0;
            if (x < y) return -1;
            return 1; // x > y
        }

    }
  
}
