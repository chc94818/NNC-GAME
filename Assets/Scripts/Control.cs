using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
    private Rigidbody2D rigi;
    private Animator anim;
    public float speed = 200.0f;
    public float jumpForce = 200.0f;
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

        if (Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow))
        {
            Move(0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
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
