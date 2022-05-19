using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenGames.Algorithm.Sort
{
    public static partial class Sort
    {
        public static void QuickSort<T>(this T[] values) where T : IComparable => QuickFuction(values, 0, values.Length);

        public static void QuickFuction<T>(T[] values, int left, int right) where T : IComparable
        {
            if (left >= right) return;
            var pivot = Partition(values, left, right);
            QuickFuction(values, left, pivot - 1);
            QuickFuction(values, pivot + 1, right);
        }

        /// <summary>
        /// 크면 아무것도 하지 않는다.
        /// 작았을 때, 
        /// pivot++
        /// 현재 비교값과 pivot값을 변경
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int Partition<T>(T[] values, int left, int right) where T : IComparable
        {
            var pivot = left;
            for (var i = left; i < right - 1; i++)
            {
                /// 0 = 같음
                /// 1 = values[i]가 더 큼
                /// -1 = values[pivot]가 더 큼
                int compare = values[i].CompareTo(values[right - 1]);
                if (compare < 0)
                {
                    var temp = values[i];
                    values[i] = values[pivot];
                    values[pivot] = temp;
                    pivot++;
                }
            }
            return pivot;
        }
    }
}
