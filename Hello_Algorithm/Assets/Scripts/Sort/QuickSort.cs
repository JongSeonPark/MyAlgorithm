using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenGames.Sort
{
    public static partial class Sort
    {
        public static void QuickSortTest()
        {
            int[] arr = new int[10]
            {
                0, 9, 7, 4, 3, 5, 6, 3, 8, 1
            };

            arr.QuickSort();
            for (int i = 0; i < arr.Length; i++)
            {
                Debug.Log(arr[i]);
            }
        }

        public static void QuickSort<T>(this T[] arr, Func<T, T, int> comparer = null) where T : IComparable
        {
            if (comparer == null)
                comparer = (T a, T b) => a.CompareTo(b);

            void QuickSortRecursive(T[] arr, int left, int right)
            {
                if (left >= right) return;

                int pivot = Partition(arr, left, right);

                QuickSortRecursive(arr, left, pivot - 1);
                QuickSortRecursive(arr, pivot + 1, right);
            }

            int Partition(T[] arr, int left, int right)
            {
                int pivot = left;
                for(int i = left; i < right - 1; i++)
                {
                    var compare = comparer(arr[i], arr[right]);
                    if (compare < 0)
                    {
                        var temp = arr[i];
                        arr[i] = arr[pivot];
                        arr[pivot] = temp;
                        pivot++;
                    }
                }
                
                return pivot;
            }

            QuickSortRecursive(arr, 0, arr.Length - 1);
        }
    }
}