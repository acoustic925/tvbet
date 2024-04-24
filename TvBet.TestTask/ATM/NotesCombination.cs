using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvBet.TestTask.ATM
{
    internal struct NotesCombination : IEquatable<NotesCombination>, IComparable<NotesCombination>
    {
        public int Sum {  get; set; }

        public int[] Notes { get; set; }

        public static NotesCombination Empty => new()
        {
            Sum = 0,
            Notes = Array.Empty<int>()
        };

        public override bool Equals(object obj) => obj is NotesCombination other && this.Equals(other);

        public bool Equals(NotesCombination nres) => Sum == nres.Sum;

        public override int GetHashCode() => (Sum).GetHashCode();

        public int CompareTo(NotesCombination other)
        {
            return Sum.CompareTo(other.Sum);
        }

        public static bool operator ==(NotesCombination lhs, NotesCombination rhs) => lhs.Equals(rhs);

        public static bool operator !=(NotesCombination lhs, NotesCombination rhs) => !(lhs == rhs);
    }
}
