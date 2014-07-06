
namespace System
{
    public struct Vector2
    {
        #region Properties

        public double X { get; private set; }
        public double Y { get; private set; }

        #endregion

        #region Vectors

        public static Vector2 One { get { return new Vector2(1.0); } }
        public static Vector2 Zero { get { return new Vector2(0.0); } }
        public static Vector2 UnitX { get { return new Vector2(1.0, 0.0); } }
        public static Vector2 UnitY { get { return new Vector2(0.0, 1.0); } }

        #endregion

        #region Constructor

        public Vector2(double value)
            : this()
        {
            this.X = value;
            this.Y = value;
        }

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
            return string.Concat(X, System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, Y);
        }

        #endregion

        #region Arithmetic

        public Vector2 Substract(Vector2 v)
        {
            return new Vector2(this.X - v.X, this.Y - v.Y);
        }

        public Vector2 Add(Vector2 v)
        {
            return new Vector2(this.X + v.X, this.Y + v.Y);
        }

        public Vector2 Multiply(double scalar)
        {
            return new Vector2(
                this.X * scalar,
                this.Y * scalar);
        }

        public Vector2 Divide(double scalar)
        {
            return new Vector2(
                this.X / scalar,
                this.Y / scalar);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return v1.Substract(v2);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return v1.Add(v2);
        }

        public static Vector2 operator *(Vector2 v, double scalar)
        {
            return v.Multiply(scalar);
        }

        public static Vector2 operator /(Vector2 v, double scalar)
        {
            return v.Divide(scalar);
        }

        public double Dot(Vector2 v)
        {
            return (this.X * v.X) + (this.Y * v.Y);
        }

        public double Angle(Vector2 v)
        {
            return Math.Acos(this.Dot(v));
        }

        public double Cross(Vector2 v)
        {
            return (this.X * v.Y) - (this.Y * v.X);
        }

        #endregion

        #region Vector

        public double Magnitude()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public double MagnitudeSquared()
        {
            return this.X * this.X + this.Y * this.Y;
        }

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
            return this / this.Length();
        }

        public Vector2 Projection(Vector2 v)
        {
            return v * (this.Dot(v) / v.Dot(v));
        }

        /*
        public double Component(Vector2 v)
        {
            return this.Dot(v.Normalize());
        }

        public Vector2 Projection(Vector2 v)
        {
            return v.Normalize() * this.Component(v);
        }
        */

        #endregion

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
            return (this - a).Projection(b - a) + a;
        }

        #endregion

        #region Math

        public const double RadiansToDegrees = 180.0 / Math.PI;
        public const double DegreesToRadians = Math.PI / 180.0;

        public static double Clamp(double value, double min, double max)
        {
            return value > max ? max : value < min ? min : value;
        }

        #endregion
    }
}
