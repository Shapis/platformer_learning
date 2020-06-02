using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDragger : MonoBehaviour, IDraggableEvents
{
    [Header("Dependencies")]
    [SerializeField] private InputHandler m_InputHandler;
    [SerializeField] private Transform m_WandTransform;
    [SerializeField] private LayerMask m_WhatAreBarriers;

    [Header("Settings")]
    [SerializeField] private float m_DraggingSpeed = 15f;
    [SerializeField] private float m_LineOfSightLeniencyTimer = 2f;
    public event EventHandler<DraggingEventArgs> OnDraggingBeginsEvent;
    public event EventHandler OnDraggingEndsEvent;
    public event EventHandler<DraggingEventArgs> OnLineOfSightBlockedEvent;
    public event EventHandler OnLineOfSightUnblockedEvent;
    public class DraggingEventArgs : EventArgs
    {
        public GameObject OriginGameObject { get; set; }
        public GameObject TargetGameObject { get; set; }
        public GameObject ClosestBlockingGameObject { get; set; }
        public float LineOfSightLeniencyTimer { get; set; }
    }
    private GameObject currentlyLineOfSightBlockingObject = null;
    private GameObject selectedObject = null;
    private bool lineOfSightSwitch = true;
    private bool lineOfSightLeniencySwitch = true;
    private bool brokeLineOfSightPermanently = false;
    private Vector3 offset;
    private bool isLineOfSightBlocked;
    private float timeCount = 0f;

    private void Start()
    {
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        m_InputHandler.OnMouseButtonLeftUnpressedEvent += OnMouseButtonLeftUnpressed;
        m_InputHandler.OnMouseHoverEvent += OnMouseHover;
    }

    private void OnMouseHover(object sender, Vector2 mousePosition)    // This will run every frame that an object is being held until the mouse left button is left go off.
    {
        if (selectedObject != null)
        {
            if (LineOfSightCheck())     // Will only allow objects to be dragged if line of sight doesnt break through grounds/barriers
            {
                DragObjectToMousePointer(mousePosition);
                RotateTheObjectToFaceUpwards();
                DrawDebugRaycasts(mousePosition);
            }
        }
    }

    private bool LineOfSightCheck()
    {
        RaycastHit2D[] hitInfoArray = Physics2D.RaycastAll(m_WandTransform.position, selectedObject.transform.position - m_WandTransform.position, Vector2.Distance(m_WandTransform.position, selectedObject.transform.position));
        List<GameObject> myGameObjects = new List<GameObject>();
        foreach (var o in hitInfoArray)
        {
            // if any of the objects in the hitInfoArray are in the m_WhatAreBarriers layer, return that the selectedObject is out of line of sight.
            if (m_WhatAreBarriers == (m_WhatAreBarriers | (1 << o.transform.gameObject.layer)))
            {
                myGameObjects.Add(o.transform.gameObject);
            }
        }

        GameObject myClosestBlockingObject = null;

        foreach (var o in myGameObjects)
        {
            if (myClosestBlockingObject == null)
            {
                myClosestBlockingObject = o;
            }
            else
            {
                if (
                Vector2.Distance(myClosestBlockingObject.transform.position, m_WandTransform.position) >
                Vector2.Distance(o.transform.position, m_WandTransform.position))
                {
                    myClosestBlockingObject = o;
                }
            }
        }

        if (myClosestBlockingObject != null && currentlyLineOfSightBlockingObject != myClosestBlockingObject)
        {
            currentlyLineOfSightBlockingObject = myClosestBlockingObject;
            DraggingEventArgs myLosArgs = new DraggingEventArgs { OriginGameObject = m_WandTransform.gameObject, TargetGameObject = selectedObject, ClosestBlockingGameObject = myClosestBlockingObject, LineOfSightLeniencyTimer = m_LineOfSightLeniencyTimer };
            lineOfSightSwitch = false;
            Invoke("SetLeniencySwitch", m_LineOfSightLeniencyTimer);
            OnLineOfSightBlocked(this, myLosArgs);
        }
        else if (myClosestBlockingObject == null && !lineOfSightSwitch)
        {
            CancelInvoke();
            lineOfSightSwitch = true;
            currentlyLineOfSightBlockingObject = null;
            OnLineOfSightUnblocked(this, EventArgs.Empty);
        }

        if (!lineOfSightLeniencySwitch && !brokeLineOfSightPermanently)
        {
            brokeLineOfSightPermanently = true;
            OnDraggingEnds(this, EventArgs.Empty);
        }

        return lineOfSightLeniencySwitch;
    }

    private void SetLeniencySwitch()
    {
        lineOfSightLeniencySwitch = false;
    }

    private void DrawDebugRaycasts(Vector2 mousePosition)
    {
        Debug.DrawRay(m_WandTransform.position, selectedObject.transform.position - m_WandTransform.position);

        Debug.DrawRay(m_WandTransform.position, GetMouseAsWorldPoint(mousePosition) - m_WandTransform.position);
    }

    private void DragObjectToMousePointer(Vector2 mousePosition)
    {
        // calc velocity necessary to follow the mouse pointer
        var vel = ((GetMouseAsWorldPoint(mousePosition) + offset) - selectedObject.transform.position) * m_DraggingSpeed;

        // lower the offset so it centers on the pointer over time
        offset *= 0.92f;

        // limit max velocity to avoid pass through objects
        if (vel.magnitude > m_DraggingSpeed)
        {
            vel *= m_DraggingSpeed / vel.magnitude;
        }

        // set object velocity
        selectedObject.GetComponent<Rigidbody2D>().velocity = vel;
    }

    private void RotateTheObjectToFaceUpwards()
    {
        selectedObject.transform.rotation = Quaternion.Slerp(selectedObject.transform.rotation, Quaternion.Euler(0, 0, 0), timeCount / 1.5f);
        timeCount += Time.deltaTime;
    }

    private void OnMouseButtonLeftUnpressed(object sender, Vector2 e)
    {
        if (selectedObject != null && !brokeLineOfSightPermanently)
        {
            OnDraggingEnds(this, EventArgs.Empty);
        }
    }

    private void OnMouseButtonLeftPressed(object sender, Vector2 mousePosition)
    {
        RaycastHit2D[] hitInfoArray = Physics2D.RaycastAll(GetMouseAsWorldPoint(mousePosition), Vector2.zero); // Get all objects that are at the position of the pointer click and return them to an array.

        // Run through all the objects that were at the position of the pointer click, and add the one with the highest sorting order to the mySelectedObject variable.
        foreach (var hitInfo in hitInfoArray)
        {
            if (hitInfo.transform.gameObject.GetComponent<Draggable>())
            {
                if (selectedObject == null)
                {
                    selectedObject = hitInfo.transform.gameObject;
                }
                else if (selectedObject.GetComponent<SpriteRenderer>().sortingOrder <= hitInfo.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder)
                {
                    selectedObject = hitInfo.transform.gameObject;
                }
            }
        }

        if (selectedObject != null)
        {
            offset = selectedObject.transform.position - GetMouseAsWorldPoint(mousePosition);
            DraggingEventArgs myDraggingEventArgs = new DraggingEventArgs { OriginGameObject = m_WandTransform.gameObject, TargetGameObject = selectedObject, ClosestBlockingGameObject = null, LineOfSightLeniencyTimer = m_LineOfSightLeniencyTimer };
            OnDraggingBegins(this, myDraggingEventArgs);
        }
    }

    public void OnDraggingBegins(object sender, DraggingEventArgs draggingEventArgs)
    {
        timeCount = 0f;
        lineOfSightLeniencySwitch = true;
        lineOfSightSwitch = true;
        brokeLineOfSightPermanently = false;
        currentlyLineOfSightBlockingObject = null;
        OnDraggingBeginsEvent?.Invoke(this, draggingEventArgs);
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        selectedObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        selectedObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        selectedObject = null;
        OnDraggingEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    private Vector3 GetMouseAsWorldPoint(Vector2 mousePoint)
    {
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnLineOfSightBlocked(object sender, DraggingEventArgs lineOfSightArgs)
    {
        OnLineOfSightBlockedEvent?.Invoke(this, lineOfSightArgs);
    }

    public void OnLineOfSightUnblocked(object sender, EventArgs e)
    {
        OnLineOfSightUnblockedEvent?.Invoke(this, EventArgs.Empty);
    }
}
