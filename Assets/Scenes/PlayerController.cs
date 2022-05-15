using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //基礎設定
    public float RunSpeed=40f;
    Rigidbody2D rb;
    bool FacingRight = true;

    //地板,移動,跳躍
    bool IsGrounded=false;
    public Transform GroundCheck;
    float CheckRadius = 0.02f;
    public LayerMask WhatIsGround;
    public float JumpForce=400f;
    float HorizontalMove = 0f;
    bool Jump;

    //滑牆
    bool IsOnWall=false;
    public Transform FrontCheck;
    public LayerMask WhatIsWall;
    bool WallSliding;
    public float WallSlidingSpeed=5;

    //牆跳
    bool WallJumping;
    public float xWallForce;
    public float yWallForce;
    public float WallJumpTime;

    //衝刺
    bool CanDash=false;
    public float DashSpeed=30f;
    public float DashingTime = 0.3f;
    bool IsDashing=false;

    //動畫相關
    //private Animator Anime;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

  
    void Update()//輸入
    {   
        //左右移動
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        //確認是否在地板上
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        //跳躍
        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump = true;
        }
        //確認是否在牆上
        IsOnWall = Physics2D.OverlapCircle(FrontCheck.position, CheckRadius, WhatIsWall);
        if (IsOnWall == true && IsGrounded == false && HorizontalMove != 0)
        {
            WallSliding = true;
        }
        else 
        {
            WallSliding = false;
        }
        //牆跳
        if (IsOnWall == true && Input.GetKeyDown(KeyCode.Space))
        {
            WallJumping = true;
            Invoke("SetWallJumpingToFalse", WallJumpTime);
        }
        //衝刺
        if(IsOnWall == true || IsGrounded == true)
        {
            CanDash =true;
        }
        if (CanDash == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsDashing = true;
            CanDash = false;
            Invoke("SetIsDashingToFalse",DashingTime);
        }
    }
    private void FixedUpdate()//玩家動作
    {
        //左右移動
        rb.velocity = new Vector2(RunSpeed * HorizontalMove,rb.velocity.y);
        //跳躍
        if (Jump == true)
        {
            rb.velocity = Vector2.up*JumpForce;
        }
        Jump = false;
        //轉身
        if (HorizontalMove > 0 && FacingRight==false)
        {
            Flip();
        }
        else if(HorizontalMove<0&&FacingRight==true)
        {
            Flip();
        }
        //在牆上滑
        if (WallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        //牆跳
        if (WallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce *-HorizontalMove, yWallForce);
        }
        //衝刺
        if (IsDashing == true)
        {
            rb.velocity = new Vector2(DashSpeed * HorizontalMove, 0f);
            PlayerAfterImagePool.Instance.GetFromPool();
        }

    }
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        FacingRight = !FacingRight;
    }
    void SetWallJumpingToFalse()
    {
        WallJumping = false;
    }
    void SetIsDashingToFalse()
    {
        IsDashing = false;
    }
}
