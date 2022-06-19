using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    class BinarySearch
    {
        public static int FindElement(List<int> list_, int toFind)
        {
            int l = 0;
            int r = list_.Count - 1;
            while (l <= r)
            {
                int mid = (l + r) / 2;
                if (list_[mid] < toFind) l = mid + 1;
                else if (list_[mid] > toFind) r = mid - 1;
                else return mid;
            }
            return -1;
        }

        public static int FindIndex(List<int> list_, int toAdd)
        {
            int l = 0;
            int r = list_.Count - 1;
            while (l <= r)
            {
                int mid = (l + r) / 2;
                if (list_[mid] < toAdd) l = mid + 1;
                else if (list_[mid] > toAdd) r = mid - 1;
                else return mid;
                //Variables.WriteLine($"L: {l}, r: {r}, mid: {mid}");
            }
            return -1 - l;
        }
    }
}
