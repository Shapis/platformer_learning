using Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_Cinemachine;
    void Start()
    {
        m_Cinemachine.Follow = GameObject.Find("Player").transform;
    }

    // [Header("Settings")]
    // [SerializeField] private Vector2 m_FollowOffset;
    // [SerializeField] private float M_Speed = 3f;

    // private GameObject followObject;
    // private Vector2 threshold;
    // private Rigidbody2D rb;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     followObject = GameObject.Find("Player");
    //     threshold = calculateThreshold();
    //     rb = followObject.GetComponent<Rigidbody2D>();
    //     transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, -10f);
    // }

    // // Update is called once per frame
    // void FixedUpdate()
    // {
    //     Vector2 follow = followObject.transform.position;
    //     float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
    //     float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
    //     Vector3 newPosition = transform.position;
    //     if (Mathf.Abs(xDifference) >= threshold.x)
    //     {
    //         newPosition.x = follow.x;
    //     }
    //     if (Mathf.Abs(yDifference) >= threshold.y)
    //     {
    //         newPosition.y = follow.y;
    //     }
    //     float moveSpeed = rb.velocity.magnitude > M_Speed ? rb.velocity.magnitude : M_Speed;
    //     transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    // }
    // private Vector3 calculateThreshold()
    // {
    //     Rect aspect = Camera.main.pixelRect;
    //     Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
    //     t.x -= m_FollowOffset.x;
    //     t.y -= m_FollowOffset.y;
    //     return t;
    // }
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     Vector2 border = calculateThreshold();
    //     Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    // }
}
