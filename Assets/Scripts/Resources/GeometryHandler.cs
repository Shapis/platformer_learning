using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeometryHandler
{
    public GameObject DrawLine(GameObject myContainer, Vector3[] myPoints, Vector2 myWidths, Color myColor)
    {
        GameObject myLine = new GameObject();
        myLine.transform.SetParent(myContainer.transform);

        LineRenderer myLineRenderer = myLine.AddComponent<LineRenderer>();
        myLineRenderer.positionCount = myPoints.Length;
        myLine.GetComponent<LineRenderer>().SetPositions(myPoints);

        myLineRenderer.startWidth = myWidths.x;
        myLineRenderer.endWidth = myWidths.y;
        myLineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        myLineRenderer.material.color = myColor;

        return myLine;
    }

    public Vector3 GetCenter(Vector3 a, Vector3 b)
    {
        int half = 2;
        return (a + b) / half;
    }


    // This whole idea was adapted from here: https://answers.unity.com/questions/1095047/detect-mouse-events-for-ui-canvas.html oct 1st 2020
    public bool IsTouchInsideObject(Vector3 touchPosition, GameObject objectToBeChecked)
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults(touchPosition), objectToBeChecked);
    }

    ///Returns 'true' if we touched or hovering over a specific UI element
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults, GameObject objectToBeChecked)
    {
        foreach (var o in eventSystemRaycastResults)
        {
            //Debug.Log(o.gameObject);
            //Debug.Log(o.gameObject.transform.parent.gameObject);
            if (o.gameObject.transform.parent.gameObject == objectToBeChecked)
            {
                return true;
            }
            else if (o.gameObject == objectToBeChecked)
            {
                return true;
            }
        }
        return false;
    }

    ///Gets all event system raycast results of current mouse or touch position.
    private List<RaycastResult> GetEventSystemRaycastResults(Vector3 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}
