
namespace System
{
    // SAT (Separating Axis Theorem): http://www.codezealot.org/archives/55
    // Sample: https://code.google.com/p/dyn4j/

    public interface IShape
    {
        Vector2[] Vertices { get; set; }
    }

    public class Polygon : IShape
    { 
        public Vector2[] Vertices { get; set; }
    }

    public struct Projection
    {
        public double Min { get; private set; }
        public double Max { get; private set; }

        public Projection(double min, double max)
            : this()
        {
            this.Min = min;
            this.Max = max;
        }

        public bool Overlap(Projection p)
        {
            return !(this.Min > p.Max || p.Min > this.Max);
        }

        public double GetOverlap(Projection p) 
        {
            return !this.Overlap(p) ? 
                0.0 : 
                Math.Abs(Math.Max(this.Min, p.Min) - Math.Min(this.Max, p.Max));
        }

        public bool Contains(Projection p)
        {
            return (this.Min <= p.Min && this.Max >= p.Max);
        }
    }

    public struct MinimumTranslationVector
    {
        public Vector2 Smallest { get; private set; }
        public double Overlap { get; private set; }

        public MinimumTranslationVector(Vector2 smallest, double overlap)
            : this()
        {
            this.Smallest = smallest;
            this.Overlap = overlap;
        }
    }

    public class SeparatingAxisTheorem
    {
        public Vector2[] GetAxes(IShape shape)
        {
            Vector2[] axes = new Vector2[shape.Vertices.Length];
            // loop over the vertices
            for (int i = 0; i < shape.Vertices.Length; i++) 
            {
                // get the current vertex
                Vector2 p1 = shape.Vertices[i];
                // get the next vertex
                Vector2 p2 = shape.Vertices[i + 1 == shape.Vertices.Length ? 0 : i + 1];
                // subtract the two to get the edge vector
                Vector2 edge = p1.Subtract(p2);
                // get either perpendicular vector
                Vector2 normal = edge.Perpendicular();
                // the perpendicular method is just (x, y) => (-y, x) or (y, -x)
                axes[i] = normal;
            }
            return axes;
        }

        public Projection Project(IShape shape, Vector2 axis)
        {
            double min = axis.Dot(shape.Vertices[0]);
            double max = min;
            for (int i = 1; i < shape.Vertices.Length; i++) 
            {
                // NOTE: the axis must be normalized to get accurate projections
                double p = axis.Dot(shape.Vertices[i]);
                if (p < min) 
                {
                    min = p;
                } 
                else if (p > max) 
                {
                    max = p;
                }
            }
            return new Projection(min, max);
        }

        public bool Overlap(IShape shape1, IShape shape2)
        {
            Vector2[] axes1 = GetAxes(shape1);
            Vector2[] axes2 = GetAxes(shape2);
            // loop over the axes1
            for (int i = 0; i < axes1.Length; i++) 
            {
                Vector2 axis = axes1[i];
                // project both shapes onto the axis
                Projection p1 = Project(shape1, axis);
                Projection p2 = Project(shape2, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    return false;
                }
            }
            // loop over the axes2
            for (int i = 0; i < axes2.Length; i++) 
            {
                Vector2 axis = axes2[i];
                // project both shapes onto the axis
                Projection p1 = Project(shape1, axis);
                Projection p2 = Project(shape2, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    return false;
                }
            }
            // if we get here then we know that every axis had overlap on it
            // so we can guarantee an intersection
            return true;
        }

        public bool MinimumTranslationVector(
            IShape shape1, 
            IShape shape2, 
            out MinimumTranslationVector? mtv)
        {
            double overlap =  Double.PositiveInfinity; // really large value;
            Vector2 smallest = default(Vector2);
            Vector2[] axes1 = GetAxes(shape1);
            Vector2[] axes2 = GetAxes(shape2);
            // loop over the axes1
            for (int i = 0; i < axes1.Length; i++) 
            {
                Vector2 axis = axes1[i];
                // project both shapes onto the axis
                Projection p1 = Project(shape1, axis);
                Projection p2 = Project(shape2, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    mtv = null;
                    return false;
                } 
                else 
                {
                    // get the overlap
                    double o = p1.GetOverlap(p2);
                    // check for minimum
                    if (o < overlap)
                    {
                        // then set this one as the smallest
                        overlap = o;
                        smallest = axis;
                    }
                }
            }
            // loop over the axes2
            for (int i = 0; i < axes2.Length; i++)
            {
                Vector2 axis = axes2[i];
                // project both shapes onto the axis
                Projection p1 = Project(shape1, axis);
                Projection p2 = Project(shape2, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    mtv = null;
                    return false;
                } 
                else 
                {
                    // get the overlap
                    double o = p1.GetOverlap(p2);
                    // check for minimum
                    if (o < overlap) 
                    {
                        // then set this one as the smallest
                        overlap = o;
                        smallest = axis;
                    }
                }
            }
            mtv = new MinimumTranslationVector(smallest, overlap);
            // if we get here then we know that every axis had overlap on it
            // so we can guarantee an intersection
            return true;
        }

        public bool MinimumTranslationVectorWithContainment(
            IShape shape1, 
            IShape shape2, 
            out MinimumTranslationVector? mtv)
        {
            double overlap =  Double.PositiveInfinity; // really large value;
            Vector2 smallest = default(Vector2);
            Vector2[] axes1 = GetAxes(shape1);
            Vector2[] axes2 = GetAxes(shape2);
            // loop over the axes1
            for (int i = 0; i < axes1.Length; i++) 
            {
                Vector2 axis = axes1[i];
                // project both shapes onto the axis
                Projection p1 = Project(shape1, axis);
                Projection p2 = Project(shape2, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    mtv = null;
                    return false;
                } 
                else 
                {
                    // get the overlap
                    double o = p1.GetOverlap(p2);
                    // check for containment
                    if (p1.Contains(p2) || p2.Contains(p1)) 
                    {
                        // get the overlap plus the distance from the minimum end points
                        double mins = Math.Abs(p1.Min - p2.Min);
                        double maxs = Math.Abs(p1.Max - p2.Max);
                        // NOTE: depending on which is smaller you may need to
                        // negate the separating axis!!
                        if (mins < maxs) 
                        {
                            o += mins;
                        }
                        else 
                        {
                            o += maxs;
                        }
                    }
                    // check for minimum
                    if (o < overlap)
                    {
                        // then set this one as the smallest
                        overlap = o;
                        smallest = axis;
                    }
                }
            }
            // loop over the axes2
            for (int i = 0; i < axes2.Length; i++)
            {
                Vector2 axis = axes2[i];
                // project both shapes onto the axis
                Projection p1 = Project(shape1, axis);
                Projection p2 = Project(shape2, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    mtv = null;
                    return false;
                } 
                else 
                {
                    // get the overlap
                    double o = p1.GetOverlap(p2);
                    // check for containment
                    if (p1.Contains(p2) || p2.Contains(p1)) 
                    {
                        // get the overlap plus the distance from the minimum end points
                        double mins = Math.Abs(p1.Min - p2.Min);
                        double maxs = Math.Abs(p1.Max - p2.Max);
                        // NOTE: depending on which is smaller you may need to
                        // negate the separating axis!!
                        if (mins < maxs) 
                        {
                            o += mins;
                        }
                        else 
                        {
                            o += maxs;
                        }
                    }
                    // check for minimum
                    if (o < overlap) 
                    {
                        // then set this one as the smallest
                        overlap = o;
                        smallest = axis;
                    }
                }
            }
            mtv = new MinimumTranslationVector(smallest, overlap);
            // if we get here then we know that every axis had overlap on it
            // so we can guarantee an intersection
            return true;
        }
    }
}
