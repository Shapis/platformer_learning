using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour, INodeable
{
    [Header("Corner Destination")]
    [SerializeField] public GameObject m_Destination;
    [SerializeField] public GameObject m_DestinationReverse;

}
