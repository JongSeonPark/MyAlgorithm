using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GraphNodeState
{
    NONE,
    STARTPOINT,
    DESTINATION,
    OBSTACLE,
}

public static class GraphExtension
{
    public static Color HoverColor() => Color.yellow;
    public static Color VisitColor() => Color.cyan;
    public static Color PathColor() => Color.magenta;
    public static Color GetColor(this GraphNodeState state)
    {
        switch (state)
        {
            case GraphNodeState.STARTPOINT: return Color.red;
            case GraphNodeState.DESTINATION: return Color.blue;
            case GraphNodeState.OBSTACLE: return Color.black;
            case GraphNodeState.NONE: return Color.white;
            default:
                return Color.white;
        }
    }
}

public class NodeClickEvent : IEventBase
{
    public GraphNodeObject node;
}

public class GraphNodeObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GraphNodeState graphNodeState;

    Dictionary<GraphNodeObject, float> roads = new Dictionary<GraphNodeObject, float>();

    public GraphNodeState GraphNodeState { get => graphNodeState; set
        {
            graphNodeState = value;
            ColorUpdate();
        }
    }

    public Dictionary<GraphNodeObject, float> Roads { get => roads; set => roads = value; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Visit()
    {
        spriteRenderer.color = GraphExtension.VisitColor();
    }

    public void Path()
    {
        spriteRenderer.color = GraphExtension.PathColor();
    }

    public void ColorUpdate() => spriteRenderer.color = graphNodeState.GetColor();

    private void OnMouseOver()
    {
        spriteRenderer.color = GraphExtension.HoverColor();
    }

    private void OnMouseExit()
    {
        ColorUpdate();
    }

    private void OnMouseUpAsButton()
    {
        EventManager.Instance.SendEvent(new NodeClickEvent { node = this });
    }
}
