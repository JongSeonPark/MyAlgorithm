using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenGames.Graph
{
    public static partial class GraphAlgorithm
    {
        public static Dictionary<string, int> Run(
            Dictionary<string, Node> nodes,
            string from,
            Dictionary<string, string> prevs // 선행 노드들 기록
            )
        {
            Dictionary<string, int> minDists = new Dictionary<string, int>();
            foreach (var entry in nodes)
            {
                minDists.Add(entry.Key, int.MaxValue);
            }

            minDists[from] = 0;
            prevs.Add(from, null);
            Queue<Candidate> open = new Queue<Candidate>();
            open.Enqueue(new Candidate(nodes[from], 0));

            while (open.Count != 0)
            {
                Candidate candidate = open.Dequeue();

                Node n = candidate.Node;
                string nodeName = n.Name;

                int minDist = minDists[nodeName];
                int dist = candidate.Dist;

                if (minDist < dist) continue;

                Dictionary<Node, int> roads = n.Roads;

                foreach (var entry in roads)
                {
                    Node next = entry.Key;
                    int weight = entry.Value;
                    int newDist = minDist + weight;

                    string nextName = next.Name;
                    int nextMinDist = minDists[nextName];
                    if (newDist >= nextMinDist) continue;

                    minDists.Add(nextName, newDist);
                    prevs.Add(nextName, nodeName);

                    Candidate newCandidate = new Candidate(next, newDist);
                    open.Enqueue(newCandidate);
                }
            }

            return minDists;
        }
    }

    public class Candidate
    {
        Node node;
        int dist;
        public Candidate(Node node, int dist)
        {
            this.node = node;
            this.dist = dist;
        }

        public Node Node { get => node; set => node = value; }
        public int Dist { get => dist; set => dist = value; }
    }

    public class Node
    {
        string name;
        Dictionary<Node, int> roads = new Dictionary<Node, int>();
        public Node(string name) => this.name = name;
        public Dictionary<Node, int> Roads { get => roads; }
        public string Name { get => name; }

        public void AddRoad(Node dest, int dist) => roads.Add(dest, dist);
        public int GetDistance(Node dest) => roads[dest];
    }
}

