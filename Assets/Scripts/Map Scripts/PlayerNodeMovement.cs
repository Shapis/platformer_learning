using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeMovement : MonoBehaviour, INodeMovementEvents
{
    private Coroutine _followPathCoroutine;


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

    public event EventHandler<GameObject> OnInitialTravelNodeLoadedEvent;
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

        foreach (var item in GameObject.FindObjectsOfType<NodeTouchMovement>())
        {
            item.OnNodeTouchedEvent += OnNodeTouched;
        }


        gameObject.transform.position = m_CurrentNode.gameObject.transform.position;
        OnInitialDestinationNodeLoaded(this, m_CurrentNode.gameObject);
    }
    private void MoveUp(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_UpDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_UpDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode.gameObject);
                StartCoroutine(DoUp());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_UpDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "up");
        }
    }

    private void MoveDown(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_DownDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_DownDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode.gameObject);
                StartCoroutine(DoDown());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_DownDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "down");
        }
    }

    private void MoveLeft(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_LeftDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_LeftDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode.gameObject);
                StartCoroutine(DoLeft());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_LeftDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "left");
        }
    }

    private void MoveRight(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_RightDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_RightDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode.gameObject);
                StartCoroutine(DoRight());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_RightDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "right");
        }
    }

    private IEnumerator DoUp()
    {
        while (gameObject.transform.position != m_CurrentNode.m_UpDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_UpDestination.transform.position, m_Speed * Time.deltaTime);


            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_UpDestination);
    }
    private IEnumerator DoDown()
    {
        while (gameObject.transform.position != m_CurrentNode.m_DownDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_DownDestination.transform.position, m_Speed * Time.deltaTime);

            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_DownDestination);
    }
    private IEnumerator DoLeft()
    {
        while (gameObject.transform.position != m_CurrentNode.m_LeftDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_LeftDestination.transform.position, m_Speed * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_LeftDestination);
    }
    private IEnumerator DoRight()
    {
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
            case "up": OnTravelNodeDeparted(this, m_CurrentNode.gameObject); StartCoroutine(DoUp()); break;
            case "down": OnTravelNodeDeparted(this, m_CurrentNode.gameObject); StartCoroutine(DoDown()); break;
            case "left": OnTravelNodeDeparted(this, m_CurrentNode.gameObject); StartCoroutine(DoLeft()); break;
            case "right": OnTravelNodeDeparted(this, m_CurrentNode.gameObject); StartCoroutine(DoRight()); break;
            default: Debug.Log("Couldnt figure out which direction to go from the travel node! ps, if this happened, assign destinations at the node: " + m_CurrentNode); break;
        }
    }

    // Decides what to do once a node is reached.
    private void NodeReachedDecider(GameObject destination)
    {
        if ((gameObject.transform.position == destination.transform.position) && !destination.GetComponent<Node>().IsTravelNode)
        {
            m_CurrentNode = destination.GetComponent<Node>();
            OnDestinationNodeReached(this, m_CurrentNode.gameObject);
        }
        else if ((gameObject.transform.position == destination.transform.position) && destination.GetComponent<Node>().IsTravelNode)
        {
            Node previousNode = m_CurrentNode;
            m_CurrentNode = destination.GetComponent<Node>();
            OnTravelNodeReached(this, m_CurrentNode.gameObject);
            DoFollow(previousNode);
        }
    }

    public void OnDestinationNodeReached(object sender, GameObject _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node reached: " + _currentNode.GetComponent<Node>());
        }
        OnDestinationNodeReachedEvent?.Invoke(this, _currentNode);
    }

    public void OnDestinationNodeDeparted(object sender, GameObject _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + _currentNode.GetComponent<Node>());
        }
        OnDestinationNodeDepartedEvent?.Invoke(this, _currentNode);
    }

    public void OnTravelNodeReached(object sender, GameObject _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Travel node reached: " + _currentNode.GetComponent<Node>());
        }
        OnTravelNodeReachedEvent?.Invoke(this, _currentNode);
    }

    public void OnTravelNodeDeparted(object sender, GameObject _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + _currentNode.GetComponent<Node>());
        }
        OnTravelNodeDepartedEvent?.Invoke(this, _currentNode);
    }

    public void OnNoDestinationFound(object sender, string targetDestination)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("No destination found! Target destination: " + targetDestination);
        }
        OnNoDestinationFoundEvent?.Invoke(this, targetDestination);
    }

    public void OnDestinationNotAccessible(object sender, GameObject targetDestination)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination not accessible! Target destination: " + targetDestination.GetComponent<Node>());
        }
        OnDestinationNotAccessibleEvent?.Invoke(this, targetDestination);
    }

    public void OnInitialDestinationNodeLoaded(object sender, GameObject _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Initial destination node loaded: " + _currentNode.GetComponent<Node>());
        }
        OnInitialTravelNodeLoadedEvent?.Invoke(this, _currentNode);
    }

    // This doesn't work well with travel nodes. Since if you are on a travel node, the nodePath you will receive will have your current travel node as the first element in the list. Meaning that the coroutine will start from the current travel node, and not the initial node that the player will end up on once the previous traveling concludes.
    public void OnNodeTouched(object sender, List<Node> nodePath)
    {
        if (!m_CurrentNode.IsTravelNode && nodePath != null)
        {
            if (_followPathCoroutine != null)
            {
                StopCoroutine(_followPathCoroutine);
            }
            _followPathCoroutine = StartCoroutine("DoFollowPath", nodePath);
        }
    }


    // TODO: Make it so it doesn't call MoveUp/Down/Left/Right when travel nodes are reached. It's unnecessary.
    IEnumerator DoFollowPath(List<Node> nodePath)
    {

        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Following path...");
        }

        bool inTransit = false;
        while (nodePath.Count > 1)
        {
            if (!inTransit)
            {
                if (nodePath[1].gameObject == nodePath[0].m_UpDestination)
                {
                    inTransit = true;
                    MoveUp(this, EventArgs.Empty);
                }
                else if (nodePath[1].gameObject == nodePath[0].m_DownDestination)
                {
                    inTransit = true;
                    MoveDown(this, EventArgs.Empty);
                }
                else if (nodePath[1].gameObject == nodePath[0].m_LeftDestination)
                {
                    inTransit = true;
                    MoveLeft(this, EventArgs.Empty);
                }
                else if (nodePath[1].gameObject == nodePath[0].m_RightDestination)
                {
                    inTransit = true;
                    MoveRight(this, EventArgs.Empty);
                }
            }

            if (m_CurrentNode == nodePath[1])
            {
                inTransit = false;
                nodePath.RemoveAt(0);
            }
            yield return null;
        }
    }
}