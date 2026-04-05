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
    public SpriteRenderer sr;
    public Sprite splatted;
    public AudioSource source;

    void Start()
    {
        // Initialize
        player = FindAnyObjectByType<PlayerController>();
        source = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        transform.localPosition = new Vector3(transform.localPosition.x+UnityEngine.Random.Range(-0.5f, 0.5f), transform.localPosition.y+UnityEngine.Random.Range(-0.5f, 0.5f), transform.localPosition.z);
    }

    public void FixedUpdate()
    {
        // Fall
        if (speed>0)
        {
            rb2d.AddForceY(-speed);
        }

        //print($"Egg Speed: {speed}");
    }

    public void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.tag=="basket")
        {
            StartCoroutine(collect());
        } else if (info.gameObject.tag=="ground") {
            splat();
        }
    }

    void splat()
    {
        if (GetComponent<Animator>()==null)
        {
            source.clip = FindAnyObjectByType<AudioController>().eggSplat;
        } else {
            source.clip = FindAnyObjectByType<AudioController>().eggSplatRotten;
        }
        source.Play();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        sr.sprite = splatted;
        Animator animator = GetComponent<Animator>();
        if (animator!=null)
        {
            animator.SetBool("broke", true);
        }
        sr.sortingOrder = 5;
        GetComponent<Collider2D>().enabled=false;
        rb2d.linearVelocityY=0;
        StartCoroutine(disappear());
    }

    IEnumerator collect()
    {
        // Set the sound
        if (GetComponent<Animator>()==null)
        {
            source.clip = FindAnyObjectByType<AudioController>().eggCollect;
        } else {
            source.clip = FindAnyObjectByType<AudioController>().eggCollectRotten;
        }

        // Play sound and hide
        sr.enabled = false;
        source.Play();
        while(source.isPlaying)
        {
            yield return null;
        }
        
        // Add score and disable
        player.addScore(scoreAdd);
        gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator disappear()
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