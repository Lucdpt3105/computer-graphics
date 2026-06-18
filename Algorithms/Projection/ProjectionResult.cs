using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Geometry;
using Project_CG_Paint.CoreModel.Model;

namespace Project_CG_Paint.Algorithms.Projection
{

    public class ProjectionResult
    {
        public List<Edge<Point2D>> VisibleEdges { get; }

        public List<Edge<Point2D>> HiddenEdges { get; }

        public ProjectionResult(List<Edge<Point2D>> visibleEdges, List<Edge<Point2D>> hiddenEdges)
        {
            VisibleEdges = visibleEdges;
            HiddenEdges = hiddenEdges;
        }
    }
}
