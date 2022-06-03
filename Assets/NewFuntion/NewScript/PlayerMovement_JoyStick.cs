using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_JoyStick : MonoBehaviour
{
    public static PlayerMovement_JoyStick ins;

    public CharacterController controller;
    public Joystick joystick;
    public Animator PlayerAni;
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform Player_HPBar;
    public Transform Camera;
    public float gravity = -9.81f; //重力
    public float jumpHeight = 3f;  //跳的高度
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public Animator JumpAni;
    public bool isGrounded;

    private void Awake()
    {
        ins=this;
    }

    // Update is called once per frame
    void Update()
    {
        Player_HPBar.LookAt(Camera);

        float x = joystick.Horizontal; //水平行動板搖桿
        float z = joystick.Vertical;  //垂直行動板搖桿

        Vector3 direction = new Vector3(x, 0f, z);
        PlayerAni.SetFloat("PlayerMoveSpeed", Mathf.Abs(speed));

        if (direction.magnitude>=0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            speed = 5;
            //MusicManager.ins.PlayFootStep();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                RunButtomDown();
                //Music_manager.ins.Footstep_player.Pause();
                //Music_manager.ins.Footstep_player.clip = Music_manager.ins.Footstep_clip[1];
                //Music_manager.ins.Footstep_player.Play();
            }
            else
            {
                RunButtomUp();
                //Music_manager.ins.Footstep_player.Pause();
                //Music_manager.ins.Footstep_player.clip = Music_manager.ins.Footstep_clip[0];
                //Music_manager.ins.Footstep_player.Play();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                JumpButtomDown();
            }
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            speed = 0;
            RunBar.ins.CurrentEndurance += 0.001f;
            //MusicManager.ins.StopFootStep();
        }
        //跳躍機制開始//
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            JumpAni.SetBool("Jump", false);
        }

        velocity.y += gravity * Time.deltaTime; //重力
        controller.Move(velocity * Time.deltaTime);
        //跳躍機制結束//
    }

    public void JumpButtomDown()//跳
    {
        Debug.Log("跳");
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            JumpAni.SetBool("Jump", true);
        }

        if (!isGrounded)
        {
            //Music_manager.ins.StopFootStep();
        }
    }

    public void RunButtomDown()//跑
    {
        speed = 10;
        RunBar.ins.CurrentEndurance -= 0.001f;
    }
    public void RunButtomUp()//走
    {
        speed = 5;
        RunBar.ins.CurrentEndurance += 0.001f;
    }
}
