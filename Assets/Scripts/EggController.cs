using System.Collections;
using UnityEngine;

public class EggController : MonoBehaviour
{
    [Header("Drop")]
    public static float speed=0.5f;
    public Rigidbody2D rb2d;

    [Header("Player")]
    public PlayerController player;
    public int scoreAdd;

    [Header("Egg")]
    public Sprite splatted;

    void Start()
    {
        // Initialize
        player = FindAnyObjectByType<PlayerController>();
        transform.localPosition = new Vector3(transform.localPosition.x+UnityEngine.Random.Range(-0.5f, 0.5f), transform.localPosition.y+UnityEngine.Random.Range(-0.5f, 0.5f), transform.localPosition.z);
    }

    public void FixedUpdate()
    {
        // Fall
        if (speed>0)
        {
            rb2d.AddForceY(-speed);
        }

        print($"Egg Speed: {speed}");
    }

    public void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.tag=="basket")
        {
            player.addScore(scoreAdd);
            gameObject.SetActive(false);
        } else if (info.gameObject.tag=="ground") {
            splat();
        }
    }

    void splat()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = splatted;
        sr.sortingOrder = 5;
        GetComponent<Collider2D>().enabled=false;
        rb2d.linearVelocityY=0;
        StartCoroutine(disappear(sr));
    }

    IEnumerator disappear(SpriteRenderer sr)
    {
        while (sr.color.a-Time.deltaTime>0)
        {
            yield return new WaitForSeconds(0.075f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a-Time.deltaTime);
            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    public static void alterSpeed(float speedChange)
    {
        speed*=speedChange;
    }
}