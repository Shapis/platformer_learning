using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class NodeTouchMovement : MonoBehaviour, INodeMovementEvents
{

    public event EventHandler<List<Node>> OnNodeTouchedEvent;

    private PlayerNodeMovement m_PlayerNodeMovement;

    private void Start()
    {
        m_PlayerNodeMovement = GameObject.FindObjectOfType<PlayerNodeMovement>()
        ;

        BoxCollider2D _bc = gameObject.AddComponent<BoxCollider2D>();
        _bc.size = new Vector2(2f, 2f);

    }

    private void OnMouseUp()
    {
        OnNodeTouched(this, GetNodePathList());
    }

    private List<Node> GetNodePathList()
    {
        NodePathFinding NPF = new NodePathFinding();
        return NPF.FindPath(
                        GameObject.FindObjectOfType<PlayerNodeMovement>().GetCurrentNode(),

                        this.gameObject.GetComponent<Node>(),

                        GameObject.FindObjectsOfType<Node>().Cast<Node>().ToList()
                         );
    }

    public void OnInitialDestinationNodeLoaded(object sender, GameObject nodeInfo)
    {
        throw new System.NotImplementedException();
    }

    public void OnTravelNodeReached(object sender, GameObject nodeInfo)
    {
        throw new System.NotImplementedException();
    }

    public void OnTravelNodeDeparted(object sender, GameObject nodeInfo)
    {
        throw new System.NotImplementedException();
    }

    public void OnDestinationNodeReached(object sender, GameObject nodeInfo)
    {
        throw new System.NotImplementedException();
    }

    public void OnDestinationNodeDeparted(object sender, GameObject nodeInfo)
    {
        throw new System.NotImplementedException();
    }

    public void OnDestinationNotAccessible(object sender, GameObject nodeInfo)
    {
        throw new System.NotImplementedException();
    }

    public void OnNoDestinationFound(object sender, string s)
    {
        throw new System.NotImplementedException();
    }

    public void OnNodeTouched(object sender, List<Node> nodePath)
    {
        OnNodeTouchedEvent?.Invoke(this, nodePath);
    }
}