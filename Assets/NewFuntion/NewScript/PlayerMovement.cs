using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement ins;

    //public GameObject FootStep;
    public CharacterController controller;
    public float speed=50;

    public float gravity = -9.81f; //重力
    public float jumpHeight = 3f;  //跳的高度

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;

    public Joystick joystick;
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //Music_manager.ins.PlayerBGM();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime; //重力

        controller.Move(velocity * Time.deltaTime);

        float x = joystick.Horizontal; //水平行動板搖桿
        float z = joystick.Vertical;  //垂直行動板搖桿
        /*float x = Input.GetAxis("Horizontal"); //水平 A、D鍵
        float z = Input.GetAxis("Vertical");  //垂直 W、S鍵*/

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            //MusicManager.ins.PlayFootStep();
            /*if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 8;
                //Music_manager.ins.Footstep_player.Pause();
                //Music_manager.ins.Footstep_player.clip = Music_manager.ins.Footstep_clip[1];
                //Music_manager.ins.Footstep_player.Play();
            }
            else
            {
                speed = 10;
                //Music_manager.ins.Footstep_player.Pause();
                //Music_manager.ins.Footstep_player.clip = Music_manager.ins.Footstep_clip[0];
                //Music_manager.ins.Footstep_player.Play();
            }*/
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            //MusicManager.ins.StopFootStep();
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

    }

    public void  JumpButtomDown()//跳
    {
        Debug.Log("跳");
        if (isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        if (!isGrounded)
        {
            //Music_manager.ins.StopFootStep();
        }
    }

    public void RunButtomDown()//跑
    {
        speed = 30;
    }
    public void RunButtomUp()//走
    {
        speed = 10;
    }
}
