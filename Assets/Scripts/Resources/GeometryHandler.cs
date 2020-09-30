using UnityEngine;

public class GeometryHandler
{
    public GameObject
    DrawLine(GameObject myContainer, Vector3[] myPoints, Vector2 myWidths, Color myColor)
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
}
