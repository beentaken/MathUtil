using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtil.Tests
{
    class Program
    {
        static int BenchSize = 100 * 1000 * 1000;

        static void Main(string[] args)
        {
            //TestSubstract();
            //TestAdd();
            //TestMultiply();
            //BenchMultiply();
            //TestDivide();
            //TestDot();
            //BenchDot();
            //TestCross();
            //TestLength();
            //TestLengthSquared();
            //TestNormalize();
            //TestProject();
            //BenchProject();
            //TestAngle();
            //TestLerp();
            //TestSlerp();
            //BenchSlerp();
            //TestNlerp();
            //BenchNlerp();
            //TestDistance();
            //TestMiddle();
            //TestNearestPointOnLine();

            Console.ReadLine();
        }

        static void TestSubstract()
        {
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var substract = v1.Substract(v2);
                Console.WriteLine("Substract: " + substract);
            }

            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var substract = v1 - v2;
                Console.WriteLine("Substract: " + substract);
            }
        }

        static void TestAdd()
        {
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var add = v1.Add(v2);
                Console.WriteLine("Add: " + add);
            }

            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var add = v1 + v2;
                Console.WriteLine("Add: " + add);
            }
        }

        static void TestMultiply()
        {
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = v1.Multiply(2.0);
                Console.WriteLine("Multiply: " + v2);
            }

            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = v1 * 2.0;
                Console.WriteLine("Multiply: " + v2);
            }
        }

        static void BenchMultiply()
        {
            var a = new Vector2[BenchSize];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < BenchSize; i++)
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = v1.Multiply(2.0);
                a[i] = v2;
            }
            sw.Stop();
            Console.WriteLine("Multiply: " + sw.Elapsed.TotalMilliseconds + "ms");
        }

        static void TestDivide()
        {
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = v1.Divide(2.0);
                Console.WriteLine("Divide: " + v2);
            }

            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = v1 / 2.0;
                Console.WriteLine("Divide: " + v2);
            }
        }

        static void TestDot()
        {
            var v1 = new Vector2(1.0, 1.0);
            var v2 = new Vector2(1.0, 2.0);
            double dot = v1.Dot(v2);
            Console.WriteLine("Dot: " + dot);
        }

        static void BenchDot()
        {
            var a = new double[BenchSize];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < BenchSize; i++)
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                double dot = v1.Dot(v2);
                a[i] = dot;
            }
            sw.Stop();
            Console.WriteLine("Dot: " + sw.Elapsed.TotalMilliseconds + "ms");
        }

        static void TestCross()
        {
            var v1 = new Vector2(1.0, 1.0);
            var v2 = new Vector2(1.0, 2.0);
            double dot = v1.Cross(v2);
            Console.WriteLine("Cross: " + dot);
        }

        static void TestLength()
        {
            var v = new Vector2(10.0, 0.0);
            var length = v.Length();
            Console.WriteLine("Length: " + length);
        }

        static void TestLengthSquared()
        {
            var v = new Vector2(10.0, 0.0);
            var lengthSquared = v.LengthSquared();
            Console.WriteLine("LengthSquared: " + lengthSquared);
        }

        static void TestNormalize()
        {
            var v = new Vector2(10.0, 0.0);
            var normalize = v.Normalize();
            Console.WriteLine("Normalize: " + normalize);
        }

        static void TestProjection()
        {
            var v1 = new Vector2(1.0, 1.0);
            var v2 = new Vector2(1.0, 2.0);
            var project = v1.Projection(v2);
            Console.WriteLine("Projection: " + project);
        }

        static void BenchProjection()
        {
            var a = new Vector2[BenchSize];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < BenchSize; i++)
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var project = v1.Projection(v2);
                a[i] = project;
            }
            sw.Stop();
            Console.WriteLine("Projection: " + sw.Elapsed.TotalMilliseconds + "ms");
        }

        private static void TestAngle()
        {
            var v1 = new Vector2(1.0, 0.0);
            var v2 = new Vector2(0.0, 1.0);
            Console.WriteLine("Acos: " + v1.Angle(v2) * Vector2.RadiansToDegrees);
        }

        static void TestLerp()
        {
            var v1 = new Vector2(1.0, 1.0);
            var v2 = new Vector2(1.0, 2.0);
            var lerp = v1.Lerp(v2, 0.2);
            Console.WriteLine("Lerp: " + lerp);
        }

        static void TestSlerp()
        {
            var v1 = new Vector2(1.0, 0.0);
            var v2 = new Vector2(0.0, 1.0);
            var slerp = v1.Slerp(v2, 0.5);
            Console.WriteLine("Slerp: " + slerp);
        }

        static void BenchSlerp()
        {
            var a = new Vector2[BenchSize];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < BenchSize; i++)
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var slerp = v1.Slerp(v2, 0.5);
                a[i] = slerp;
            }
            sw.Stop();
            Console.WriteLine("Slerp: " + sw.Elapsed.TotalMilliseconds + "ms");
        }

        static void TestNlerp()
        {
            var v1 = new Vector2(1.0, 0.0);
            var v2 = new Vector2(0.0, 1.0);
            var nlerp = v1.Nlerp(v2, 0.5);
            Console.WriteLine("Nlerp: " + nlerp);
        }

        static void BenchNlerp()
        {
            var a = new Vector2[BenchSize];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < BenchSize; i++)
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var nlerp = v1.Nlerp(v2, 0.5);
                a[i] = nlerp;
            }
            sw.Stop();
            Console.WriteLine("Nlerp: " + sw.Elapsed.TotalMilliseconds + "ms");
        }

        static void TestDistance()
        {
            var a = new Vector2(100.0, 100.0);
            var b = new Vector2(100.0, 200.0);
            var distance = a.Distance(b);
            Console.WriteLine("Distance: " + distance);
        }

        static void TestMiddle()
        {
            var a = new Vector2(100.0, 100.0);
            var b = new Vector2(100.0, 200.0);
            var middle = a.Middle(b);
            Console.WriteLine("Middle: " + middle);
        }

        static void TestNearestPointOnLine()
        {
            var a = new Vector2(100.0, 100.0);
            var b = new Vector2(100.0, 200.0);
            var p = new Vector2(50.0, 150.0);
            var nearest = p.NearestPointOnLine(a, b);
            Console.WriteLine("NearestPointOnLine: " + nearest);
        }
    }
}
