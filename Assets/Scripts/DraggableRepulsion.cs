using System;
using System.Collections;
using UnityEngine;

public class DraggableRepulsion : MonoBehaviour, IDraggableEvents
{
    // TODO: Change this class so it's not awkward when you let go of an object on top of the player.
    [SerializeField] private PlayerItemDragger m_PlayerItemDragger;
    [SerializeField] private Collider2D m_TopCollider;
    [SerializeField] private Collider2D m_BottomCollider;
    private GameObject selectedObject;
    private int originalLayer = 0;
    private int originalSortingLayerOrder;


    void Start()
    {
        m_PlayerItemDragger.OnDraggingBeginsEvent += OnDraggingBegins;
        m_PlayerItemDragger.OnDraggingEndsEvent += OnDraggingEnds;
    }

    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        selectedObject = draggingEventArgs.TargetGameObject;
        originalLayer = selectedObject.layer;
        originalSortingLayerOrder = selectedObject.GetComponent<SpriteRenderer>().sortingOrder;
        selectedObject.GetComponent<SpriteRenderer>().sortingOrder += 100;
        selectedObject.layer = 30; // 30 is the intangible to player layer
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        selectedObject.layer = originalLayer;
        selectedObject.GetComponent<SpriteRenderer>().sortingOrder = originalSortingLayerOrder;
        selectedObject = null;
    }

    private IEnumerator CheckIfTheObjectIsCollidingWithPlayer()
    {
        selectedObject.layer = originalLayer;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        if (selectedObject.GetComponent<Rigidbody2D>().IsTouching(m_BottomCollider) || selectedObject.GetComponent<Rigidbody2D>().IsTouching(m_TopCollider))
        {
            selectedObject.layer = 30;
            selectedObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.Normalize(selectedObject.transform.position - this.transform.position) * 3f;
            Debug.Log("a");
            yield return new WaitForSeconds(1.5f);
            Debug.Log("b");
            selectedObject.layer = originalLayer;
            selectedObject.GetComponent<SpriteRenderer>().sortingOrder = originalSortingLayerOrder;
            selectedObject = null;
            yield break;

        }
        selectedObject.GetComponent<SpriteRenderer>().sortingOrder = originalSortingLayerOrder;
        selectedObject = null;
    }

    private IEnumerator PushItemAway()
    {
        selectedObject.layer = 30;
        selectedObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.Normalize(selectedObject.transform.position - this.transform.position);
        float timer = 0;
        while (timer <= 1.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        selectedObject.layer = originalLayer;
    }

    public void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        throw new NotImplementedException();
    }

    public void OnLineOfSightUnblocked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
