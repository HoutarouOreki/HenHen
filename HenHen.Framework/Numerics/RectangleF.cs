﻿using System.Numerics;

namespace HenHen.Framework.Numerics
{
    public struct RectangleF
    {
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }

        public float Area => System.Math.Abs(Width * Height);
        public Vector2 Center => TopLeft + (Size / 2);

        /// <summary>
        /// If <see cref="Left"/> is greater than <see cref="Right"/>,
        /// this will be negative.
        /// Setter: sets <see cref="Right"/> to be
        /// equal to <see cref="Left"/> + <see cref="Width"/>.
        /// </summary>
        public float Width
        {
            get => Right - Left;
            set => Right = Left + value;
        }

        /// <summary>
        /// If <see cref="Top"/> is greater than <see cref="Bottom"/>,
        /// this will be negative.
        /// Setter: sets <see cref="Bottom"/> to be
        /// equal to <see cref="Top"/> + <see cref="Height"/>.
        /// </summary>
        public float Height
        {
            get => Bottom - Top;
            set => Bottom = Top + value;
        }

        /// <summary>
        /// Shorthand for <see cref="Width"/>
        /// and <see cref="Height"/>.
        /// </summary>
        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public Vector2 TopLeft
        {
            get => new Vector2(Left, Top);
            set
            {
                Top = value.Y;
                Left = value.X;
            }
        }

        public Vector2 BottomRight
        {
            get => new Vector2(Right, Bottom);
            set
            {
                Bottom = value.Y;
                Right = value.X;
            }
        }

        public override string ToString() => $"Left={Left},Top={Top},Right={Right},Bottom={Bottom}";
    }
}