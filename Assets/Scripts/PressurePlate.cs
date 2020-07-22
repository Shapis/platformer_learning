using System;
using UnityEngine;

public class PressurePlate : BaseItem, IPressurePlateEvents
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private ColorPalette.ColorName m_KeyType;
    private KeyDoor[] myDoors;
    private int numberOfColliders = 0;
    public event EventHandler<KeyDoor[]> OnPressurePlateActivatedEvent;
    public event EventHandler<KeyDoor[]> OnPressurePlateDeactivatedEvent;

    void Awake()
    {
        // On initialization add all GameObjects with the component KeyDoor to the myDoors array. 
        // This is done so we can check later which KeyDoors match the color of each pressure plate so they open when you step on the pressure plate
        myDoors = (KeyDoor[])GameObject.FindObjectsOfType(typeof(KeyDoor));
    }

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = ColorPalette.GetColor32(m_KeyType);
    }

    internal void Activate()
    {
        if (numberOfColliders <= 0)
        {
            foreach (var o in myDoors)
            {
                if (o.GetKeyType() == m_KeyType)
                {
                    o.OpenDoor();
                }
            }
            OnPressurePlateActivated(this, myDoors);
        }
        numberOfColliders++;
    }

    internal void Deactivate()
    {
        numberOfColliders--;

        if (numberOfColliders <= 0)
        {
            foreach (var o in myDoors)
            {
                if (o.GetKeyType() == m_KeyType)
                {
                    o.CloseDoor();
                }
            }
            OnPressurePlateDeactivated(this, myDoors);
        }
    }

    // TODO: Make it so th matchingcolordoorlist is an array of doors matching the color of the specific pressure plate
    // Right now it's just an array with -all- doors.
    public void OnPressurePlateActivated(object sender, KeyDoor[] matchingColorDoorList)
    {
        m_Animator.SetBool("isPressed", true);
        OnPressurePlateActivatedEvent?.Invoke(this, matchingColorDoorList);
    }

    public void OnPressurePlateDeactivated(object sender, KeyDoor[] matchingColorDoorList)
    {
        m_Animator.SetBool("isPressed", false);
        OnPressurePlateDeactivatedEvent?.Invoke(this, matchingColorDoorList);
    }
}