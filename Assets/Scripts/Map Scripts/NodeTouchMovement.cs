using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NodeTouchMovement : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        NodePathFinding NPF = new NodePathFinding();

        foreach (var item in NPF.FindPath(
                GameObject.FindObjectOfType<PlayerNodeMovement>().GetCurrentNode(),

                this.gameObject.GetComponent<Node>(),

                GameObject.FindObjectsOfType<Node>().Cast<Node>().ToList()
                 ))
        {
            Debug.Log(item);
        }

        Debug.Log("----");
    }
}