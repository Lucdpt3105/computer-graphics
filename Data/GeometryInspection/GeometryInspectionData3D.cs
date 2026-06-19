using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.GeometryInspection
{
    public class GeometryInspectionData3D
    {
        public List<Point3D> OriginalVertices { get; set; } = new List<Point3D>();
        public List<Point3D> CurrentVertices { get; set; } = new List<Point3D>();
        public List<EdgeIndex> Edges { get; set; } = new List<EdgeIndex>();
        public List<NamePoint<Point3D>> ControlPoints { get; set; } = new List<NamePoint<Point3D>>();
        public void ResetCurrentToOriginal()
        {
            CurrentVertices.Clear();
            CurrentVertices = new List<Point3D>(OriginalVertices);

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
