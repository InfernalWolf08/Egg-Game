using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Rigidbody2D rb2d;
    public float dir;

    [Header("Score")]
    public int score=0;
    public int money=0;

    [Header("Animation")]
    public float frameRate=0.1f;
    public Sprite[] walkCycle;
    public SpriteRenderer sr;
    public Transform basket;

    void Start()
    {
        // Initialize
        rb2d = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(walkCycleAnim(frameRate));
    }

    void FixedUpdate()
    {
        dir = Input.GetAxis("Horizontal");
        rb2d.AddForceX(dir*speed);
    }

    // Functions
    public void addScore(int scoreAdd=1)
    {
        score+=scoreAdd;
    }

    // Coroutines
    IEnumerator walkCycleAnim(float frameRate=0.1f)
    {
        int index = 0;
        while (true)
        {
            // Face correct direction
            sr.flipX = dir>0;

            // Move to next frame
            if (dir!=0)
            {
                // Change sprite
                index++;
                index = index>walkCycle.Length-1 ? 0 : index;

                // Move basket
                float newY=basket.localPosition.y;
                if (index==4 || index==11)
                {
                    newY=1.37f;
                } else if (index==5) {
                    newY=1.31f;
                } else if (index==0 || index==12) {
                    newY=1.43f;
                }
                basket.localPosition = new Vector3(basket.localPosition.x, newY, basket.localPosition.z);

                yield return new WaitForSeconds(frameRate);
            } else {
                index=0;
            }

            sr.sprite = walkCycle[index];
            yield return null;
        }
    }
}