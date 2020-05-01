using System;
using System.Collections;
using UnityEngine;

public class PlayerNodeMovement : MonoBehaviour
{
    [SerializeField] private InputHandler myInputHandler;

    [SerializeField] private Node myCurrentNode;

    [SerializeReference] private bool m_DebugLoggingEnabled = false;

    event EventHandler<GameObject> OnTravelNodeReachedEvent;
    event EventHandler<GameObject> OnTravelNodeDepartedEvent;
    event EventHandler<GameObject> OnDestinationNodeReachedEvent;
    event EventHandler<GameObject> OnDestinationNodeDepartedEvent;
    event EventHandler<GameObject> OnDestinationNotAccessibleEvent;
    event EventHandler<string> OnNoDestinationFoundEvent;


    void Start()
    {
        myInputHandler.OnVerticalUpPressedEvent += MoveUp;
        myInputHandler.OnVerticalDownPressedEvent += MoveDown;
        myInputHandler.OnHorizontalLeftPressedEvent += MoveLeft;
        myInputHandler.OnHorizontalRightPressedEvent += MoveRight;
    }
    private void MoveUp(object sender, EventArgs e)
    {
        if (myCurrentNode.m_UpDestination != null)
        {
            if (gameObject.transform.position == myCurrentNode.transform.position && myCurrentNode.m_UpDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoUp());
            }
            else if (gameObject.transform.position == myCurrentNode.transform.position)
            {
                OnDestinationNotAccessible(myCurrentNode.m_UpDestination);
            }
        }
        else if (gameObject.transform.position == myCurrentNode.transform.position)
        {
            OnNoDestinationFound("up");
        }
    }

    private void MoveDown(object sender, EventArgs e)
    {
        if (myCurrentNode.m_DownDestination != null)
        {
            if (gameObject.transform.position == myCurrentNode.transform.position && myCurrentNode.m_DownDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoDown());
            }
            else if (gameObject.transform.position == myCurrentNode.transform.position)
            {
                OnDestinationNotAccessible(myCurrentNode.m_DownDestination);
            }
        }
        else if (gameObject.transform.position == myCurrentNode.transform.position)
        {
            OnNoDestinationFound("down");
        }
    }

    private void MoveLeft(object sender, EventArgs e)
    {
        if (myCurrentNode.m_LeftDestination != null)
        {
            if (gameObject.transform.position == myCurrentNode.transform.position && myCurrentNode.m_LeftDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoLeft());
            }
            else if (gameObject.transform.position == myCurrentNode.transform.position)
            {
                OnDestinationNotAccessible(myCurrentNode.m_LeftDestination);
            }
        }
        else if (gameObject.transform.position == myCurrentNode.transform.position)
        {
            OnNoDestinationFound("left");
        }
    }

    private void MoveRight(object sender, EventArgs e)
    {
        if (myCurrentNode.m_RightDestination != null)
        {
            if (gameObject.transform.position == myCurrentNode.transform.position && myCurrentNode.m_RightDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoRight());
            }
            else if (gameObject.transform.position == myCurrentNode.transform.position)
            {
                OnDestinationNotAccessible(myCurrentNode.m_RightDestination);
            }
        }
        else if (gameObject.transform.position == myCurrentNode.transform.position)
        {
            OnNoDestinationFound("right");
        }
    }

    IEnumerator DoUp()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != myCurrentNode.m_UpDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, myCurrentNode.m_UpDestination.transform.position, 2f * Time.deltaTime);


            yield return null;
        }

        NodeReachedDecider(myCurrentNode.m_UpDestination);
    }
    IEnumerator DoDown()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != myCurrentNode.m_DownDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, myCurrentNode.m_DownDestination.transform.position, 2f * Time.deltaTime);

            yield return null;
        }

        NodeReachedDecider(myCurrentNode.m_DownDestination);
    }
    IEnumerator DoLeft()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != myCurrentNode.m_LeftDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, myCurrentNode.m_LeftDestination.transform.position, 2f * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(myCurrentNode.m_LeftDestination);
    }
    IEnumerator DoRight()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != myCurrentNode.m_RightDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, myCurrentNode.m_RightDestination.transform.position, 2f * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(myCurrentNode.m_RightDestination);

    }


    private void DoFollow(Node previousNode)
    {
        string myDirection = "";


        if (myCurrentNode.m_DownDestination != null)
        {
            if (myCurrentNode.m_DownDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "down";
            }
        }

        if (myCurrentNode.m_UpDestination != null)
        {
            if (myCurrentNode.m_UpDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "up";
            }
        }

        if (myCurrentNode.m_LeftDestination != null)
        {
            if (myCurrentNode.m_LeftDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "left";
            }
        }

        if (myCurrentNode.m_RightDestination != null)
        {
            if (myCurrentNode.m_RightDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "right";
            }
        }

        switch (myDirection)
        {
            case "up": OnTravelNodeDeparted(); StartCoroutine(DoUp()); break;
            case "down": OnTravelNodeDeparted(); StartCoroutine(DoDown()); break;
            case "left": OnTravelNodeDeparted(); StartCoroutine(DoLeft()); break;
            case "right": OnTravelNodeDeparted(); StartCoroutine(DoRight()); break;
            default: Debug.Log("Couldnt figure out which direction to go from the travel node! ps, if this happened, assign destinations at the node: " + myCurrentNode); break;
        }
    }

    // Decides what to do once a node is reached.
    private void NodeReachedDecider(GameObject destination)
    {
        if ((gameObject.transform.position == destination.transform.position) && !destination.GetComponent<Node>().IsTravelNode)
        {
            myCurrentNode = destination.GetComponent<Node>();
            OnDestinationNodeReached();
        }
        else if ((gameObject.transform.position == destination.transform.position) && destination.GetComponent<Node>().IsTravelNode)
        {
            Node previousNode = myCurrentNode;
            myCurrentNode = destination.GetComponent<Node>();
            OnTravelNodeReached();
            DoFollow(previousNode);
        }
    }

    private void OnDestinationNodeReached()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node reached: " + myCurrentNode);
        }
        OnDestinationNodeReachedEvent?.Invoke(this, myCurrentNode.gameObject);
    }

    private void OnDestinationNodeDeparted()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + myCurrentNode);
        }
        OnDestinationNodeDepartedEvent?.Invoke(this, myCurrentNode.gameObject);
    }

    private void OnTravelNodeReached()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Travel node reached: " + myCurrentNode);
        }
        OnTravelNodeReachedEvent?.Invoke(this, myCurrentNode.gameObject);
    }

    private void OnTravelNodeDeparted()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + myCurrentNode);
        }
        OnTravelNodeDepartedEvent?.Invoke(this, myCurrentNode.gameObject);
    }

    private void OnNoDestinationFound(string targetDestination)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("No destination found!");
        }
        OnNoDestinationFoundEvent?.Invoke(this, targetDestination);
    }

    private void OnDestinationNotAccessible(GameObject targetDestination)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination not accessible!");
        }
        OnDestinationNotAccessibleEvent?.Invoke(this, targetDestination);
    }
}
