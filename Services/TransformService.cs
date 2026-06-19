using System;
using System.Collections.Generic;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Transform;

namespace Project_CG_Paint.Services
{
    internal class TransformService
    {
        public void ApplyTransform(Shape2D shape, Matrix3x3 matrix, TransformType type, Dictionary<string, double> parameters = null)
        {
            if (shape == null)
                throw new ArgumentNullException(nameof(shape));

            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            shape.CurrentMatrix.CurrentMatrix3x3 = shape.CurrentMatrix.CurrentMatrix3x3 * matrix;
            shape.TransformHistory.Enqueue(new TransformRecord
            {
                Type = type,
                TimeStamp = DateTime.Now,
                Parameters = parameters ?? new Dictionary<string, double>(),
                AppliedMatrix3x3 = matrix
            });
        }
    }
}
