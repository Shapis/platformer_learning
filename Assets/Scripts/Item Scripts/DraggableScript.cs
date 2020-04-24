using System;
using UnityEngine;
using UnityEngine.Events;

public class DraggableScript : MonoBehaviour, IDraggableEvents
{
    private const float LevitatingMassScaling = 0.03f;

    float m_Speed = 5;

    float m_MaxSpeed = 15;

    float m_LineOfSightLeniency = 1.5f;

    private float m_Range;

    private Vector3 mOffset;

    private float mZCoord;

    private Rigidbody2D myRigidBody2D;

    private float objectMass;

    private GameObject myPlayer;

    public UnityEvent OnDraggingBeginsEvent; // When an object begins being dragged.

    public UnityEvent OnDraggingEndsEvent; // When an object begins being dragged.

    private float initialTime;

    private bool lineOfSightSwitch = false;

    private bool wasLetGoSwitch = false;

    private bool wasLetGoSwitch2 = false;

    public bool LineOfSightLeniencySwitch { get; set; }



    private float lineOfSightLeniencyInitialTime;

    private void Start()
    {
        myRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        myPlayer = GameObject.Find("Player");

        OnDraggingBeginsEvent?.AddListener(myPlayer.GetComponent<PlayerMagic>().OnDraggingBegins);

        OnDraggingEndsEvent?.AddListener(myPlayer.GetComponent<PlayerMagic>().OnDraggingEnds);

        OnDraggingBeginsEvent?.AddListener(myPlayer.GetComponent<PlayerMovement>().OnDraggingBegins);

        OnDraggingEndsEvent?.AddListener(myPlayer.GetComponent<PlayerMovement>().OnDraggingEnds);

        m_Range = myPlayer.GetComponent<PlayerMagic>().m_DraggingRange;

        objectMass = myRigidBody2D.mass;


    }

    void OnMouseDown()
    {
        if (myPlayer.GetComponent<CharacterController2D>().Grounded)
        {
            OnDraggingBegins();
        }
    }

    void OnMouseDrag()
    {

        //Debug.Log(myRigidBody2D.angularVelocity);
        //Debug.Log(transform.rotation.z);
        //Debug.Log(LineOfSightCheck());
        if (
            ((LineOfSightCheck() && lineOfSightSwitch) || LineOfSightLeniencySwitch) && wasLetGoSwitch2 && myPlayer.GetComponent<CharacterController2D>().Grounded)
        {
            DragObjectToMousePointer();

            RotateTheObjectToFaceUpwards();
        }
        else if (wasLetGoSwitch && wasLetGoSwitch2)
        {
            //Debug.Log("out of line of sight!");
            WasLetGo();
            wasLetGoSwitch = false;
            wasLetGoSwitch2 = false;
        }


    }



    private void OnMouseUp()
    {

        if (wasLetGoSwitch2)
        {
            WasLetGo();
            LineOfSightLeniencySwitch = false;
            wasLetGoSwitch = false;
            wasLetGoSwitch2 = false;
        }

    }

    private void WasLetGo()
    {
        myRigidBody2D.velocity = new Vector3(0f, 0f, 0f);

        myRigidBody2D.mass = objectMass;

        myRigidBody2D.angularVelocity = 0;

        ResetRotationIfCloseEnough();

        //Debug.Log("saracens");
        OnDraggingEnds();
    }

    private void OnMouseDownInit()
    {
        mZCoord =
            Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        myRigidBody2D.mass = objectMass * LevitatingMassScaling;

        // Once the gameObject is picked up it zeros its rotation.
        myRigidBody2D.angularVelocity = 0;
    }

    private void ResetRotationIfCloseEnough()
    {
        // on mouse release drop the object with rotation 0,0,0 if the object was close to rotation 0,0,0
        if (Mathf.Abs(transform.rotation.z) <= 0.05)
        {
            ///Debug.Log(transform.rotation.eulerAngles.z);
            transform.rotation = new Quaternion();
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void RotateTheObjectToFaceUpwards()
    {
        //        Debug.Log(myRigidBody2D.angularVelocity);
        const float RotationScalingConstant = 1f;

        // Reset rotation to 0 over time.
        if (
            transform.rotation.eulerAngles.z >= 0 &&
            transform.rotation.eulerAngles.z < 180
        )
        {
            myRigidBody2D
                .AddTorque(-m_Speed *
                RotationScalingConstant *
                myRigidBody2D.mass);
        }
        else if (
            transform.rotation.eulerAngles.z >= 180 &&
            transform.rotation.eulerAngles.z < 360
        )
        {
            myRigidBody2D
                .AddTorque(m_Speed *
                RotationScalingConstant *
                myRigidBody2D.mass);
        }

        if (Mathf.Abs(transform.rotation.z) <= 0.1)
        {
            myRigidBody2D.angularVelocity = 0;
            ResetRotationIfCloseEnough();
        }
    }

    private void DragObjectToMousePointer()
    {
        // calc velocity necessary to follow the mouse pointer
        var vel =
            (
            (GetMouseAsWorldPoint() + mOffset) - gameObject.transform.position
            ) *
            m_Speed;

        // lower the offset so it centers on the pointer over time
        mOffset *= 0.92f;

        // limit max velocity to avoid pass through objects
        if (vel.magnitude > m_MaxSpeed)
        {
            vel *= m_MaxSpeed / vel.magnitude;
        }

        // set object velocity
        myRigidBody2D.velocity = vel;
    }

    private bool LineOfSightCheck()
    {
        RaycastHit2D
            myRaycastInfoWandToTargetPosition,
            myRaycastInfoWandToTargetMousePosition;
        GetRaycastHitInfo(out myRaycastInfoWandToTargetPosition,
        out myRaycastInfoWandToTargetMousePosition);

        DrawDebugRaycasts();

        if (myRaycastInfoWandToTargetPosition)
        {
            if (
                myRaycastInfoWandToTargetPosition.transform.gameObject ==
                this.gameObject
            )
            {
                lineOfSightSwitch = true;
                LineOfSightLeniencySwitch = false;
                return true;
            }
        }

        if (myRaycastInfoWandToTargetMousePosition)
        {
            if (
                (
                myRaycastInfoWandToTargetMousePosition.transform.gameObject ==
                this.gameObject
                ) &&
                Time.time <= initialTime + m_LineOfSightLeniency
            )
            {
                lineOfSightSwitch = true;
                LineOfSightLeniencySwitch = false;
                return true;
            }
        }

        // if it gets to this point it means that the object is no longer in line of sight, so we turn the was let go switch on and the line of sight switch off.
        if (lineOfSightSwitch)
        {
            wasLetGoSwitch = true;
            lineOfSightLeniencyInitialTime = Time.time; // registers the time that the object initially went out of line of sight.
        }

        lineOfSightSwitch = false;

        // if less than m_LineOfSightLeniency seconds has passed since the object went line of sight, turn the line of sight leniency switch on, else turn it off.
        if (Time.time <= lineOfSightLeniencyInitialTime + m_LineOfSightLeniency)
        {
            LineOfSightLeniencySwitch = true;
        }
        else
        {
            LineOfSightLeniencySwitch = false;
        }

        return false;
    }

    private void GetRaycastHitInfo(
        out RaycastHit2D myRaycastInfoWandToTargetPosition,
        out RaycastHit2D myRaycastInfoWandToTargetMousePosition
    )
    {
        myRaycastInfoWandToTargetPosition =
            Physics2D
                .Raycast(myPlayer.transform.position,
                transform.position - myPlayer.transform.position,
                m_Range);
        myRaycastInfoWandToTargetMousePosition =
            Physics2D
                .Raycast(myPlayer.transform.position,
                (GetMouseAsWorldPoint() - myPlayer.transform.position),
                m_Range);
    }

    private void DrawDebugRaycasts()
    {
        Debug
            .DrawRay(myPlayer.transform.position,
            (GetMouseAsWorldPoint() - myPlayer.transform.position));

        Debug
            .DrawRay(myPlayer.transform.position,
            (this.gameObject.transform.position - myPlayer.transform.position));
    }

    public void OnDraggingBegins()
    {
        initialTime = Time.time;
        if (LineOfSightCheck())
        {
            OnMouseDownInit();

            lineOfSightSwitch = true;

            wasLetGoSwitch = false;

            wasLetGoSwitch2 = true;

            myPlayer.GetComponent<PlayerMagic>().ObjectBeingDragged = gameObject;

            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            //Debug.Log(gameObject + " dragging begun.");
            OnDraggingBeginsEvent?.Invoke();
        }
    }

    public void OnDraggingEnds()
    {
        //Debug.Log(gameObject + " dragging ended.");
        OnDraggingEndsEvent?.Invoke();
    }
}
