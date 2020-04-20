using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{

    [SerializeField] private float m_SawRotationSpeed = 150;


    // Update is called once per frame
    void FixedUpdate()
    {

        gameObject.GetComponent<Transform>().Rotate(0, 0, m_SawRotationSpeed*Time.deltaTime);

    }
}
