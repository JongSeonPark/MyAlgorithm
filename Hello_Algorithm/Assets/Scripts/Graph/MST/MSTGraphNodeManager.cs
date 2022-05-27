using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MSTGraphManagerState
{
    PEEKNODE,
    EREASENODE,
    LINKEDGE,
}
public class MSTGraphNodeManager : MonoBehaviour
{
    [SerializeField]
    Vector2 size;
    [SerializeField]
    GameObject prefabNode;

    [SerializeField]
    MSTGraphManagerState mstGraphManagerState;

    List<GraphNodeObject> nodes = new List<GraphNodeObject>();
    GraphNodeObject peekedNode1;

    public MSTGraphManagerState MSTGraphManagerState
    {
        get => mstGraphManagerState;
        set
        {
            peekedNode1 = null;
            switch (value)
            {
                case MSTGraphManagerState.PEEKNODE:
                    break;
                case MSTGraphManagerState.EREASENODE:
                    break;
                case MSTGraphManagerState.LINKEDGE:
                    break;
            }
            mstGraphManagerState = value;
            Debug.Log($"State: {mstGraphManagerState}");
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

        EventManager.Instance.AddListener<NodeClickEvent>(NodeClickEvent);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) MSTGraphManagerState = MSTGraphManagerState.PEEKNODE;
        if (Input.GetKeyDown(KeyCode.Alpha2)) MSTGraphManagerState = MSTGraphManagerState.EREASENODE;
        if (Input.GetKeyDown(KeyCode.Alpha3)) MSTGraphManagerState = MSTGraphManagerState.LINKEDGE;
    }

    public void NodeClickEvent(NodeClickEvent evt)
    {
        switch (MSTGraphManagerState)
        {
            case MSTGraphManagerState.PEEKNODE:
                evt.node.GraphNodeState = GraphNodeState.MSTNODE;
                break;
            case MSTGraphManagerState.EREASENODE:
                evt.node.GraphNodeState = GraphNodeState.NONE;
                break;
            case MSTGraphManagerState.LINKEDGE:
                if (peekedNode1.GraphNodeState != GraphNodeState.MSTNODE 
                    || peekedNode1 == evt.node) break;
                if (peekedNode1 == null)
                {
                    peekedNode1 = evt.node;
                }
                else
                {
                    var dist = evt.node.GetDistance(peekedNode1);
                    peekedNode1.Roads.Add(evt.node, dist);
                    evt.node.Roads.Add(peekedNode1, dist);
                    peekedNode1 = null;
                }
                break;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (var i = 0; i < nodes.Count; i++)
        {
            foreach (var n in nodes[i].Roads.Keys)
            {
                var start = nodes[i].transform.position;
                var dest = n.transform.position;
                Gizmos.DrawLine(start, dest);
            }
        }
    }
}
