using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float _jumpForce = 5.0f;
    [SerializeField]
    private bool _grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);     

        if (Input.GetKeyDown(KeyCode.Space) && _grounded){
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            _grounded = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        if (hit.collider != null){
            _grounded = true;
        }

    }
}
