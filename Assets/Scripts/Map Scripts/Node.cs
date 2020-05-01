using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, INodeable
{
    [Header("Level Info")]
    [SerializeField] private string m_LevelName;
    [SerializeField] private SceneHandler.Scene m_Scene;

    [Header("Node Destinations")]

    [SerializeField] public GameObject m_UpDestination;
    [SerializeField] public GameObject m_DownDestination;
    [SerializeField] public GameObject m_LeftDestination;
    [SerializeField] public GameObject m_RightDestination;
    [SerializeField] public bool IsTravelNode;
    [SerializeField] public bool IsAccessible = true;


    private void Start()
    {
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
