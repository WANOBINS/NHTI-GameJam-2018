using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScoreManager;

namespace Assets.Scripts.Util
{
    public static class HiScoreListExtensions
    {
        public static HiScore FindLowest(this List<HiScore> hiScores)
        {
            HiScore currentLowest = null;
            foreach(HiScore hiScore in hiScores)
            {
                if(currentLowest == null || currentLowest.Score < hiScore.Score)
                {
                    currentLowest = hiScore;
                }
            }
            return currentLowest;
        }
    }
}
