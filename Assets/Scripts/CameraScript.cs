using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_Cinemachine;
    void Start()
    {
        m_Cinemachine.Follow = GameObject.Find("Player").transform;
    }
}
