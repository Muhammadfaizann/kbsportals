using System;
using System.Collections.Generic;
using System.Text;

namespace KBS.Portals.Calculator.Logic.Models
{
    public class SortKey : IComparable<SortKey>, IComparable
    {
        public DateTime Date { get; }
        public int Serial { get; }

        public SortKey (DateTime date, int serial)
        {
            Date = date;
            Serial = serial;
        }
        
        public int CompareTo(object obj)
        {
            if (obj != null && !(obj is SortKey))
            {
                throw new ArgumentException("Object must be of type SortKey.");
            }

            return CompareTo((SortKey)obj);           
        }

        public int CompareTo(SortKey other)
        {
            if (other == null) return 1;

            return Equals(other) ? 0 : 1;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SortKey);
        }

        public bool Equals(SortKey other)
        {
            if (other == null) { return false; }

            if (ReferenceEquals(this, other)) { return true; }

            return Date == other.Date && Serial == other.Serial;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hash = 17;
                
                hash = hash * 23 + Date.GetHashCode();
                hash = hash * 23 + Serial.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(SortKey item1, SortKey item2)
        {
            if ((object)item1 == null || (object)item2 == null) { return false; }

            if (ReferenceEquals(item1, item2)) { return true; }

            return item1.Date == item2.Date && item1.Serial == item2.Serial;
        }

        public static bool operator !=(SortKey item1, SortKey item2)
        {
            return !(item1 == item2);
        }

        //public bool Equals(SortKey x, SortKey y)
        //{
        //    return x.Date == y.Date && x.Serial == y.Serial;
        //}

        //public int GetHashCode(SortKey obj)
        //{
        //    return obj.Date.GetHashCode() + obj.Serial.GetHashCode();
        //}

    }
}
