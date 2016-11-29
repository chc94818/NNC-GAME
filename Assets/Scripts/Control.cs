using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
    private Rigidbody2D rigi;
    private Animator anim;
    public float speed = 350;
    public float jumpForce = 700;
    public float flyForce = 300;
    public float clawJumpForce = 350;
    public bool isGround = true;

    //初始化
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        //左右移動--------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(1);
            Direction(0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-1);
            Direction(1);
        }
        //-----------------------------------------------------------------------------------
        //停止移動--------------------------------------------------------------------------
        if (Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow))
        {
            Move(0);
        }
        //-----------------------------------------------------------------------------------

        //跳躍------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        //-----------------------------------------------------------------------------------
        //飛行-------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.Z))
        {
            Fly(true);
        }
         if (Input.GetKeyUp(KeyCode.Z))
        {
            Fly(false);
        }
        //-----------------------------------------------------------------------------------
        //啄擊-------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.X))
        {
            Peck(true);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Peck(false);
        }
        //-----------------------------------------------------------------------------------
        //抓擊-------------------------------------------------------------------------------
        if (Input.GetKey(KeyCode.C))
        {
            Claw(true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Claw(false);
        }
        //-----------------------------------------------------------------------------------
        StateMachine();
    }
    //動畫狀態機
    void StateMachine()
    {
        anim.SetBool("Ground", isGround);
        anim.SetFloat("Velocity_Y", rigi.velocity.y);

    }

    //面對方向
    void Direction(int direction)
    {
        transform.eulerAngles = new Vector3(0, 180 * direction, 0);
    }

    //移動
    void Move(int direction)
    {
        rigi.velocity = new Vector2(direction*speed*Time.deltaTime, rigi.velocity.y);
        anim.SetFloat("Move", Mathf.Abs(direction));
    }
    
    //跳躍
    void Jump()
    {
        if (!isGround)
        {
            return;
        }
        rigi.velocity = new Vector2(rigi.velocity.x, jumpForce * Time.deltaTime);
        anim.SetTrigger("Jump");
    }

    //飛行
    void Fly(bool isFlying)
    {
        rigi.velocity = new Vector2(rigi.velocity.x, flyForce * Time.deltaTime);

        anim.SetBool("isFlying", isFlying);
    }

    //啄擊
    void Peck(bool isPeck)
    {
        anim.SetBool("isPeck", isPeck);
        if (!isGround)
        {
            return;
        }
        rigi.velocity = new Vector2(0, rigi.velocity.y);

    }
        //抓擊
        void Claw(bool isClaw)
    {
        anim.SetBool("isClaw", isClaw);
        if (!isGround||rigi.velocity.y < -0.07f)
        {
            return;
        }
            rigi.velocity = new Vector2(rigi.velocity.x, clawJumpForce * Time.deltaTime);

       
    }
    //碰撞
    /*
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }*/

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            rigi.velocity = new Vector2(0,rigi.velocity.y);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}
