using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTracksControl : MonoBehaviour
{

    public GameObject[] rightSideWwheels;
    public GameObject[] leftSideWwheels;





    
    void fixedUpdate()
    {

    }
    public void AnimateWheels()
    {
        //forwards
        //backcwards
        //turn right
        //turn left
        for (int i = 0; i < rightSideWwheels.Length; ++i)
        {
            rightSideWwheels[i].transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);

        }



    }
    public void AnimateTracks()
    {
        //forwards
        //backcwards
        //turn right
        //turn left
    }




}