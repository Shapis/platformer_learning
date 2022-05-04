using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    [SerializeField] private string m_ContainerName = "ItemsContainer";
    [SerializeField] private bool m_AutoMove = true;
    [SerializeField] private bool m_SnappingActive = true;
    [SerializeField] private Vector3 m_GridSize = new Vector3(1f, 1f, 1f);

    private void OnDrawGizmos()
    {
        if ((this.transform.parent != GameObject.Find(m_ContainerName).transform) && m_AutoMove)
        {
            MoveToDirectory();
        }
        if (!Application.isPlaying && transform.hasChanged && m_SnappingActive)
        {
            Snap();
        }
    }

    private void MoveToDirectory()
    {
        this.transform.parent = GameObject.Find(m_ContainerName).transform;
        Debug.Log(this.gameObject + " moved to " + m_ContainerName);
    }

    private void Snap()
    {
        if (m_GridSize.x == 0 || m_GridSize.y == 0 || m_GridSize.z == 0)
        {
            Debug.Log(this + " : m_GridSize cannot be 0");
            return;
        };
        ;
        Vector3 _position = new Vector3(
            (Mathf.Round(this.transform.position.x / m_GridSize.x) * m_GridSize.x),
            (Mathf.Round(this.transform.position.y / m_GridSize.y) * m_GridSize.y),
            (Mathf.Round(this.transform.position.z / m_GridSize.z) * m_GridSize.z)
        ); ;
        this.transform.position = _position;
    }
}