using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Add to solid destructible object.
 */

public class Destructible : MonoBehaviour
{
    public GameObject fracturedMesh;
    //public GameObject powerUpObject;
    public GameObject destructionEffect;

   
   
        //if (/*collison triggered*/ Input.GetKeyDown(KeyCode.E))
        private void OnCollisionEnter(Collision collision)
        { 
            {
            //Instantiate(destructionEffect, transform.position, Quaternion.identity);
            destructionEffect = Instantiate(destructionEffect, transform.position, Quaternion.identity) as GameObject;
            //destructionEffect.play
            GameObject fracturedMeshObject = Instantiate(fracturedMesh, transform.position, Quaternion.identity) as GameObject;
            Destroy(fracturedMeshObject, 4.0f);
            Destroy(destructionEffect, 2.0f);
            Rigidbody[] allRigidbodies = fracturedMeshObject.GetComponentsInChildren<Rigidbody>(); //remember to add for each fractured piece rigidbody component and convex mesh colider for this to work
            if (allRigidbodies.Length > 0)
            {
                foreach (var body in allRigidbodies)
                {
                    body.AddExplosionForce(15, transform.position, 100);
                }
            }
            Destroy(this.gameObject);
            //Add tidy up. Maybe after coroutine...
        }
    }
}
