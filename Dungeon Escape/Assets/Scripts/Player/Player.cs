using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    private bool _grounded = false;
    [SerializeField]
    private float _speed = 2.5f;
    private bool isDead = false;
    public int _diamonds;
    public int Health { get; set; }
    private PlayerAnimation _playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
            return;

        Movement();

        if(CrossPlatformInputManager.GetButtonDown("A_Button") && _grounded){
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal");  
        isGrounded();

        if(move != 0)
            _playerAnim.Flip(move);

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && _grounded){
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }

    void isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null){
            if(!_resetJump){
                _playerAnim.Jump(false);
                _grounded = true;
                return;
            }
        }
        _grounded = false;
        return;
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(isDead)
            return;
            
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if(1 > Health){
            _playerAnim.Death();
            isDead = true;
        }
    }

    public void AddGems(int amount)
    {
        _diamonds += amount;
        UIManager.Instance.UpdateGemCount(_diamonds);
    }
}
