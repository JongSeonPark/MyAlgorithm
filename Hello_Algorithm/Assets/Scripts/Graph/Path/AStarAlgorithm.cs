using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenGames.Graph
{
    public static partial class GraphAlgorithm
    {
        public static Dictionary<GraphNodeObject, float> AStarRun(
            List<GraphNodeObject> nodes,
            GraphNodeObject startPoint,
            GraphNodeObject endPoint,
            Dictionary<GraphNodeObject, GraphNodeObject> prevs // 선행 노드들 기록
            )
        {
            Dictionary<GraphNodeObject, float> minDists = new Dictionary<GraphNodeObject, float>();
            foreach (var entry in nodes) {
                minDists.Add(entry, float.MaxValue);
            }

            minDists[startPoint] = 0;
            prevs.Add(startPoint, null);
            

            // open은 나중에 피보나치 heap, 우선순위 큐로 변경
            List<GraphNodeCandidate> open = new List<GraphNodeCandidate>();
            open.Add(new GraphNodeCandidate(startPoint, 0, endPoint));

           
            while (open.Count != 0)
            {
                GraphNodeCandidate candidate = open[0];
                open.RemoveAt(0);

                GraphNodeObject n = candidate.Node;

                float minDist = minDists[n];
                float dist = candidate.Weight;
               
                if (minDist < dist) continue;

                Dictionary<GraphNodeObject, float> roads = n.Roads;

                foreach (var entry in roads)
                {
                    GraphNodeObject next = entry.Key;
                    float weight = entry.Value + endPoint.GetDistance(next);
                    float newDist = minDist + weight;

                    float nextMinDist = minDists[next];
                    if (newDist >= nextMinDist) continue;

                    minDists[next] = newDist;
                    prevs[next] = n;

                    if (next == endPoint) return minDists;
                    GraphNodeCandidate newCandidate = new GraphNodeCandidate(next, newDist, endPoint);
                    next.SettingVisitColor();
                    open.Add(newCandidate);
                    open.Sort((s1, s2) =>
                    {
                        if (s1.Weight > s2.Weight) return 1;
                        else if (s1.Weight < s2.Weight) return -1;
                        return 0;
                    });
                }
            }

            return minDists;
        }

        public static IEnumerator AStarRunCor(
            List<GraphNodeObject> nodes,
            GraphNodeObject startPoint,
            GraphNodeObject endPoint,
            Dictionary<GraphNodeObject, GraphNodeObject> prevs // 선행 노드들 기록
            )
        {
            Dictionary<GraphNodeObject, float> minDists = new Dictionary<GraphNodeObject, float>();
            foreach (var entry in nodes)
            {
                minDists.Add(entry, float.MaxValue);
            }

            minDists[startPoint] = 0;
            prevs.Add(startPoint, null);


            // open은 나중에 피보나치 heap, 우선순위 큐로 변경
            List<GraphNodeCandidate> open = new List<GraphNodeCandidate>();
            open.Add(new GraphNodeCandidate(startPoint, 0, endPoint));
            startPoint.SettingVisitColor();

            while (open.Count != 0)
            {
                GraphNodeCandidate candidate = open[0];
                open.RemoveAt(0);

                GraphNodeObject n = candidate.Node;

                float minDist = minDists[n];
                float dist = candidate.Weight;

                if (minDist < dist) continue;

                Dictionary<GraphNodeObject, float> roads = n.Roads;

                foreach (var entry in roads)
                {
                    GraphNodeObject next = entry.Key;
                    float weight = entry.Value;
                    float newDist = minDist + weight;
                    float nextMinDist = minDists[next];
                    if (newDist >= nextMinDist) continue;

                    minDists[next] = newDist;
                    prevs[next] = n;

                    if (next == endPoint)
                    {
                        GraphNodeObject path = prevs[endPoint];
                        while (path != startPoint)
                        {
                            path.SettingPathColor();
                            path = prevs[path];
                        }
                        startPoint.SettingPathColor();

                        yield break;
                    }

                    GraphNodeCandidate newCandidate = new GraphNodeCandidate(next, newDist, endPoint);
                    next.SettingVisitColor();
                    open.Add(newCandidate);
                    
                    yield return new WaitForSeconds(0.005f);
                }
                open.Sort((s1, s2) =>
                {
                    if (s1.Sum > s2.Sum) return 1;
                    else if (s1.Sum < s2.Sum) return -1;
                    return 0;
                });
            }

        }
    }


    public class GraphNodeCandidate
    {
        GraphNodeObject node;
        float weight;
        float sqrDist;
        public GraphNodeCandidate(GraphNodeObject node, float weight, GraphNodeObject endPoint)
        {
            this.node = node;
            this.weight = weight;
            sqrDist = node.GetDistance(endPoint);
        }

        public GraphNodeObject Node { get => node; set => node = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Sum { get => weight + sqrDist; }
    }


}

