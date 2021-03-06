﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MedicalDiamondSearch.Core.Helpers
{
    public struct PixelBlock
    {
        private readonly List<Pixel> _pixels;
        public Point Position { get; }

        public ICollection<Pixel> Pixels => _pixels;

        public PixelBlock(Point position, IEnumerable<Pixel> pixels)
        {
            Position = position;
            _pixels = pixels.ToList();
        }

        /// <summary>
        /// Cots function for calculating mean squared error.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public double BlockDistortion(PixelBlock block) => Pixels.Select((p, i) => new { p, i }).Sum(pi =>
        {
            var diff = pi.p - block._pixels[pi.i];
            return diff * diff;
        }) / Pixels.Count;

        /// <summary>
        /// Calculates vector between two blocks.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public Vector GetVector(PixelBlock block) => new Vector(Position, block.Position);

        public override bool Equals(object obj)
        {
            if (!(obj is PixelBlock pixelBlock))
                return false;
            return pixelBlock.Position.Equals(Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Position.X},{Position.Y})";
        }
    }
}
