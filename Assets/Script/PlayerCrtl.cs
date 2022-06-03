//Player Movement script 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class PlayerCrtl : MonoBehaviour
{
    public Animator PlayerAni;
    public CharacterController Player;
    float MovSpeed;
    public Vector3 MoveDirection = Vector3.zero;
    public float Gravity = 10;
    static public  bool kickStatus = false;
    static public  bool AttackStatus = false;
    public Image PlayerHPui;
    public int PlayerHP = 100;
    public bool playWeapon = false;
    public Transform Player_HPBar;
    public Transform mCamera2;
    public GameObject playerSword;
    public GameObject AttackButton;
    public VariableJoystick joystick;
    public float jumpHigh = 0;
    public bool kickButtom = false;
    public bool attackButtom = false;
    public bool jumpButtom = false;
    void Start()
    {
        Player_HPBar = transform.Find("Player_Canvas");
        mCamera2 = GameObject.Find("Main Camera").transform;
        PlayerAni = GetComponent<Animator>();
        Player = GetComponent<CharacterController>();
        joystick = FindObjectOfType<VariableJoystick>();
        //PlayerHP = gameObject.Find("Player-HP");
    }
    // Update is called once per frame
    void Update()
    {
        Player_HPBar.LookAt(mCamera2);
        PlayerHPui.fillAmount = PlayerHP * 0.01f;

        if (Player.isGrounded)
        {
            print(joystick.Vertical);
            MovSpeed = Input.GetAxis("Vertical") + joystick.Vertical;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MovSpeed *= 6;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                jumpHigh = 6;
            }
            else
            {
                jumpHigh = 0;
            }
            MoveDirection = new Vector3(0, jumpHigh, MovSpeed);
            MoveDirection = transform.TransformDirection(MoveDirection);
            PlayerAni.SetFloat("PlayerMoveSpeed", Mathf.Abs(MovSpeed));
            float turn = Input.GetAxis("Horizontal")+joystick.Horizontal;
            transform.Rotate(0, turn*2, 0);

        }
        if(PlayerAni.GetFloat("KickingTime")>0.1f)
        {
            kickStatus = true;
        }
        else
        {
            kickStatus = false;
        }

        if (PlayerAni.GetFloat("KickingTime") > 0.01f ||PlayerAni.GetFloat("SwordingTime") > 0.01f)
        {
            AttackStatus = true;
        }
        else
        {
            AttackStatus = false;
        }

        if (Input.GetKey(KeyCode.K) ||kickButtom)
        {
           
            PlayerAni.SetBool("kicking", true);
        }
        else
        {
            PlayerAni.SetBool("kicking", false);
        }
        if (playWeapon &&Input.GetKey(KeyCode.L) ||attackButtom)
        {

            PlayerAni.SetBool("Sword", true);
        }
        else
        {
            PlayerAni.SetBool("Sword", false);
        }
       

        MoveDirection.y -= Gravity * Time.deltaTime;
        Player.Move(MoveDirection * Time.deltaTime);
    }

    public void KickButtomUP()
    {
        kickButtom = false;
    }
    public void KickButtomDown()
    {
        kickButtom = true;
    }
    public void AttackButtomUP()
    {
        attackButtom = false;
    }
    public void AttackButtomDown()
    {
        attackButtom = true;
    }

    private void OnTriggerEnter (Collider HPhit)
    {
        if ((HPhit.gameObject.name == "NPC02-LeftHand" || HPhit.gameObject.name == "NPC02-RightHand") && NPC02ctrl.NPC2_PunchStatus )
        {
           
            PlayerHP -= 10;
        }

        if (HPhit.gameObject.tag =="weapon")
        {
            playWeapon = true;
            playerSword.SetActive(true); //玩家手上的刀變為可見
            AttackButton.SetActive(true);
            Destroy(HPhit.gameObject);
        }


    }
}
