using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    //玩家攻擊Npc//

    public static PlayerAttack ins;
    public Animator KickAni;
    public Animator SwordAni;
    public bool Playerattack;
    private void Awake()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (KickAni.GetFloat("KickingTime")>0)
        {
            PlayerMovement_JoyStick.ins.speed = 0;
        }


        //以下測試用//
        /*if (Input.GetKey(KeyCode.Q))
        {
            KickButtomDown();
        }
        else
        {
            KickButtomUp();
        }
        if (Input.GetKey(KeyCode.W))
        {
            SwordButtomDown();
        }
        else
        {
            SwordButtomUp();
        }*/
        //以上測試用//
    }

    public void KickButtomDown()//踢
    {
        KickAni.SetBool("kicking", true);
        Playerattack = true;
    }
    public void KickButtomUp()
    {
        KickAni.SetBool("kicking", false);
        Playerattack = false;
    }

    public void SwordButtomDown()//揮劍
    {
        SwordAni.SetBool("Sword", true);
        Playerattack = true;
    }
    public void SwordButtomUp()
    {
        SwordAni.SetBool("Sword", false);
        Playerattack = false;
    }
}
