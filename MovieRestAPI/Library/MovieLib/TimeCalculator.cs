using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib {
    public static class TimeCalculator { 
        public static int ConvertMinutesToSeconds(int minutes){
            if (minutes <= 0){
                throw new ArgumentOutOfRangeException();
            }

            return minutes * 60;
        }
    }
}
