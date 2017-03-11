using System;
using System.Collections.Generic;
using System.Text;

namespace KBS.Portals.Calculator.Logic.Models
{
    class SortKey : IComparable
    {
        DateTime Date { get; set; }
        int Serial { get; set; }

        public SortKey (DateTime date, int serial)
        {
            this.Date = date;
            this.Serial = serial;
        }

        // The following code is to allow clases to be compared in Dictioary order

        int IComparable.CompareTo(Object obj)
        {
            if (obj == null) return 1;

            SortKey otherSortKey = obj as SortKey;
            if (otherSortKey != null) 
                if (this.Equals(obj)) {
                    return 0;
                } else { return 1; }
            else
                throw new ArgumentException("Object is not a Calculator.SortKey");
        }


        public bool Equals(SortKey x, SortKey y)
        {
            return x.Date == y.Date && x.Serial == y.Serial;
        }

        public int GetHashCode(SortKey x)
        {
            return x.Date.GetHashCode() + x.Serial.GetHashCode();
        }

    }
}
