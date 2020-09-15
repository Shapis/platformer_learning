using System.Collections;
using UnityEngine;

public class Overworld : MonoBehaviour
{
    [SerializeField] private Sprite[] m_OverworldSprites = new Sprite[4];
    private Coroutine spriteChangerCoroutine;
    private int myCurrentColor = 0;

    void Start()
    {
        spriteChangerCoroutine = StartCoroutine("ChangeColors");
    }

    private IEnumerator ChangeColors()
    {
        while (true)
        {
            if (myCurrentColor >= m_OverworldSprites.Length)
            {
                myCurrentColor = 0;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = m_OverworldSprites[myCurrentColor];
            myCurrentColor++;
            yield return new WaitForSecondsRealtime(1.5f);
        }
    }
}
