using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Rigidbody2D rb2d;

    void Start()
    {
        // Initialize
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.AddForceX(Input.GetAxis("Horizontal")*speed);
    }
}