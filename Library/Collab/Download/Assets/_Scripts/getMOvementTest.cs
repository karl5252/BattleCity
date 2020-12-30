using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMOvementTest : MonoBehaviour
{
    public Vector3 oldPos;
    public Vector3 newPos;
    public GameObject myobject;

    private void Start()
    {
        myobject = this.gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        Ray hit = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        oldPos = myobject.transform.position;
        newPos = new Vector3(myobject.transform.localScale.x, myobject.transform.localScale.y / 2, myobject.transform.localScale.z);
        myobject.GetComponent<Rigidbody>().MovePosition(newPos);

        Vector3 velocity = (newPos - oldPos);
        Vector3 direction = velocity.normalized;
        float worldDegrees = Vector3.Angle(Vector3.forward, direction); // angle relative to world space
        float localDegrees = Vector3.Angle(myobject.transform.forward, direction); // angle relative to last heading of myobject

        Debug.Log("ai object velocity: " + velocity + " direction: " + direction);
       // Debug.Log("Heading towards " + worldDegrees);
    }
}
