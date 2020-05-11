using System;
using System.Collections;
using UnityEngine;

public class PlayerNodeMovement : MonoBehaviour
{
    [SerializeField] private InputHandler m_InputHandler;

    [SerializeField] private Node m_CurrentNode;

    public Node GetCurrentNode()
    {
        return m_CurrentNode;
    }

    public void SetCurrentNode(Node myTargetNode)
    {
        m_CurrentNode = myTargetNode;
        gameObject.transform.position = m_CurrentNode.gameObject.transform.position;
    }

    [SerializeField] private bool m_DebugLoggingEnabled = false;

    [SerializeField] private float m_Speed = 3f;



    public event EventHandler<GameObject> OnTravelNodeReachedEvent;
    public event EventHandler<GameObject> OnTravelNodeDepartedEvent;
    public event EventHandler<GameObject> OnDestinationNodeReachedEvent;
    public event EventHandler<GameObject> OnDestinationNodeDepartedEvent;
    public event EventHandler<GameObject> OnDestinationNotAccessibleEvent;
    public event EventHandler<string> OnNoDestinationFoundEvent;


    void Start()
    {
        m_InputHandler.OnVerticalUpPressedEvent += MoveUp;
        m_InputHandler.OnVerticalDownPressedEvent += MoveDown;
        m_InputHandler.OnHorizontalLeftPressedEvent += MoveLeft;
        m_InputHandler.OnHorizontalRightPressedEvent += MoveRight;


        gameObject.transform.position = m_CurrentNode.gameObject.transform.position;
    }
    private void MoveUp(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_UpDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_UpDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoUp());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(m_CurrentNode.m_UpDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound("up");
        }
    }

    private void MoveDown(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_DownDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_DownDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoDown());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(m_CurrentNode.m_DownDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound("down");
        }
    }

    private void MoveLeft(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_LeftDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_LeftDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoLeft());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(m_CurrentNode.m_LeftDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound("left");
        }
    }

    private void MoveRight(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_RightDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_RightDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted();
                StartCoroutine(DoRight());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(m_CurrentNode.m_RightDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound("right");
        }
    }

    IEnumerator DoUp()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != m_CurrentNode.m_UpDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_UpDestination.transform.position, m_Speed * Time.deltaTime);


            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_UpDestination);
    }
    IEnumerator DoDown()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != m_CurrentNode.m_DownDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_DownDestination.transform.position, m_Speed * Time.deltaTime);

            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_DownDestination);
    }
    IEnumerator DoLeft()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != m_CurrentNode.m_LeftDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_LeftDestination.transform.position, m_Speed * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_LeftDestination);
    }
    IEnumerator DoRight()
    {
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.position != m_CurrentNode.m_RightDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_RightDestination.transform.position, m_Speed * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_RightDestination);

    }


    private void DoFollow(Node previousNode)
    {
        string myDirection = "";


        if (m_CurrentNode.m_DownDestination != null)
        {
            if (m_CurrentNode.m_DownDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "down";
            }
        }

        if (m_CurrentNode.m_UpDestination != null)
        {
            if (m_CurrentNode.m_UpDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "up";
            }
        }

        if (m_CurrentNode.m_LeftDestination != null)
        {
            if (m_CurrentNode.m_LeftDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "left";
            }
        }

        if (m_CurrentNode.m_RightDestination != null)
        {
            if (m_CurrentNode.m_RightDestination.GetComponent<Node>() != previousNode)
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
            default: Debug.Log("Couldnt figure out which direction to go from the travel node! ps, if this happened, assign destinations at the node: " + m_CurrentNode); break;
        }
    }

    // Decides what to do once a node is reached.
    private void NodeReachedDecider(GameObject destination)
    {
        if ((gameObject.transform.position == destination.transform.position) && !destination.GetComponent<Node>().IsTravelNode)
        {
            m_CurrentNode = destination.GetComponent<Node>();
            OnDestinationNodeReached();
        }
        else if ((gameObject.transform.position == destination.transform.position) && destination.GetComponent<Node>().IsTravelNode)
        {
            Node previousNode = m_CurrentNode;
            m_CurrentNode = destination.GetComponent<Node>();
            OnTravelNodeReached();
            DoFollow(previousNode);
        }
    }

    private void OnDestinationNodeReached()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node reached: " + m_CurrentNode);
        }
        OnDestinationNodeReachedEvent?.Invoke(this, m_CurrentNode.gameObject);
    }

    private void OnDestinationNodeDeparted()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + m_CurrentNode);
        }
        OnDestinationNodeDepartedEvent?.Invoke(this, m_CurrentNode.gameObject);
    }

    private void OnTravelNodeReached()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Travel node reached: " + m_CurrentNode);
        }
        OnTravelNodeReachedEvent?.Invoke(this, m_CurrentNode.gameObject);
    }

    private void OnTravelNodeDeparted()
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + m_CurrentNode);
        }
        OnTravelNodeDepartedEvent?.Invoke(this, m_CurrentNode.gameObject);
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
