using ChickenGames.Graph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNodeManager : MonoBehaviour
{
    [SerializeField]
    Vector2 size;
    [SerializeField]
    GameObject prefabNode;

    [SerializeField]
    GraphNodeState clickNodeState;

    GraphNodeObject startPointNode, destinationNode;
    List<GraphNodeObject> nodes = new List<GraphNodeObject>();

    public GraphNodeState ClickNodeState { 
        get => clickNodeState;
        set
        {
            clickNodeState = value;
            Debug.Log($"ClickNode: {clickNodeState}");
        }
    }

    private void Awake()
    {
        for (var i = 0; i < size.y; i++)
        {
            for (var j = 0; j < size.x; j++)
            {
                var node = Instantiate(prefabNode);
                node.transform.position = new Vector2(j, i);
                node.transform.SetParent(transform, true);
                nodes.Add(node.GetComponent<GraphNodeObject>());
            }
        }

        startPointNode = nodes[0];
        destinationNode = nodes[nodes.Count - 1];
        startPointNode.GraphNodeState = GraphNodeState.STARTPOINT;
        destinationNode.GraphNodeState = GraphNodeState.DESTINATION;
        EventManager.Instance.AddListener<NodeClickEvent>(NodeClickEvent);


        clickNodeState = GraphNodeState.STARTPOINT;

    }

    void CreateNeighborRoad()
    {
        void TryAddBlock(GraphNodeObject targetNode, int x, int y)
        {
            if (0 <= x && x < (int)size.x &&
                0 <= y && y < (int)size.y)
            {
                var node = nodes[y * (int)size.y + x];
                if(node.GraphNodeState != GraphNodeState.OBSTACLE)
                {
                    
                    var mag = targetNode.GetDistance(node);
                    targetNode.Roads.Add(node, mag);
                }
            }
        }

        for (var i = 0; i < nodes.Count; i++)
        {
            int x = i % (int)size.y;
            int y = i / (int)size.y;

            nodes[i].Roads = new Dictionary<GraphNodeObject, float>();
            if (nodes[i].GraphNodeState == GraphNodeState.OBSTACLE) continue;

            TryAddBlock(nodes[i], x - 1, y - 1);
            TryAddBlock(nodes[i], x - 1, y);
            TryAddBlock(nodes[i], x - 1, y + 1);

            TryAddBlock(nodes[i], x, y - 1);
            TryAddBlock(nodes[i], x, y);
            TryAddBlock(nodes[i], x, y + 1);

            TryAddBlock(nodes[i], x + 1, y - 1);
            TryAddBlock(nodes[i], x + 1, y);
            TryAddBlock(nodes[i], x + 1, y + 1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ClickNodeState = GraphNodeState.NONE;
        if (Input.GetKeyDown(KeyCode.Alpha2)) ClickNodeState = GraphNodeState.OBSTACLE;
        if (Input.GetKeyDown(KeyCode.Alpha3)) ClickNodeState = GraphNodeState.STARTPOINT;
        if (Input.GetKeyDown(KeyCode.Alpha4)) ClickNodeState = GraphNodeState.DESTINATION;
        if (Input.GetKeyDown(KeyCode.Alpha5)) CreateNeighborRoad();
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Dictionary<GraphNodeObject, GraphNodeObject> prevs= new Dictionary<GraphNodeObject, GraphNodeObject>();
            GraphAlgorithm.AStarRun(nodes, startPointNode, destinationNode, prevs);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Dictionary<GraphNodeObject, GraphNodeObject> prevs= new Dictionary<GraphNodeObject, GraphNodeObject>();
            StartCoroutine(GraphAlgorithm.AStarRunCor(nodes, startPointNode, destinationNode, prevs));
        } 
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Clear();
        }
    }

    void Clear()
    {
        nodes.ForEach(node => node.ColorUpdate());
    }


    public void NodeClickEvent(NodeClickEvent evt)
    {
        if (evt.node == startPointNode || evt.node == destinationNode) return;

        switch (clickNodeState)
        {
            case GraphNodeState.STARTPOINT:
                startPointNode.GraphNodeState = GraphNodeState.NONE;
                startPointNode = evt.node;
                startPointNode.GraphNodeState = GraphNodeState.STARTPOINT;
                break;
            case GraphNodeState.DESTINATION:
                destinationNode.GraphNodeState = GraphNodeState.NONE;
                destinationNode = evt.node;
                destinationNode.GraphNodeState = GraphNodeState.DESTINATION;
                break;
            default:
                evt.node.GraphNodeState = clickNodeState;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (var i = 0; i < nodes.Count; i++)
        {
            foreach(var n in nodes[i].Roads.Keys)
            {
                var start = nodes[i].transform.position;
                var dest = n.transform.position;
                Gizmos.DrawLine(start, dest);
            }
        }
    }
}
