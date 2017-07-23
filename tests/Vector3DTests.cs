﻿using Xunit;
using System.Linq;
using static System.Math;
using System;

namespace SearchAThing.Sci.Tests
{
    public class Vector3DTests
    {

        double rad_tol;

        public Vector3DTests()
        {
            rad_tol = (1e-1).ToRad();
        }

        [Fact]
        public void AngleRadTest()
        {
            var v1 = new Vector3D(10, 0, 0);
            var v2 = new Vector3D(2, 5, 0);
            var angv1v2 = v1.AngleRad(1e-4, v2);
            var angv2v1 = v2.AngleRad(1e-4, v1);
            Assert.True(angv1v2.EqualsTol(rad_tol, angv2v1));
            Assert.True(angv1v2.EqualsTol(rad_tol, 68.2d.ToRad()));
        }

        [Fact]
        public void AngleTowardTest()
        {
            var v1 = new Vector3D(10, 0, 0);
            var v2 = new Vector3D(2, 5, 0);

            var angv1v2_zplus = v1.AngleToward(1e-4, v2, Vector3D.ZAxis);
            var angv1v2_zminus = v1.AngleToward(1e-4, v2, -Vector3D.ZAxis);

            var angv2v1_zplus = v2.AngleToward(1e-4, v1, Vector3D.ZAxis);
            var angv2v1_zminus = v2.AngleToward(1e-4, v1, -Vector3D.ZAxis);

            Assert.True(angv1v2_zplus.EqualsTol(rad_tol, angv2v1_zminus));
            Assert.True(angv1v2_zplus.EqualsTol(rad_tol, 68.1d.ToRad()));

            Assert.True(angv2v1_zplus.EqualsTol(rad_tol, angv1v2_zminus));
            Assert.True(angv2v1_zplus.EqualsTol(rad_tol, 291.8d.ToRad()));
        }

        [Fact]
        public void AxisTest()
        {
            var xaxis = Vector3D.Axis(0);
            Assert.True(xaxis.EqualsTol(1e-6, Vector3D.XAxis));

            var yaxis = Vector3D.Axis(1);
            Assert.True(yaxis.EqualsTol(1e-6, Vector3D.YAxis));

            var zaxis = Vector3D.Axis(2);
            Assert.True(zaxis.EqualsTol(1e-6, Vector3D.ZAxis));
        }

        [Fact]
        public void BBoxTest()
        {
            var v = new Vector3D(1, 2, 3);
            var bbox = v.BBox(1e-6, rad_tol);
            Assert.True(bbox.Min.EqualsTol(1e-6, bbox.Max));
            Assert.True(bbox.Min.EqualsTol(1e-6, v));
        }

        [Fact]
        public void ColinearTest()
        {
            var v = new Vector3D(1, 2, 3);
            var v2 = v.ScaleAbout(Vector3D.Zero, 2);
            Assert.True(v.Colinear(1e-6, v2));
            var v3 = v2.RotateAboutZAxis(rad_tol);
            Assert.False(v.Colinear(1e-6, v3));
        }

        [Fact]
        public void ConcordantTest()
        {
            var v = new Vector3D(1, 2, 3);
            var v2 = v.ScaleAbout(Vector3D.Zero, 2);
            var v3 = v.ScaleAbout(Vector3D.Zero, .5);
            var v4 = v.ScaleAbout(Vector3D.Zero, -.5);
            Assert.True(v.Concordant(1e-6, v2));
            Assert.True(v.Concordant(1e-6, v3));
            Assert.False(v.Concordant(1e-6, v4));
        }

        [Fact]
        public void ConvertTest()
        {
            var v = new Vector3D(1, 2, 3);
            var v2 = v.Convert(MUCollection.Force.kN, MUCollection.Force.N);
            Assert.True(v2.EqualsTol(1e-6, 1e3, 2e3, 3e3));
        }

        [Fact]
        public void CrossProductTest()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(4, 5, 6);
            var c = a.CrossProduct(b);
            Assert.True(c.Normalized().EqualsTol(1e-6, new Vector3D(-4.0825, 8.1650, -4.0825).Normalized()));
        }

        [Fact]
        public void DistanceTest()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(4, 5, 6);
            Assert.True(a.Distance(b).EqualsTol(1e-4, 5.1962));
        }

        [Fact]
        public void Distance2DTest()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(4, 5, 6);
            Assert.True(a.Distance2D(b).EqualsTol(1e-4, Sqrt(3 * 3 + 3 * 3)));
        }

        [Fact]
        public void DivideTest()
        {
            var v = new Vector3D(1, 2, 3);
            try
            {
                var vd = v.Divide(3);
            }
            catch (NotImplementedException)
            {
                // Geometry type vector, Divide method not implemented for Vector3D
                Assert.True(true);
            }
        }

        [Fact]
        public void DotProductTest()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(4, 5, 6);
            var d = a.DotProduct(b);
            Assert.True(d.EqualsTol(1e-6, a.X * b.X + a.Y * b.Y + a.Z * b.Z));
        }

        [Fact]
        public void EqualsTol1Test()
        {
            var v1 = new Vector3D(1, 2, 3);
            var v2 = new Vector3D(1.1, 2.1, 3.1);
            Assert.True(v1.EqualsTol(.11, v2));
            Assert.False(v1.EqualsTol(.09, v2));
        }

        [Fact]
        public void EqualsTol2Test()
        {
            var v1 = new Vector3D(1, 2, 3);
            var v2 = new Vector3D(1.1, 2.1, 3.1);
            Assert.True(v1.EqualsTol(.11, 1.1, 2.1, 3.1));
            Assert.False(v1.EqualsTol(.09, 1.1, 2.1, 3.1));
        }

        [Fact]
        public void EqualsTol3Test()
        {
            var v1 = new Vector3D(1, 2, 3);
            var v2 = new Vector3D(1.1, 2.1, 40);
            // test only x, y
            Assert.True(v1.EqualsTol(.11, 1.1, 2.1));
            Assert.False(v1.EqualsTol(.09, 1.1, 2.1));
        }

        [Fact]
        public void From2DCoordsTest()
        {
            var v = Vector3D.From2DCoords(1, 2, 3, 4, 5, 6);
            Assert.True(v.Count() == 3);
            Assert.True(v.First().EqualsTol(1e-6, 1, 2, 0));
            Assert.True(v.Skip(1).First().EqualsTol(1e-6, 3, 4, 0));
            Assert.True(v.Skip(2).First().EqualsTol(1e-6, 5, 6, 0));
        }

        [Fact]
        public void From3DCoordsTest()
        {
            var v = Vector3D.From3DCoords(1, 2, 3, 4, 5, 6);
            Assert.True(v.Count() == 2);
            Assert.True(v.First().EqualsTol(1e-6, 1, 2, 3));
            Assert.True(v.Skip(1).First().EqualsTol(1e-6, 4, 5, 6));
        }

        [Fact]
        public void FromStringTest()
        {
            var v = Vector3D.FromString("(1.2 3.4 5.6)");
            Assert.True(v.EqualsTol(1e-6, 1.2, 3.4, 5.6));

            // 2th format with comma separated
            v = Vector3D.FromString("(1.2,3.4,5.6)");
            Assert.True(v.EqualsTol(1e-6, 1.2, 3.4, 5.6));
        }

        [Fact]
        public void FromStringArrayTest()
        {
            var v = Vector3D.FromStringArray("(1.2 3.4 5.6);(7.8 9.0 1.2)");
            Assert.True(v.Count() == 2);
            Assert.True(v.First().EqualsTol(1e-6, 1.2, 3.4, 5.6));
            Assert.True(v.Skip(1).First().EqualsTol(1e-6, 7.8, 9.0, 1.2));
        }

        [Fact]
        public void GetOrdTest()
        {
            var v = new Vector3D(1, 2, 3);
            Assert.True(v.GetOrd(0).EqualsTol(1e-6, 1));
            Assert.True(v.GetOrd(1).EqualsTol(1e-6, 2));
            Assert.True(v.GetOrd(2).EqualsTol(1e-6, 3));
        }

        [Fact]
        public void IsParallelToTest()
        {
            var v1 = new Vector3D(2.5101, 1.7754, -2.1324);
            var v2 = new Vector3D(9.0365, 6.3918, -7.6768);
            Assert.True(v1.IsParallelTo(1e-4, v2));
        }

        [Fact]
        public void IsPerpendicularTest()
        {
            var v1 = new Vector3D(2.5101, 1.7754, -2.1324);
            var v2 = new Vector3D(-9.7136,8.0369,-4.7428);
            Assert.True(v1.IsPerpendicular(v2));
        }

        [Fact]
        public void MirrorTest()
        {

        }

        [Fact]
        public void NormalizedTest()
        {

        }

        [Fact]
        public void Project1Test()
        {

        }

        [Fact]
        public void Project2Test()
        {

        }

        [Fact]
        public void Random1Test()
        {

        }

        [Fact]
        public void Random2Test()
        {

        }

        [Fact]
        public void RelTest()
        {

        }

        [Fact]
        public void RotateAboutAxis1Test()
        {

        }

        [Fact]
        public void RotateAboutAxis2Test()
        {

        }

        [Fact]
        public void RotateAboutXAxisTest()
        {

        }

        [Fact]
        public void RotateAboutYAxisTest()
        {

        }

        [Fact]
        public void RotateAboutZAxisTest()
        {

        }

        [Fact]
        public void RotateAsTest()
        {

        }

        [Fact]
        public void ScalarTest()
        {

        }

        [Fact]
        public void ScaleAbout1Test()
        {

        }

        [Fact]
        public void ScaleAbout2Test()
        {

        }

        [Fact]
        public void SetTest()
        {

        }

        [Fact]
        public void StringRepresentationTest()
        {

        }

        [Fact]
        public void ToString1Test()
        {

        }

        [Fact]
        public void ToString2Test()
        {

        }

        [Fact]
        public void ToString3Test()
        {

        }

        [Fact]
        public void ToSystemVector3DTest()
        {

        }

        [Fact]
        public void ToUCSTest()
        {

        }

        [Fact]
        public void ToWCSTest()
        {

        }

        [Fact]
        public void Vector3D1Test()
        {

        }

        [Fact]
        public void Vector3D2Test()
        {

        }

        [Fact]
        public void Vector3D3Test()
        {

        }

        [Fact]
        public void Vector3D4Test()
        {

        }

        [Fact]
        public void OperatorSub1Test()
        {

        }

        [Fact]
        public void OperatorSub2Test()
        {

        }

        [Fact]
        public void OperatorScalarMul1Test()
        {

        }

        [Fact]
        public void OperatorScalarMul2Test()
        {

        }

        [Fact]
        public void OperatorMulTest()
        {

        }

        [Fact]
        public void OperatorDivide1Test()
        {

        }

        [Fact]
        public void OperatorDivide2Test()
        {

        }

        [Fact]
        public void OperatorSumTest()
        {

        }

    }

}