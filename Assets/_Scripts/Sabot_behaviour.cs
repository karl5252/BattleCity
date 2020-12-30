using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabot_behaviour : MonoBehaviour
{   private float sabotEjectionTime;
    public float sabotTimer = 2f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //transform.localRotation = Quaternion.identity;
        transform.localPosition = transform.parent.position;
        if (Time.time >= sabotEjectionTime)
        {
            
            sabotEjectionTime = Time.time + sabotTimer;
           
            transform.parent = null;


        }


    }
}
