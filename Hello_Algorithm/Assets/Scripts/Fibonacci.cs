using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fibonacci : MonoBehaviour
{
    [SerializeField]
    GameObject CreateObject;


    int?[] memoizationCache;
    private void Awake()
    {
        memoizationCache = new int?[2];
        memoizationCache[0] = 0; // 0번째 항
        memoizationCache[1] = 1; // 1번째 항
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GenerateMemoFibonacci(30));
        StartCoroutine(GenerateTabulationFibonacci(30, (int v) =>
        {
            Debug.Log(v);
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //메모이제이션 방법
    public int GenerateMemoFibonacci(int number)
    {
        if (number < 1) return 0;
        if (memoizationCache.Length < number) Array.Resize(ref memoizationCache, number + 1);
        int MemoFibonacciRecursive(int number)
        {
            var value1 = memoizationCache[number - 1] ?? MemoFibonacciRecursive(number - 1);
            var value2 = memoizationCache[number - 2] ?? MemoFibonacciRecursive(number - 2);
            memoizationCache[number] = value1 + value2;
            return memoizationCache[number].Value;
        }

        return MemoFibonacciRecursive(number);
    }

    // 타불레이션 방법
    public IEnumerator GenerateTabulationFibonacci(uint number, Action<int> Actfunc, YieldInstruction yieldInstruction = null)
    {
        if (yieldInstruction == null) yieldInstruction = new WaitForSeconds(0.05f);
        int beforeValue = 0;  // 첫번째 항의 수열
        int afterValue = 1;  // 두번째 항의 수열
        int idx = 2;
        while(idx <= number)
        {
            yield return yieldInstruction;
            int temp = beforeValue + afterValue;
            beforeValue = afterValue;
            afterValue = temp;
            Actfunc(afterValue);
            idx++;
        }
    }
}
