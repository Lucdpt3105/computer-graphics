using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CG_Paint.Data.Animation
{
    public enum InterpolationMode
    {
        Step,
        Linear
    }
    public enum AnimationProperty2D
    {
        Position,
        Rotation,
        Scale
    }
    public class AnimationChannel<T>
    {
        private readonly List<KeyFrame<T>> _keyFrames = new List<KeyFrame<T>>();

        public AnimationProperty2D Property { get; }

        public bool IsEnabled { get; set; } = true;

        public InterpolationMode Interpolation { get; set; } = InterpolationMode.Linear;

        public IReadOnlyList<KeyFrame<T>> KeyFrames => _keyFrames;

        public bool HasKeyFrames => IsEnabled && _keyFrames.Count > 0;

        public AnimationChannel(AnimationProperty2D property)
        {
            Property = property;
        }

        public void AddKeyFrame(KeyFrame<T> keyFrame)
        {
            if (keyFrame == null)
            {
                throw new ArgumentNullException(
                    nameof(keyFrame));
            }

            int existingIndex =
                _keyFrames.FindIndex(
                    keyFrameItem =>
                        keyFrameItem.TimeSeconds ==
                        keyFrame.TimeSeconds);

            if (existingIndex >= 0)
            {
                _keyFrames[existingIndex] = keyFrame;
            }
            else
            {
                _keyFrames.Add(keyFrame);
            }

            _keyFrames.Sort(
                (left, right) =>
                    left.TimeSeconds.CompareTo(
                        right.TimeSeconds));
        }

        public bool RemoveKeyFrameAt(
            double timeSeconds)
        {
            int index =
                _keyFrames.FindIndex(
                    keyFrame =>
                        keyFrame.TimeSeconds ==
                        timeSeconds);

            if (index < 0)
            {
                return false;
            }

            _keyFrames.RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            _keyFrames.Clear();
        }
    }
}
