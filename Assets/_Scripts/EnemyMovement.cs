using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleCity
{

    public class EnemyMovement : MonoBehaviour
    {

        public GameObject player;
        public Transform playerPosition;

        public float maximumDistance = 15f;
        public float idleRange = 10.0f;
        public float safeDistance = 3.0f;

        public float moveSpeed = 3.0f;
        public float rotateSpeed = 3.0f;

        private NavMeshAgent navMesh;
        public ArtificalIntelligenceStates states;
        
        public void Awake()
        {
            states = ArtificalIntelligenceStates.WAIT;
            
        }

        void Start()
        {

            GetComponent<Rigidbody>().isKinematic = false;
            navMesh = gameObject.GetComponent<NavMeshAgent>();
            StartCoroutine(EnemyFSM());




        }
        private void FixedUpdate()
        {
            CheckForPlayer();

        }
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            float theta = 0;
            float x = safeDistance * Mathf.Cos(theta);
            float y = safeDistance * Mathf.Sin(theta);
            Vector3 pos = transform.position + new Vector3(x, 0, y);
            Vector3 newPos = pos;
            Vector3 lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = safeDistance * Mathf.Cos(theta);
                y = safeDistance * Mathf.Sin(theta);
                newPos = transform.position + new Vector3(x, 0, y);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }
            
        }

        private void CheckForPlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (!player)
            {
                //StartCoroutine(waitForPlayerToSpawn(player, player.transform));
                //StartCoroutine(waitForPlayerToSpawn2(player.transform));
                StartCoroutine(waitForPlayerToSpawn1());

            }
            else
            {
                playerPosition = player.transform;
                //navMesh.destination = playerPosition.position;

            }



        }

        public float GetDistance()
        {
            return (transform.position -
            playerPosition.transform.position).magnitude;
        }
        private void RotateTowardsTarget()
        {
            transform.rotation =
            Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(playerPosition.position - transform.position),
            rotateSpeed * Time.deltaTime);
        }


        public enum ArtificalIntelligenceStates
        {
            WAIT = 0,
            CHASE = 1,
            KEEP_DISTANCE = 2
        }
        IEnumerator EnemyFSM()
        {
            while (true)
            {
                yield return StartCoroutine(states.ToString());
                
            }
        }
        IEnumerator WAIT()
        {
            // ENTER THE IDLE STATE
            Debug.Log("Alright, seems no evil Player is around, I can chill!");

            // EXECUTE IDLE STATE
            while (states == ArtificalIntelligenceStates.WAIT)
            {
                yield return new WaitForSeconds(2.0f);

                Debug.Log("Sitting here...");
                if (GetDistance() < maximumDistance)
                {
                    states = ArtificalIntelligenceStates.CHASE;
                    Debug.Log("Definately smells like a playah! " + GetDistance());
                }
                yield return 0;

            }
            EnemyFSM();

            // EXIT THE IDLE STATE

            Debug.Log("Uh, I guess I smell a Playah!");
        }
        IEnumerator CHASE()
        {
            Debug.Log("Oh boy, I will get Playah!");
            // EXECUTE IDLE STATE
            while (states == ArtificalIntelligenceStates.CHASE)
            {
                RotateTowardsTarget();
                transform.position =
                Vector3.MoveTowards(transform.position,
                playerPosition.position,
                Time.deltaTime * moveSpeed);
                //navMesh.destination = playerPosition.position;

                if (GetDistance() <= safeDistance)
                {
                    states = ArtificalIntelligenceStates.KEEP_DISTANCE;
                }
                yield return 0;
            }

            // EXIT THE CHASE STATE
            EnemyFSM();

        }
        IEnumerator KEEP_DISTANCE()
        {
            // ENTER THE DISTANCING STATE
            Debug.Log("WHOOA too close to playah");

            // EXECUTE DISTANCING STATE
            while (states == ArtificalIntelligenceStates.KEEP_DISTANCE)
            {
                RotateTowardsTarget();
                transform.position =
                Vector3.MoveTowards(transform.position,
                playerPosition.position,
                Time.deltaTime * -moveSpeed);

                //RotateTowardsTarget();
                if (player == null)
                {
                    states = ArtificalIntelligenceStates.WAIT;
                }
                if (GetDistance() > safeDistance)
                {
                    states = ArtificalIntelligenceStates.CHASE;
                }
                yield return 0;
            }
            EnemyFSM();

            // EXIT THE KEEP_DISTANCE STATE

            Debug.Log("Uh, I guess I smell a Player!");
        }
        IEnumerator waitForPlayerToSpawn1()
        {
            float counter = 0;
            int waitTimeInterval = 8;

            while (player == null)
            {
                //Increment Timer until counter >= waitTimeInterval
                counter += Time.deltaTime;
                Debug.Log("We have waited for: " + counter + " seconds");
                if (counter >= waitTimeInterval)
                {
                    //Quit function
                    yield break;
                }
                //Wait for a frame so that Unity doesn't freeze
                yield return null;
            }
        }







    }









}
/*public GameObject PlayerObject;
void Start()
{
    int number = (int)mState;
    Debug.Log(number);
    Vector3 pos = new Vector3();
    pos.x = Random.Range(-10f, 10f);
    pos.z = Random.Range(-10f, 10f);
    transform.position = pos;
    GameObject[] AllGameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
    foreach (GameObject aGameObject in AllGameObjects)
    {
        Component aComponent = aGameObject.GetComponent(typeof(Player));
        if (aComponent != null)
        {
            PlayerObject = aGameObject;
        }
    }
}*/
/*Void Update()
{
    Vector3 MyVector = PlayerObject.transform.position - transform.position;
    float DistanceToPlayer = MyVector.magnitude;
    Start with a new Vector3 made from subtracting our current position from the position we’re headed toward.Then to get the magnitude of that vector, we create a float called DistanceToPlayer and set that to the MyVector.magnitude.If you remember anything from math class, some of this should sound familiar.
  Now we have data to make use of with some logic.We’ll surround the code that moves the monster with an if statement controlled by some arbitrary number. If we are more than 3 units away from the player, then move toward him.
Vector3 MyVector =
  PlayerObject.transform.position - transform.position;
float DistanceToPlayer = MyVector.magnitude;
if (DistanceToPlayer > 3.0f)
{
    Direction =
    Vector3.Normalize(PlayerObject.transform.position - transform.position);
    transform.position += Direction * 0.1f;
}*/

/*float DistanceToPlayer = (PlayerObject.transform.position - transform.position).magnitude;
Likewise, we don’t even need the DistanceToPlayer to be calculated before it’s used. This means we can do the calculation inside of the if statement’s parameters.
if (PlayerObject.transform.position - transform.position).magnitude > AttackRange)
Again we can reduce this even more. Calculating the Direction and then using its value may also be done in the line of code where it’s eventually used.
if ((PlayerObject.transform.position - transform.position).magnitude > AttackRange)
{
transform.position +=
Vector3.Normalize(PlayerObject.transform.position -
transform.position) * SpeedMultiplyer;
}*/






