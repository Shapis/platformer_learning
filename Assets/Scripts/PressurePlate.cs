using System;
using UnityEngine;

public class PressurePlate : BaseItem
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private ColorPalette.ColorName keyType;
    private KeyDoor[] myDoors;

    void Awake()
    {
        // On initialization add all GameObjects with the component KeyDoor to the myDoors array. 
        //This is done so we can check later which KeyDoors match the color of each pressure plate so they open when you step on the pressure plate
        myDoors = (KeyDoor[])GameObject.FindObjectsOfType(typeof(KeyDoor));
    }

    internal void Activate()
    {
        foreach (var o in myDoors)
        {
            if (o.GetKeyType() == keyType)
            {
                o.OpenDoor();
            }
        }
    }

    internal void Deactivate()
    {
        foreach (var o in myDoors)
        {
            if (o.GetKeyType() == keyType)
            {
                o.CloseDoor();
            }
        }
    }
}