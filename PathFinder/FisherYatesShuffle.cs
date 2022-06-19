using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    class FisherYatesShuffle
    {
        public static void Shuffle<T>(List<T> inp, Random rand)
        {
            //Variables.WriteLine($"Length: {inp.Count}");
            for (int i = 0; i < inp.Count-1; i++)
            {
                int r = i + rand.Next(inp.Count - i);
                T t = inp[r];
                inp[r] = inp[i];
                inp[i] = t;
            }
        }

        public static void Shuffle<T>(T[] inp, Random rand)
        {
            //Variables.WriteLine($"Length: {inp.Length}");
            for (int i = 0; i < inp.Length - 1; i++)
            {
                int r = i + rand.Next(inp.Length - i);
                T t = inp[r];
                inp[r] = inp[i];
                inp[i] = t;
            }
        }
    }
}
