using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTouchMovement : MonoBehaviour
{
    private PlayerNodeMovement m_PlayerNodeMovement;

    private void Awake()
    {
        m_PlayerNodeMovement = GameObject.FindObjectOfType<PlayerNodeMovement>();
    }

    private void Start()
    {
        BoxCollider2D _bc = gameObject.AddComponent<BoxCollider2D>();
        _bc.size = new Vector2(1.8f, 1.8f);

    }

    private void OnMouseDown()
    {
        FindShortestPath(m_PlayerNodeMovement.GetCurrentNode(), gameObject.GetComponent<Node>());
    }

    private void FindShortestPath(Node origin, Node destination)
    {
        int f = 0;
        int g = 0;
        int h = 0;






        List<Node> openList = new List<Node>();
    }




}
