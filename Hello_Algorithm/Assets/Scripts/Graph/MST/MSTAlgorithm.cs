using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ChickenGames.Graph
{
    public static partial class MSTAlgorithm
    {
        public static IEnumerator MSTRun(
            List<GraphNodeObject> nodes,
            List<GraphNodeObject> prevs, // 선행 노드들 기록
            YieldInstruction yi = null
            )
        {
            if (yi == null) yi = new WaitForSeconds(0.005f);
            yield return yi;
        }
    }
}