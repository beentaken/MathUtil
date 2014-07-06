using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtil.Tests
{
    public struct Vector2
    {
        #region Properties

        public double X { get; private set; }
        public double Y { get; private set; }

        #endregion

        #region Constructor

        public Vector2(double x, double y)
            : this()
        {
            this.X = x;
            this.Y = y;
        } 

        #endregion

        #region ToString

        public override string ToString()
        {
            return string.Concat(X, CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, Y);
        }

        #endregion

        #region Arithmetic

        public Vector2 Substract(Vector2 v)
        {
            return new Vector2(this.X - v.X, this.Y - v.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return v1.Substract(v2);
        }

        public Vector2 Add(Vector2 v)
        {
            return new Vector2(this.X + v.X, this.Y + v.Y);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return v1.Add(v2);
        }

        public Vector2 Multiply(double scalar)
        {
            return new Vector2(
                this.X * scalar,
                this.Y * scalar);
        }

        public static Vector2 operator *(Vector2 v, double scalar)
        {
            return v.Multiply(scalar);
        }

        public Vector2 Divide(double scalar)
        {
            return new Vector2(
                this.X / scalar,
                this.Y / scalar);
        }

        public static Vector2 operator /(Vector2 v, double scalar)
        {
            return v.Divide(scalar);
        }

        public double Dot(Vector2 v)
        {
            return (this.X * v.X) + (this.Y * v.Y);
        }

        #endregion

        public double Length()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public double LengthSquared()
        {
            return this.X * this.X + this.Y * this.Y;
        }

        public Vector2 Normalize()
        {
            double length = this.Length();
            return this / length;
        }

        public Vector2 Project(Vector2 v)
        {
            return v * (this.Dot(v) / v.Dot(v));
        }
 
        #region Interpolation

        public Vector2 Lerp(Vector2 v, double amount)
        {
            return this + (v - this) * amount;
        }

        public Vector2 Slerp(Vector2 v, double amount)
        {
            double dot = Clamp(this.Dot(v), -1.0, 1.0);
            double theta = Math.Acos(dot) * amount;
            Vector2 relative = (v - this * dot).Normalize();
            return ((this * Math.Cos(theta)) + (relative * Math.Sin(theta)));
        }

        public Vector2 Nlerp(Vector2 v, double amount)
        {
            return this.Lerp(v, amount).Normalize();
        }
        
        #endregion

        #region Point

        public double Distance(Vector2 v)
        {
            double dx = this.X - v.X;
            double dy = this.Y - v.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public Vector2 Middle(Vector2 v)
        {
            return new Vector2(
                (this.X + v.X) / 2.0,
                (this.Y + v.Y) / 2.0);
        }

        public Vector2 NearestPointOnLine(Vector2 a, Vector2 b)
        {
            return (this - a).Project(b - a) + a;
        }

        #endregion

        #region Math

        public static double Clamp(double value, double min, double max)
        {
            return value > max ? max : value < min ? min : value;
        }

        #endregion
    }

    public struct PointF
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public PointF(float x, float y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public void Set(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return string.Concat(X, CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, Y);
        }
    }

    public static class VectorUtil
    {
        public static float Dot(PointF a, PointF b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        public static PointF Multiply(PointF a, float scalar)
        {
            return new PointF(a.X * scalar, a.Y * scalar);
        }

        public static PointF Project(PointF a, PointF b)
        {
            return Multiply(b, Dot(a, b) / Dot(b, b));
        }

        public static PointF Substract(PointF a, PointF b)
        {
            return new PointF(a.X - b.X, a.Y - b.Y);
        }

        public static PointF Add(PointF a, PointF b)
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }

        public static PointF NearestPointOnLine(PointF a, PointF b, PointF p)
        {
            // http://en.wikipedia.org/wiki/Vector_projection
            return Add(
                Project(
                    Substract(p, a),
                    Substract(b, a)),
                a);
        }
    }

    public static class LineUtil
    {
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float dx = x1 - x2;
            float dy = y1 - y2;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static void Middle(ref PointF point, float x1, float y1, float x2, float y2)
        {
            float x = x1 + x2;
            float y = y1 + y2;
            point.Set(x / 2f, y / 2f);
        }
    }

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

            //TestLength();
            //TestLengthSquared();
            //TestNormalize();
            //TestProject();
            //BenchProject();

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

        static void TestProject()
        {
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var project = v1.Project(v2);
                Console.WriteLine("Project: " + project);
            }

            {
                var p1 = new PointF(1.0f, 1.0f);
                var p2 = new PointF(1.0f, 2.0f);
                var project = VectorUtil.Project(p1, p2);
                Console.WriteLine("Project: " + project);
            }
        }

        static void BenchProject()
        {
            var a = new Vector2[BenchSize];
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < BenchSize; i++)
            {
                var v1 = new Vector2(1.0, 1.0);
                var v2 = new Vector2(1.0, 2.0);
                var project = v1.Project(v2);
                a[i] = project;
            }
            sw.Stop();
            Console.WriteLine("Project: " + sw.Elapsed.TotalMilliseconds + "ms");
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
            {
                var a = new Vector2(100.0, 100.0);
                var b = new Vector2(100.0, 200.0);
                var distance = a.Distance(b);
                Console.WriteLine("Distance: " + distance);
            }

            {
                var a = new PointF(100.0f, 100.0f);
                var b = new PointF(100.0f, 200.0f);
                var distance = LineUtil.Distance(a.X, a.Y, b.X, b.Y);
                Console.WriteLine("Distance: " + distance);
            }
        }

        static void TestMiddle()
        {
            {
                var a = new Vector2(100.0, 100.0);
                var b = new Vector2(100.0, 200.0);
                var middle = a.Middle(b);
                Console.WriteLine("Middle: " + middle);
            }

            {
                var a = new PointF(100.0f, 100.0f);
                var b = new PointF(100.0f, 200.0f);
                var p = new PointF();
                LineUtil.Middle(ref p, a.X, a.Y, b.X, b.Y);
                Console.WriteLine("Middle: " + p);
            }
        }

        static void TestNearestPointOnLine()
        {
            {
                var a = new Vector2(100.0, 100.0);
                var b = new Vector2(100.0, 200.0);
                var p = new Vector2(50.0, 150.0);
                var nearest = p.NearestPointOnLine(a, b);
                Console.WriteLine("NearestPointOnLine: " + nearest);
            }

            {
                var a = new PointF(100.0f, 100.0f);
                var b = new PointF(100.0f, 200.0f);
                var p = new PointF(50.0f, 150.0f);
                var nearest = VectorUtil.NearestPointOnLine(a, b, p);
                Console.WriteLine("NearestPointOnLine: " + nearest);
            }
        }
    }
}
