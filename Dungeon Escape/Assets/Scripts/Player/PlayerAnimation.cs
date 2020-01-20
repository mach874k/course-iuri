using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordArcAnimation;
    private SpriteRenderer _sprite;
    private SpriteRenderer _swordArcSprite;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcAnimation = transform.GetChild(1).GetComponent<Animator>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Flip(float move){
        if(move > 0){
            _sprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else{
            _sprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    public void Jump(bool jumping){
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack(){
        _anim.SetTrigger("Attack");
        _swordArcAnimation.SetTrigger("SwordArcAnimation");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
