using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{


    [SerializeField] private GameObject m_EscMenu;

    public UnityEvent[] OnEscapePress = new UnityEvent[2];




    private void Start()
    {
        OnEscapePress[0]?.AddListener(GameHandler.Pause);
        OnEscapePress[1]?.AddListener(GameHandler.Resume);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {
            if (!m_EscMenu.GetComponent<PopupMenuController>().IsActive)
            {
                OnEscapePress[0].Invoke();
            }
            else
            {
                OnEscapePress[1].Invoke();
            }
        }



    }
}
