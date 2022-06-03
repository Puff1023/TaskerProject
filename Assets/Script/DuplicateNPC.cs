using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateNPC : MonoBehaviour
{
    int totalNPC = 2;
    int NPC_num = 0;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject NPC02_1 = Instantiate(Resources.Load("NPC02", typeof(GameObject)),transform.position,transform.rotation) as GameObject;
        //GameObject NPC02_2 = Instantiate(Resources.Load("NPC02", typeof(GameObject)), transform.position, transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float timing = Random.Range(0, 100);
        if (timing<.5f  && NPC_num < totalNPC)
        {
            GameObject NPC = Instantiate(Resources.Load("NPC", typeof(GameObject))) as GameObject;
            NPC.transform.position = NPC.transform.position + new Vector3(Random.Range(-3,3),0 , Random.Range(-3,3));
            NPC_num++;
        }
    }
}
