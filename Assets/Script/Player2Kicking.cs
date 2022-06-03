using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Kicking : MonoBehaviour
{
    // Start is called before the first frame update

    public TrailRenderer kickingTrail;

    void Start()
    {
        kickingTrail = GetComponent<TrailRenderer>();

        kickingTrail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCrtl.kickStatus)
        {
            kickingTrail.enabled = true;
        }
        else
        {
            kickingTrail.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider kicking)
    {

        //print("kicking Trigger....");


       // if (kicking.gameObject.name == "Cube")
       if(kicking.gameObject.tag =="Box" && PlayerCrtl.kickStatus)
        {
            print("kicking a cube");
            Destroy(kicking.gameObject);
        
        }

    }



}
