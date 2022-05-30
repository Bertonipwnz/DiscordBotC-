using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бункер2._0.Классы
{
    class MyRandom
    {
        private int[] shuffled;
        private int index;
        private readonly int _start;

        public int Count { get; private set; }

        public MyRandom(int start, int end)
        {
            Count = end - start;
            Random r = new Random();
            shuffled = Enumerable.Range(start, Count).OrderBy(x => r.Next()).ToArray();
            _start = start;
        }
        public int Next()
        {
            if (index == shuffled.Length)
            {
                index = 0;
            }
            return shuffled[index++];
        }

        public void Shuffle()
        {
            Random r = new Random();
            shuffled = Enumerable.Range(_start, Count).OrderBy(x => r.Next()).ToArray();
        }
    }
}
