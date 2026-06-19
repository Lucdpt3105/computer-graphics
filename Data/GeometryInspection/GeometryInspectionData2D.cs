using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.CoreModel.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.GeometryInspection
{
    public class GeometryInspectionData2D
    {
        public List<Point2D> OriginalVertices { get; set; } = new List<Point2D>();
        public List<Point2D> CurrentVertices { get; set; } = new List<Point2D>();
        public List<EdgeIndex> Edges { get; set; } = new List<EdgeIndex>();
        public List<NamePoint<Point2D>> ControlPoints { get; set; } = new List<NamePoint<Point2D>>();
        public void ResetCurrentToOriginal()
        {
            CurrentVertices.Clear();
            CurrentVertices = new List<Point2D>(OriginalVertices);

            for (int i = 0; i < ControlPoints.Count; i++)
            {
                ControlPoints[i].CurrentValue =
                    ControlPoints[i].OriginalValue;
            }
        }

        public void Clear()
        {
            OriginalVertices.Clear();
            CurrentVertices.Clear();
            Edges.Clear();
            ControlPoints.Clear();
        }
    }
}
