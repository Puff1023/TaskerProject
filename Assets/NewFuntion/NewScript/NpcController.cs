using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    //Npc追蹤玩家&攻擊玩家扣血&遭受玩家攻擊並扣血&Npc死亡判定//

    public static NpcController ins;

    public NavMeshAgent NPC2_navi;
    public Transform target;
    public Animator NPC2_ani;
    public Image NPC02_HP;
    public int NPC_DetecDist = 8;
    public Transform NPC02_HPBar;
    public Transform Camera;
    public Image PlayerHp;
    public bool PlayerHurt;
    public bool CheckNpcDeath;
    private void Awake()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        NPC02_HPBar.LookAt(Camera);
        float dist = Vector3.Distance(target.position, transform.position);  //計算NPC與Player之距離
        float NPCwalkspeed = NPC2_navi.velocity.magnitude; //取得NPC走路速度
        NPC2_ani.SetFloat("NPC02walkspeed", NPCwalkspeed);
        NPC2_ani.SetFloat("NPC02-PunchTime", dist);
        if (NPCwalkspeed < 0.1f && dist <= 1.65f ) //判斷NPC目前速度是否小於0.1,Player之距離是否<1.65
        {
            NPC2_ani.SetBool("NPC02punch", true);
            NPC2_navi.speed = 0;
            if (PlayerHurt)
            {
                PlayerHp.fillAmount -= 0.001f;//扣血
            }
        }
        else
        {
            NPC2_ani.SetBool("NPC02punch", false);
            NPC2_navi.speed = 3.5f;
        }
        if (dist < NPC_DetecDist)
        {
            NPC2_navi.destination = target.position; //NPC跟隨目標的位置
        }


        if (CheckNpcDeath)
        {
            if (NPC02_HP.fillAmount <= 0)
            {
                NPC2_ani.SetBool("NPC02dying", true);
                Destroy(gameObject, 3f);
                Invoke("NpcLose", 3f);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag=="PlayerAttack")
    //    {
    //        PlayerAttack.ins.Playerattack = true;
    //    }
    //}

    private void OnTriggerEnter(Collider NPChit)
    {
        //判斷NPC 是否被玩家的腳踢到
        if (NPChit.tag=="PlayerAttack" && PlayerAttack.ins.Playerattack==true)
        {
            NPC02_HP.fillAmount -= 0.1f;
        }
    }

    public void NpcLose()
    {
        CheckNpcDeath = false;
    }
}
