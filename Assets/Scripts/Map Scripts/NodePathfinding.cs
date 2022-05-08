using System;
using System.Collections.Generic;
using UnityEngine;

public class NodePathFinding
{
    private List<Node> openList;
    private List<Node> closedList;

    public List<Node> FindPath(Node originNode, Node destinationNode, List<Node> AllNodes)
    {
        // Add origin to the openList
        openList = new List<Node> { originNode };
        closedList = new List<Node>();


        foreach (var item in AllNodes)
        {
            item.g = int.MaxValue;
            item.h = int.MaxValue;
            item.f = int.MaxValue;
            item.previousPathNode = null;
        }

        originNode.g = 0;
        originNode.h = CalculateDistanceCost(originNode, destinationNode);
        originNode.f = CalculateF(originNode);

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList);
            if (currentNode == destinationNode)
            {
                return GetPath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            foreach (Node neighbourNode in GetNeighboursList(currentNode, AllNodes))
            {
                if (closedList.Contains(neighbourNode)) continue;

                float tentativeGCost = currentNode.g + CalculateDistanceCost(currentNode, neighbourNode);

                if (tentativeGCost < neighbourNode.g)
                {
                    neighbourNode.previousPathNode = currentNode;
                    neighbourNode.g = tentativeGCost;
                    neighbourNode.h = CalculateDistanceCost(neighbourNode, destinationNode);
                    neighbourNode.f = CalculateF(neighbourNode);

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    private List<Node> GetNeighboursList(Node currentNode, List<Node> AllNodes)
    {
        List<Node> neighbours = new List<Node>();


        if (currentNode.m_UpDestination != null)
        {
            neighbours.Add(currentNode.m_UpDestination.GetComponent<Node>());
        }
        if (currentNode.m_DownDestination != null)
        {
            neighbours.Add(currentNode.m_DownDestination.GetComponent<Node>());
        }
        if (currentNode.m_LeftDestination != null)
        {
            neighbours.Add(currentNode.m_LeftDestination.GetComponent<Node>());
        }
        if (currentNode.m_RightDestination != null)
        {
            neighbours.Add(currentNode.m_RightDestination.GetComponent<Node>());
        }
        return neighbours;
    }

    private List<Node> GetPath(Node endNode)
    {
        List<Node> path = new List<Node> { endNode };

        Node currentNode = endNode;

        while (currentNode.previousPathNode != null)
        {
            path.Add(currentNode.previousPathNode);
            currentNode = currentNode.previousPathNode;
        }
        path.Reverse();
        return path;
    }


    private float CalculateF(Node node)
    {
        return node.g + node.h;
    }

    private float CalculateDistanceCost(Node nodeA, Node nodeB)
    {
        return Vector3.Distance(nodeA.transform.position, nodeB.transform.position);
    }

    private Node GetLowestFCostNode(List<Node> nodes)
    {
        Node lowestFCostNode = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i].f < lowestFCostNode.f)
            {
                lowestFCostNode = nodes[i];
            }
        }
        return lowestFCostNode;
    }
}