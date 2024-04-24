using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TvBet.TestTask.ATM
{
    internal class Atm
    {
        private readonly List<int> _balance;
        private readonly bool _isGreedyAlg;

        private Atm(List<int> balance, bool isGreedyAlg)
        {
            _balance = balance.OrderDescending().ToList();
            _isGreedyAlg = isGreedyAlg;
        }

        public static Atm CreateAtm(List<int> balance, List<int> denominations)
        {
            var isGreedyAlg = IsGreedyAlgorithm(denominations);

            return new Atm(balance, isGreedyAlg);
        }

        public int[] GetNotes(int amount) => _isGreedyAlg ? GreedyMethod(amount) : DynamicMethod(amount);

        private int[] GreedyMethod(int amount)
        {
            var notes = new List<int>();

            foreach (var note in _balance)
            {
                var diff = amount - note;
                if (diff < 0)
                    continue;

                notes.Add(note);
                amount -= note;

                if(diff == 0)
                {
                    return [.. notes];
                }
            }

            return [];
        }


        private int[] DynamicMethod(int amount)
        {
            var list = new SortedSet<NotesCombination>() { NotesCombination.Empty };

            foreach (var note in _balance)
            {
                if (note > amount)
                    continue;

                var newNotesList = list.ToArray().AsSpan();

                foreach (var item in newNotesList)
                {
                    var sum = item.Sum + note;

                    if (sum == amount)
                    {
                        return [.. item.Notes, note];
                    }

                    if (sum > amount)
                        break;

                    list.Add(new NotesCombination
                    {
                        Sum = sum,
                        Notes = [.. item.Notes, note]
                    });
                }
            }

            return [];
        }

        private static bool IsGreedyAlgorithm(List<int> denominations)
        {
            for(int i = 0; i < denominations.Count; i++)
            {
                for(int j = i + 1; j < denominations.Count; j++)
                {
                    if (denominations[j] % denominations[i] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
