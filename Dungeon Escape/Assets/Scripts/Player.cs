using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * _speed, rb.velocity.y);     

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
        }
    }
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        if (hitInfo.collider != null)
            if(!_resetJump)
                return true;

        return false;
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}
