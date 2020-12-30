using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleCity
{
    public class EnemyShooting : MonoBehaviour
    {
        public int playerID;              // Used to identify the different players.
        //fire type 1
        public Rigidbody shellPrefab;
        public Transform barrelBore;           // A child of the tank where the shells are spawned.
        //fire type 2
        public Rigidbody ammoPrefab;
        public Transform sideWeaponMount;




        public AudioSource shootingAudio;
        public AudioClip playerReloadClip;
        public AudioClip playerFireClip;

        //public GameObject barrelSmoke;

        public float launchForce = 250f;

        /* private string fire1TriggerButton;
         private string fire2TriggerButton;*/
         private float reloadTimeFire1;        
         private float reloadTimeFire2;
        public float reloadOffset = 3f;
        public float maxRange = 10.0f;
        public float minRange = 3.0f;
        //private bool onRange = false;
        private bool fired1;
        private bool fired2;
        

        private void Start()
        {
          
        }
        private void Update()
        {
            

            RaycastHit hit;
            //int layerMask = 9;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, maxRange/*, layerMask*/))
            {
               
                    Debug.Log("collision with rigidbody with tag " + hit.rigidbody.tag);
                
                if (Time.time >= reloadTimeFire1 && hit.rigidbody.tag == "Player")
                {
                    //transform.LookAt();
                    Fire1();
                    reloadTimeFire1 = Time.time + reloadOffset;
                    fired1 = false;
                }
                else
                {
                    return;
                }
            }

        }



        private void Fire1()
        {
            // Set the fired flag so only Fire is only called once.
            fired1 = true;
            shootingAudio.clip = playerFireClip;
            shootingAudio.Play();

            // Create an instance of the shell and store a reference to it's rigidbody.
            GameObject shell = ObjectsLake.SharedInstance.GetPooledObject("tankShell");
            if (shell != null)
            {
                shell.transform.position = barrelBore.transform.position;
                shell.transform.rotation = barrelBore.transform.rotation;
                shell.GetComponent<Rigidbody>().velocity = launchForce * barrelBore.forward;
                shell.SetActive(true);

            }
            //barrelSmoke = Instantiate(barrelSmoke, barrelBore.position, barrelBore.rotation) as GameObject;

            //shootingAudio.clip = playerReloadClip;
            //shootingAudio.Play();





        }
        private void Fire2()
        {
            // Set the fired flag so only Fire is only called once.
            fired2 = true;

        }
        void OnDrawGizmos()
            
        {

            // Draw a yellow sphere at the transform's position
            
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRange))
            {

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.localPosition, Vector3.forward);
            }
            
            else
            {

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.localPosition, Vector3.forward);
            }
        }

    }

}


/*We’ll start with a simple system to get an ArrayList of objects in the scene.
public GameObject[] GameObjectArray;
//Use this for initialization
void Start ()
{
ArrayList aList = new ArrayList();
GameObject[] gameObjects =
(GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
foreach(GameObject go in gameObjects)
{
if(go.name == "Sphere")
{
aList.Add(go);
}
}
GameObjectArray =
aList.ToArray(typeof(GameObject)) as GameObject[];
}
This creates a new ArrayList called aList using ArrayList aList = new ArrayList(); at the beginning of the Start () function. To populate this list, we need to get all of the GameObjects in the scene with GameObject[] gameObjects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));, which does a few different tasks in one statement.
First, it creates a new GameObject[] array called gameObjects. Then we use GameObject.FindObjectsOfType() and assign the function in GameObject the typeof(GameObject); this returns a new array of every GameObject in the scene.
After we get all of our data, we use a foreach loop foreach(GameObject go in gameObjects) to check through all of the different objects. In the parameters of the foreach, we create a GameObject go that stores the current iteration as we go through the gameObjects array. Then we filter the objects and take only the ones named Sphere using the if statement if(go.name == "Sphere") which adds the go to the aList array if the names match.
Finally, the aList is assigned to GameObjectArray which was created at the class scope. The statement is fulfilled by aList.ToArray() that takes the argument typeof(GameObject). We then convert the returned data to GameObject[] by a cast as GameObject[]; at the end of the statement.
Now we’re ready to sort through the GameObjectArray based on distance.
void sortObjects(GameObject[] objects, out GameObject[] sortedObjects)
{
for(int i = 0; i < objects.Length-1; i++)
{
Vector3 PositionA = objects[i].transform.position;
Vector3 PositionB = objects[i+1].transform.position;
Vector3 VectorToA = PositionA - transform.position;
Vector3 VectorToB = PositionB - transform.position;
float DistanceToA = VectorToA.magnitude;
float DistanceToB = VectorToB.magnitude;
if(DistanceToA > DistanceToB)
{
GameObject temp = objects[i];
objects[i] = objects[i+1];
objects[i+1] = temp;
}
}
sortedObjects = objects;
}
Intermediate 409
Here we have a new function added to our class. What we do here is simple and works only if it’s iterated a few times. The idea is that we need to discover the distances between two objects in the array. Compare the distances, and if one distance is greater than another, then swap the two objects in the array. As we repeat this, we rearrange the objects in the array with each pass. As this can repeat any number of times, we’re doing things in a very inefficient manner, but it’s getting the job done. We’ll look at optimizing this loop again in this section, in which we go further into sorting algorithms.
The function starts with GameObject[] objects, which accepts an array of GameObjects called objects. Then we start up a for loop to check through each object. This begins with for(int i = 0; i < objects.Length-1; i++); what is important here is that we don’t want to iterate through to the last object in the array. We want to stop one short of the end. We’ll see why as soon as we start going through the rest of the loop.
Next we want to get the location of the current object and the next object. This is done with Vector3 PositionA = objects[i].transform.position;. Then we use Vector3 PositionB = objects[i+1].transform.position; to get the next object in the list. You’ll notice objects[i+1] adds 1 to i that will reach the end of the array. If we used i < objects.Length, and not i < objects.Length-1, we’d be looking at a place in the array that doesn’t exist. If there were 10 objects in the scene, the array is Length 10, which means that there’s no objects[11]. Looking for objects[11] results in an index out of range error.
Then we get some vectors representing the object’s position minus the script’s position. This is done with Vector3 VectorToA = PositionA - transform.position; and Vector3 VectorToB = PositionB - transform.position;. Then these are converted to magnitudes, which is a math function that can be done to a Vector3. This gives us two float values to compare float DistanceToA = VectorToA.magnitude; and float DistanceToB = VectorToB.magnitude;.
Finally, we need to compare distance and do a swap if A is greater than B.
if(DistanceToA > DistanceToB)
{
GameObject temp = objects[i];
objects[i] = objects[i+1];
objects[i+1] = temp;
}
We use a GameObject temp to store the object[i] that is going to be written over with the swap. The swap begins with objects[i] = objects[i+1]; where we will for a single statement have two copies of the same object in the array. This is because both objects[i] and objects[i+1] are holding on to the same GameObject in the scene. Then to finish the swap, we replace the objects[i+1] with temp that holds onto objects[i].
To send the sorted array back, we use sortedObjects = objects; and the out parameter pushes the array back up out of the function into where it was called on. To check on the sorting, we’ll use the following code in the Update () function:
void Update ()
{
sortObjects(GameObjectArray, out GameObjectArray);
for(int i = 0; i < GameObjectArray.Length; i++)
{
Vector3 PositionA =
GameObjectArray[i].transform.position;
Debug.DrawRay(PositionA, new Vector3(0, i * 2f, 0),
Color.red);
}
}
410 Learning C# Programming with Unity 3D
Call on the new function, sortObjects(GameObjectArray, out GameObjectArray). Woah! You can do that? Yes you can! You can have both in and out parameters reference the same array variable. What this will do is that it will take in the array, sort it, and send it back out sorted! Once the array has been sorted, it can be sorted again and again.*/


