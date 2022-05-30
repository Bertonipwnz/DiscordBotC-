using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Бункер2._0.Классы
{
    class Shuffle
    {

        public string[] ShuffleAndRandom(int razm, string[] A)
        {
            int razmer = razm - 1;
            MyRandom rnd = new MyRandom(0, razmer);
            string[] B = new string[razm];
            for (int i = 0; i < razmer; i++)
            {
                int j = rnd.Next();
                B[i] = A[j];
            }

            return B;

        }
    }
}
