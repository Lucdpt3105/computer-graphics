using Project_CG_Paint.Algorithms.Transform;
using Project_CG_Paint.CoreModel.Model;
using Project_CG_Paint.Data.Objects;
using Project_CG_Paint.Data.Shapes2D;
using Project_CG_Paint.Data.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Project_CG_Paint.Forms.Transform
{
    public partial class TransformForm : Form
    {
        private readonly List<GraphicObject> _objects;
        private readonly Action<GraphicObject, Matrix3x3, TransformType, Dictionary<string, double>> _applyTransform;

        public TransformForm()
            : this(new List<GraphicObject>(), null, null)
        {
        }

        public TransformForm(
            IEnumerable<GraphicObject> objects,
            GraphicObject selectedObject,
            Action<GraphicObject, Matrix3x3, TransformType, Dictionary<string, double>> applyTransform)
        {
            InitializeComponent();

            _objects = objects?.ToList() ?? new List<GraphicObject>();
            _applyTransform = applyTransform;

            ConfigureInputs();
            LoadObjects(selectedObject);
            WireEvents();
        }

        private void ConfigureInputs()
        {
            NumericUpDown[] numericInputs =
            {
                inputOffsetX, inputOffsetY,
                inputPivotRotateX, inputPivotRotateY, inputRotateAngle,
                inputScaleX, inputScaleY,
                inputPivotReflectX, inputPivotReflectY,
                inputStartLineReflectX, inputStartLineReflectY,
                inputEndLineReflectX, inputEndLineReflectY
            };

            foreach (NumericUpDown input in numericInputs)
            {
                input.Minimum = -10000;
                input.Maximum = 10000;
                input.DecimalPlaces = 2;
                input.Increment = 1;
            }

            inputScaleX.Value = 1;
            inputScaleY.Value = 1;
            btnRotateSelectPivot.Checked = false;
            btnRotateEnterPivot.Checked = false;
            btnReflectSelectPivot.Checked = false;
            btnReflectEnterPivot.Checked = false;
            btnReflectSelectLine.Checked = false;
            btnReflectEnterLine.Checked = false;

            SetPlaceholder(inputSelectRotatePivot, "Select pivot...");
            SetPlaceholder(inputSelectPivotScale, "Select pivot...");
            SetPlaceholder(inputSelectReflectPivot, "Select pivot...");
            SetPlaceholder(inputSelectReflectLine, "Select line...");
        }

        private void LoadObjects(GraphicObject selectedObject)
        {
            currentObject.Items.Clear();
            inputSelectRotatePivot.Items.Clear();
            inputSelectPivotScale.Items.Clear();
            inputSelectReflectPivot.Items.Clear();
            inputSelectReflectLine.Items.Clear();

            foreach (GraphicObject obj in _objects)
            {
                ObjectListItem item = new ObjectListItem(obj);
                currentObject.Items.Add(item);

                if (obj is Shape2D shape2D)
                {
                    inputSelectRotatePivot.Items.Add(new PivotListItem(obj, shape2D.Pivot));
                    inputSelectPivotScale.Items.Add(new PivotListItem(obj, shape2D.Pivot));
                    inputSelectReflectPivot.Items.Add(new PivotListItem(obj, shape2D.Pivot));

                    if (shape2D is LineShape line)
                        inputSelectReflectLine.Items.Add(new LineListItem(obj, line.Start, line.End));
                }

                if (ReferenceEquals(obj, selectedObject))
                    currentObject.SelectedItem = item;
            }

            if (currentObject.SelectedIndex < 0 && currentObject.Items.Count > 0)
                currentObject.SelectedIndex = 0;

            SetPlaceholder(inputSelectRotatePivot, "Select pivot...");
            SetPlaceholder(inputSelectPivotScale, "Select pivot...");
            SetPlaceholder(inputSelectReflectPivot, "Select pivot...");
            SetPlaceholder(inputSelectReflectLine, "Select line...");
        }

        private void WireEvents()
        {
            btnApply.Click += btnApply_Click;
            btnClose.Click += (sender, e) => Close();
            btnClear.Click += (sender, e) => ClearSelections();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (_applyTransform == null)
            {
                Close();
                return;
            }

            GraphicObject target = GetSelectedObject();
            if (!(target is Shape2D shape2D))
            {
                MessageBox.Show("TransformForm currently applies to 2D shapes selected in PaintForm.", "No 2D object", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<TransformCommand> commands = BuildCommands(shape2D);
            if (commands.Count == 0)
            {
                MessageBox.Show("Please select at least one transform option.", "No transform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (TransformCommand command in commands)
            {
                _applyTransform(target, command.Matrix, command.Type, command.Parameters);
            }
        }

        private List<TransformCommand> BuildCommands(Shape2D selectedShape)
        {
            List<TransformCommand> commands = new List<TransformCommand>();

            if (btnTranslate.Checked)
            {
                double offsetX = (double)inputOffsetX.Value;
                double offsetY = (double)inputOffsetY.Value;
                commands.Add(new TransformCommand(
                    TransformComposer2D.BuildTranslationByOffset(new Point2D(offsetX, offsetY)),
                    TransformType.Translate,
                    new Dictionary<string, double> { ["OffsetX"] = offsetX, ["OffsetY"] = offsetY }));
            }

            if (btnRotatePivot.Checked)
            {
                Point2D pivot = btnRotateSelectPivot.Checked ? GetSelectedPivot(inputSelectRotatePivot, selectedShape.Pivot) : ReadPoint(inputPivotRotateX, inputPivotRotateY);
                double angle = (double)inputRotateAngle.Value;
                commands.Add(new TransformCommand(
                    TransformComposer2D.BuildRotationByPoint(pivot, angle),
                    TransformType.Rotate,
                    new Dictionary<string, double> { ["PivotX"] = pivot.X, ["PivotY"] = pivot.Y, ["Angle"] = angle }));
            }

            if (btnRotateOrigin.Checked)
            {
                double angle = (double)inputRotateAngle.Value;
                commands.Add(new TransformCommand(
                    TransformComposer2D.BuildRotationByPoint(new Point2D(0, 0), angle),
                    TransformType.Rotate,
                    new Dictionary<string, double> { ["PivotX"] = 0, ["PivotY"] = 0, ["Angle"] = angle }));
            }

            if (btnScale.Checked)
            {
                Point2D pivot = GetSelectedPivot(inputSelectPivotScale, selectedShape.Pivot);
                double scaleX = (double)inputScaleX.Value;
                double scaleY = (double)inputScaleY.Value;
                commands.Add(new TransformCommand(
                    TransformComposer2D.BuildScaleByPoint(pivot, scaleX, scaleY),
                    TransformType.Scale,
                    new Dictionary<string, double> { ["PivotX"] = pivot.X, ["PivotY"] = pivot.Y, ["ScaleX"] = scaleX, ["ScaleY"] = scaleY }));
            }

            if (btnReflectPivot.Checked)
            {
                Point2D pivot = btnReflectSelectPivot.Checked ? GetSelectedPivot(inputSelectReflectPivot, selectedShape.Pivot) : ReadPoint(inputPivotReflectX, inputPivotReflectY);
                commands.Add(new TransformCommand(
                    TransformComposer2D.BuildReflectionByPoint(pivot),
                    TransformType.Reflect,
                    new Dictionary<string, double> { ["PivotX"] = pivot.X, ["PivotY"] = pivot.Y }));
            }

            if (btnReflectLine.Checked)
            {
                LineListItem selectedLine = inputSelectReflectLine.SelectedItem as LineListItem;
                Point2D start = btnReflectSelectLine.Checked && selectedLine != null
                    ? selectedLine.Start
                    : ReadPoint(inputStartLineReflectX, inputStartLineReflectY);
                Point2D end = btnReflectSelectLine.Checked && selectedLine != null
                    ? selectedLine.End
                    : ReadPoint(inputEndLineReflectX, inputEndLineReflectY);

                commands.Add(new TransformCommand(
                    TransformComposer2D.BuildReflectionByLine(start, end),
                    TransformType.Reflect,
                    new Dictionary<string, double> { ["StartX"] = start.X, ["StartY"] = start.Y, ["EndX"] = end.X, ["EndY"] = end.Y }));
            }

            return commands;
        }

        private GraphicObject GetSelectedObject()
        {
            return (currentObject.SelectedItem as ObjectListItem)?.Object;
        }

        private static Point2D ReadPoint(NumericUpDown xInput, NumericUpDown yInput)
        {
            return new Point2D((double)xInput.Value, (double)yInput.Value);
        }

        private static Point2D GetSelectedPivot(ComboBox comboBox, Point2D fallback)
        {
            return comboBox.SelectedItem is PivotListItem item ? item.Pivot : fallback;
        }

        private void ClearSelections()
        {
            btnTranslate.Checked = false;
            btnRotatePivot.Checked = false;
            btnRotateOrigin.Checked = false;
            btnScale.Checked = false;
            btnReflectPivot.Checked = false;
            btnReflectLine.Checked = false;
            btnRotateSelectPivot.Checked = false;
            btnRotateEnterPivot.Checked = false;
            btnReflectSelectPivot.Checked = false;
            btnReflectEnterPivot.Checked = false;
            btnReflectSelectLine.Checked = false;
            btnReflectEnterLine.Checked = false;
            inputOffsetX.Value = 0;
            inputOffsetY.Value = 0;
            inputRotateAngle.Value = 0;
            inputScaleX.Value = 1;
            inputScaleY.Value = 1;
            SetPlaceholder(inputSelectRotatePivot, "Select pivot...");
            SetPlaceholder(inputSelectPivotScale, "Select pivot...");
            SetPlaceholder(inputSelectReflectPivot, "Select pivot...");
            SetPlaceholder(inputSelectReflectLine, "Select line...");
        }

        private static void SetPlaceholder(ComboBox comboBox, string placeholder)
        {
            comboBox.SelectedIndex = -1;
            comboBox.Text = placeholder;
        }

        private class TransformCommand
        {
            public Matrix3x3 Matrix { get; }
            public TransformType Type { get; }
            public Dictionary<string, double> Parameters { get; }

            public TransformCommand(Matrix3x3 matrix, TransformType type, Dictionary<string, double> parameters)
            {
                Matrix = matrix;
                Type = type;
                Parameters = parameters;
            }
        }

        private class ObjectListItem
        {
            public GraphicObject Object { get; }

            public ObjectListItem(GraphicObject obj)
            {
                Object = obj;
            }

            public override string ToString()
            {
                return Object.Metadata.Name;
            }
        }

        private class PivotListItem
        {
            public GraphicObject Object { get; }
            public Point2D Pivot { get; }

            public PivotListItem(GraphicObject obj, Point2D pivot)
            {
                Object = obj;
                Pivot = pivot;
            }

            public override string ToString()
            {
                return $"{Object.Metadata.Name} Pivot ({Pivot.X:F2}, {Pivot.Y:F2})";
            }
        }

        private class LineListItem
        {
            public GraphicObject Object { get; }
            public Point2D Start { get; }
            public Point2D End { get; }

            public LineListItem(GraphicObject obj, Point2D start, Point2D end)
            {
                Object = obj;
                Start = start;
                End = end;
            }

            public override string ToString()
            {
                return $"{Object.Metadata.Name} Line";
            }
        }
    }
}
