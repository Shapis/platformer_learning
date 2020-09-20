using System;
using System.Collections;
using UnityEngine;

public class DraggableRepulsion : MonoBehaviour, IDraggableEvents
{
    [SerializeField] private PlayerItemDragger m_PlayerItemDragger;
    private GameObject selectedObject = null;
    private bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerItemDragger.OnDraggingBeginsEvent += OnDraggingBegins;
        m_PlayerItemDragger.OnDraggingEndsEvent += OnDraggingEnds;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (selectedObject != null && inRange)
        {
            if ((selectedObject.GetComponent<Rigidbody2D>().velocity.y > 0) && this.transform.position.y > selectedObject.transform.position.y)
            {
                selectedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(selectedObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            if ((selectedObject.GetComponent<Rigidbody2D>().velocity.y < 0) && this.transform.position.y < selectedObject.transform.position.y)
            {
                selectedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(selectedObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            if ((selectedObject.GetComponent<Rigidbody2D>().velocity.x > 0) && this.transform.position.x > selectedObject.transform.position.x)
            {
                selectedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, selectedObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            if ((selectedObject.GetComponent<Rigidbody2D>().velocity.x < 0) && this.transform.position.x < selectedObject.transform.position.x)
            {
                selectedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, selectedObject.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (selectedObject == other.gameObject)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }

    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        selectedObject = draggingEventArgs.TargetGameObject;
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        selectedObject = null;
    }

    public void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        throw new NotImplementedException();
    }

    public void OnLineOfSightUnblocked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
