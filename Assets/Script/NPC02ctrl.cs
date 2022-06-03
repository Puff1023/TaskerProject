using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC02ctrl : MonoBehaviour
{
    public NavMeshAgent NPC2_navi;
    public Transform target;
    public Animator NPC2_ani;
    public Image NPC02_HPui;
    public int NPC02_HP = 100;
    public int NPC_DetecDist = 8;
    static public bool NPC2_PunchStatus = false;
    public int PlayerToeAttackHP = 40; //NPC被玩家踢到的扣血量
    public int PlayerSwordAttkHP = 100;
    public Transform NPC02_HPBar;
    public Transform mCamera;
    void Start()
    {
        NPC2_navi = GetComponent<NavMeshAgent>();
        NPC2_ani = GetComponent<Animator>();

        target = GameObject.Find("PIayer_idle").transform; //取得Player的transform
        NPC02_HPBar = transform.Find("NPC02_Canvas");
        mCamera = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        NPC02_HPBar.LookAt(mCamera); //NPC血條永遠面向攝影機
        NPC02_HPui.fillAmount = NPC02_HP * 0.01f;
        float dist = Vector3.Distance(target.position, transform.position);  //計算NPC與Player之距離

        
        float NPC02walkspeed = NPC2_navi.velocity.magnitude; //取得NPC走路速度

        NPC2_ani.SetFloat("NPC02walkspeed", NPC02walkspeed);

        if (NPC02walkspeed<0.01f & dist<= 1.65 && !NPC2_PunchStatus) //判斷NPC目前速度是否小於0.01,Player之距離是否<1.65
        {
            NPC2_ani.SetBool("NPC02punch", true);
        }
        else
        {
            NPC2_ani.SetBool("NPC02punch", false);
        }

        if (NPC2_ani.GetFloat("NPC02-PunchTime") > 0.1f)
        {
            NPC2_PunchStatus = true;
        }
        else
        {
            NPC2_PunchStatus = false;
        }

        if (dist < NPC_DetecDist)
        {
            NPC2_navi.destination = target.position; //NPC跟隨目標的位置

        }
        else
        {
            NPC2_navi.destination = transform.position;
        }

        if(NPC02_HP < 0)
        {
            NPC2_ani.SetBool("NPC02dying", true);

            Destroy(gameObject, 3);
        }




    }

    private void OnTriggerEnter(Collider NPChit)
    {
        //判斷NPC 是否被玩家的腳踢到
        if((NPChit.gameObject.name == "Player-LeftToeBase"|| NPChit.gameObject.name == "Player-RightToeBase") && PlayerCrtl.kickStatus)
        {
            NPC02_HP -= PlayerToeAttackHP;

        }
        if(NPChit.gameObject.name == "Sword" && PlayerCrtl.AttackStatus)
        {
            NPC02_HP -= PlayerSwordAttkHP;
        }
        
    }
}
