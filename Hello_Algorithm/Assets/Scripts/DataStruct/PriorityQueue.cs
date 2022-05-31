using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenGames.DataStruct
{
    public class PriorityQueue<TValue> where TValue : IComparable
    {
        List<TValue> heap = new List<TValue>();
        IComparer<TValue> comparer = Comparer<TValue>.Default;

        public void Enqueue(TValue value)
        {
            heap.Add(value);
            var curIdx = heap.Count - 1;
            UpHeapUpdateRecursive(curIdx);

            void UpHeapUpdateRecursive(int idx)
            {
                int parentIdx = (idx + 1) / 2 - 1;
                if (parentIdx < 0) return;
                var parentValue = heap[parentIdx];
                if (comparer.Compare(heap[idx], parentValue) >= 0)
                {
                    heap[parentIdx] = heap[idx];
                    heap[idx] = parentValue;
                    UpHeapUpdateRecursive(parentIdx);
                }
            }
        }

        public TValue Dequeue()
        {
            var result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            DownHeapUpdateRecursive(0);
            return result;
            void DownHeapUpdateRecursive(int idx)
            {
                var leftIdx = (idx + 1) * 2 - 1;
                var rightIdx = (idx + 1) * 2;

                // 다음 자식 힙이 없는 경우
                if (leftIdx > heap.Count - 1) return;

                // left와고만 비교
                // 결국 마지막 인덱스임.
                var leftValue = heap[leftIdx];
                if (rightIdx > heap.Count - 1)
                {
                    if (comparer.Compare(heap[idx], leftValue) <= 0)
                    {
                        heap[leftIdx] = heap[idx];
                        heap[idx] = leftValue;
                    }
                    return;
                }
                var rightValue = heap[rightIdx];
                bool compareIsLeft = comparer.Compare(leftValue, rightValue) > 0;

                if (compareIsLeft)
                {
                    heap[leftIdx] = heap[idx];
                    heap[idx] = leftValue;
                    DownHeapUpdateRecursive(leftIdx);
                }
                else
                {
                    heap[rightIdx] = heap[idx];
                    heap[idx] = rightValue;
                    DownHeapUpdateRecursive(rightIdx);
                }

            }
        }
    }
}