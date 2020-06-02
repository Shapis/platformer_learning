using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHighlightScript : MonoBehaviour
{

    [SerializeField] private Material m_OutlineHighlight;

    [SerializeField] Color[] myColors;

    private Material tempMaterial;

    private GameObject myGameObject;




    private void Update()
    {
        if (myGameObject != null)
        {
            // if (!gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.GetComponent<DraggableScript>().LineOfSightLeniencySwitch)
            // {
            //     myGameObject.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", myColors[0]);
            // }
            // else
            // {
            //     myGameObject.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", myColors[1]);
            // }
        }
    }
    public void Begin(GameObject myGameObject)
    {
        this.myGameObject = myGameObject;
        tempMaterial = myGameObject.GetComponent<SpriteRenderer>().material;
        myGameObject.GetComponent<SpriteRenderer>().material = m_OutlineHighlight;
    }



    public void End()
    {
        myGameObject.GetComponent<SpriteRenderer>().material = tempMaterial;
    }
}
