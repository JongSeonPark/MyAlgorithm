using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGames.DataStruct;
public class TestComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PriorityQueueTest();
        CompareTest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PriorityQueueTest()
    {
        var pq = new PriorityQueue<int>();
        pq.Enqueue(5);
        pq.Enqueue(1);
        pq.Enqueue(7);
        pq.Enqueue(8);
        pq.Enqueue(2);
        pq.Enqueue(3);
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
    }

    void CompareTest()
    {
        Debug.Log(Comparer<int>.Default.Compare(1, 2)); // -1
        Debug.Log(Comparer<int>.Default.Compare(1, 1)); // 0
        Debug.Log(Comparer<int>.Default.Compare(2, 1)); // 1

        Debug.Log(1.CompareTo(2)); // -1
        Debug.Log(1.CompareTo(1)); // 0
        Debug.Log(2.CompareTo(1)); // 1
    }
}

