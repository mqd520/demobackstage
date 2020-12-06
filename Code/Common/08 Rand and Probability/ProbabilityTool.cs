using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Probability Tool
    /// </summary>
    public static class ProbabilityTool
    {
        /// <summary>
        /// Is Hit
        /// </summary>
        /// <param name="percent">percent: (0 - 100)</param>
        /// <param name="rand">rand</param>
        /// <returns></returns>
        public static bool IsHit(int percent, ref int rand)
        {
            if (percent < 0)
            {
                percent = 0;
                return false;
            }

            if (percent > 100)
            {
                percent = 100;
                return true;
            }

            int val = RandTool.CreateRandValWithMinMax(0, Int32.MaxValue);

            rand = val;

            if (val % percent == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
