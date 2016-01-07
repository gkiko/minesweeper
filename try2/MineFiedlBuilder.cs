using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal class MineFiedlBuilder
    {
        [Pure]
        public static MineField GenerateRandom(int minesCount, Size size)
        {
            var mines = CreateRandomMines(minesCount, size);
            var warings = CalculateWarnings(mines, size);
            return new MineField(size, mines, warings);
        }

        [Pure]
        private static IReadOnlySet<Point> CreateRandomMines(int minesCount, Size size)
        {
            return Enumerable
                .Repeat(new Random(), minesCount)
                .Select(r => new Point(
                    r.Next(size.Width),
                    r.Next(size.Height)))
                .ToReadOnlySet();
        }

        [Pure]
        private static IReadOnlyDictionary<Point, int> CalculateWarnings(IEnumerable<Point> mines, Size size)
        {
            return 
                mines
                    .SelectMany(m => m.Neighbours(size))
                    .GroupBy(p => p)
                    .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}