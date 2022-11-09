﻿using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count == 1)
            {
                outPoints = points;
            }
            else
            {
                int pointA, pointB, comparedPoint, leftAndRightRotation = 0, colinear=0;
                Line ab;
                List<Point> toBeRemovedPoints = new List<Point>();
                for (pointA = 0; pointA < points.Count; pointA++)
                {
                    for (pointB = pointA + 1; pointB < points.Count; pointB++)
                    {
                        ab = new Line(points[pointA], points[pointB]);
                        for (comparedPoint = 0; comparedPoint < points.Count; comparedPoint++)
                        {
                            if (points[comparedPoint] != points[pointA] && points[comparedPoint] != points[pointB])
                            {
                                switch (HelperMethods.CheckTurn(ab, points[comparedPoint]))
                                {
                                    case Enums.TurnType.Left: leftAndRightRotation--; break;
                                    case Enums.TurnType.Right: leftAndRightRotation++; break;
                                    default:
                                        colinear++;
                                        if (HelperMethods.PointOnSegment(points[comparedPoint], points[pointA], points[pointB]))
                                        {
                                            toBeRemovedPoints.Add(points[comparedPoint]);
                                        }
                                        break;
                                }
                            }
                        }
                        if (Math.Abs(leftAndRightRotation) == (points.Count - 2 - colinear))
                        {
                            if (!outPoints.Contains(points[pointA]))
                            {
                                outPoints.Add(points[pointA]);
                            }
                            if (!outPoints.Contains(points[pointB]))
                            {
                                outPoints.Add(points[pointB]);
                            }
                        }
                        leftAndRightRotation = 0;
                        colinear = 0;
                    }
                }
                for (int size = 0; size < toBeRemovedPoints.Count; size++)
                {
                    outPoints.Remove(toBeRemovedPoints[size]);
                }
            }
        }
        
        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}
