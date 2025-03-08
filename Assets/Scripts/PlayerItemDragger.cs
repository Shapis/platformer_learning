using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDragger : MonoBehaviour, IDraggableEvents
{
    [Header("Dependencies")]
    [SerializeField] private Transform m_WandTransform;
    [SerializeField] private LayerMask m_WhatAreBarriers;
    [SerializeField] private MobileJoystick m_MobileJoystick;

    private InputHandler m_InputHandler;

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
    }
    private GameObject selectedObject = null;
    private GameObject closestBlockingObject = null;
    private float leniencyTimer = 0f;
    private float leniencyTime = 1.5f;
    private float rotationTimer = 0f;
    private bool initiallyInLos = false; // ! This isn't a good name for this variable.
    private bool lineOfSightForGood = false; // ! This isn't a good name for this variable either.

    private void Awake()
    {
        m_MobileJoystick = FindObjectOfType<MobileJoystick>();
        m_InputHandler = FindObjectOfType<InputHandler>();
    }
    private void Start()
    {
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        m_InputHandler.OnMouseButtonLeftUnpressedEvent += OnMouseButtonLeftUnpressed;
        m_InputHandler.OnMouseHoverEvent += OnMouseHover;
    }

    private void RotateTheObjectToFaceUpwards()
    {
        selectedObject.transform.rotation = Quaternion.Slerp(selectedObject.transform.rotation, Quaternion.Euler(0, 0, 0), rotationTimer / 1.5f);
        rotationTimer += Time.deltaTime;
    }

    private void OnMouseHover(object sender, Vector2 mousePosition)    // This will run every frame that an object is being held until the mouse left button is left go off.
    {
        if (selectedObject != null && LineOfSightCheck(m_WandTransform.position, selectedObject.transform.position) == null && !lineOfSightForGood)
        {
            if (closestBlockingObject != null)
            {
                OnLineOfSightUnblocked(this, EventArgs.Empty);
                closestBlockingObject = null;
            }
            leniencyTimer = 0f;
            initiallyInLos = true;
            DragObjectToMousePointer(mousePosition);
            RotateTheObjectToFaceUpwards();
            DrawDebugRaycasts(mousePosition);
        }
        else if (selectedObject != null && leniencyTimer < leniencyTime && initiallyInLos)
        {
            leniencyTimer += Time.deltaTime;
            DragObjectToMousePointer(mousePosition);
            RotateTheObjectToFaceUpwards();
            DrawDebugRaycasts(mousePosition);
            if (closestBlockingObject != LineOfSightCheck(m_WandTransform.position, selectedObject.transform.position))
            {
                DraggingEventArgs lineOfSightArgs = new DraggingEventArgs()
                {
                    OriginGameObject = m_WandTransform.gameObject,
                    TargetGameObject = selectedObject,
                    ClosestBlockingGameObject = LineOfSightCheck(m_WandTransform.position, selectedObject.transform.position),
                };
                OnLineOfSightBlocked(this, lineOfSightArgs);
            }
            closestBlockingObject = LineOfSightCheck(m_WandTransform.position, selectedObject.transform.position);
        }
        else if (selectedObject != null && leniencyTimer < leniencyTime && !initiallyInLos)
        {
            leniencyTimer += Time.deltaTime;
        }
        else if (selectedObject != null && leniencyTimer != 5000)
        {
            initiallyInLos = false;
            lineOfSightForGood = true;
            leniencyTimer = 5000;
            OnDraggingEnds(this, EventArgs.Empty);
        }
    }

    private void OnMouseButtonLeftPressed(object sender, Vector2 mousePosition)
    {
        // Get all objects that are at the position of the pointer click and return them to an array.
        RaycastHit2D[] hitInfoArray = Physics2D.RaycastAll(GetPointerAsWorldPoint(mousePosition), Vector2.zero);
        selectedObject = null;
        //closestBlockingObject = null; // ? Not sure if this is necessary, putting it here just in case ?
        leniencyTimer = 0f;
        lineOfSightForGood = false;

        // Run through all the objects that were at the position of the pointer click, and add the one with the highest sorting 
        // order to the selectedObject variable.
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

        // Checks if a MobileJoystick exists and if it does then check if the touch is inside of the MobileJoystick
        if (m_MobileJoystick != null && m_MobileJoystick.IsTheTouchInsideJoystick(mousePosition))
        {
            selectedObject = null;
        }


        DraggingEventArgs myDraggingEventArgs = new DraggingEventArgs();
        if (selectedObject != null)
        {
            myDraggingEventArgs = new DraggingEventArgs
            {
                OriginGameObject = m_WandTransform.gameObject,
                TargetGameObject = selectedObject,
                ClosestBlockingGameObject = LineOfSightCheck(m_WandTransform.position, selectedObject.transform.position),
            };
            closestBlockingObject = myDraggingEventArgs.ClosestBlockingGameObject;
            OnDraggingBegins(this, myDraggingEventArgs);
        }
    }

    private void DrawDebugRaycasts(Vector2 mousePosition)
    {
        Debug.DrawRay(m_WandTransform.position, selectedObject.transform.position - m_WandTransform.position);

        Debug.DrawRay(m_WandTransform.position, GetPointerAsWorldPoint(mousePosition) - m_WandTransform.position);
    }

    private void DragObjectToMousePointer(Vector2 mousePosition)
    {
        // calc velocity necessary to follow the mouse pointer
        var vel = ((GetPointerAsWorldPoint(mousePosition)) - selectedObject.transform.position) * m_DraggingSpeed;

        // lower the offset so it centers on the pointer over time
        // offset *= 0.92f;

        // limit max velocity to avoid pass through objects
        if (vel.magnitude > m_DraggingSpeed)
        {
            vel *= m_DraggingSpeed / vel.magnitude;
        }

        // set object velocity
        selectedObject.GetComponent<Rigidbody2D>().linearVelocity = vel;
    }

    private GameObject LineOfSightCheck(Vector3 origin, Vector3 target)
    {
        List<GameObject> myClosestBlockingObjects = new List<GameObject>();

        RaycastHit2D[] hitInfoArray = Physics2D.RaycastAll(origin, target - origin, Vector2.Distance(origin, target));
        foreach (var o in hitInfoArray)
        {
            // if any of the objects in the hitInfoArray are in the m_WhatAreBarriers layer, return that the selectedObject is out of line of sight.
            if (m_WhatAreBarriers == (m_WhatAreBarriers | (1 << o.transform.gameObject.layer)))
            {
                myClosestBlockingObjects.Add(o.transform.gameObject);
            }
        }

        // Loops through all the objects in myClosestBlockObjects and determines the myClosestBlockingObject
        GameObject myClosestBlockingObject = null;
        foreach (var o in myClosestBlockingObjects)
        {
            if (myClosestBlockingObject == null)
            {
                myClosestBlockingObject = o;
            }
            else
            {
                if (Vector2.Distance(myClosestBlockingObject.transform.position, origin) > Vector2.Distance(o.transform.position, origin))
                {
                    myClosestBlockingObject = o;
                }
            }
        }

        return myClosestBlockingObject; // Returns the closest blocking object.
    }

    private void OnMouseButtonLeftUnpressed(object sender, Vector2 e)
    {
        if (selectedObject != null)
        {
            OnDraggingEnds(this, EventArgs.Empty);
        }
    }

    public void OnDraggingBegins(object sender, DraggingEventArgs draggingEventArgs)
    {
        OnDraggingBeginsEvent?.Invoke(this, draggingEventArgs);
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        selectedObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        selectedObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        rotationTimer = 0f;
        initiallyInLos = false;
        selectedObject = null;
        OnDraggingEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    private Vector3 GetPointerAsWorldPoint(Vector2 pointerPosition)
    {
        return Camera.main.ScreenToWorldPoint(pointerPosition);
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
