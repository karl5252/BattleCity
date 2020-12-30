using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleCity
{
    public class EnemyMovement_inh : Movement
    {
        public GameObject player;
        public Transform playerPosition;
        private NavMeshAgent navMesh;
        public ArtificalIntelligenceStates states;

        public int playerID;

        public float playerSpeed;
        public float playerTurningSpeed;

        

       
        private Rigidbody thisRigidbody;              // Reference used to move the tank.
        
        private float originalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] particleSystems; // References to all the particles systems used by the Tanks


        

        public GameObject leftTankTrack;
        public GameObject rightTankTrack;
        Renderer leftTrackRenderer;
        Renderer rightTrackRenderer;

        public void Awake()
        {
            states = ArtificalIntelligenceStates.WAIT;
        }

        void Start()
        {

            navMesh = gameObject.GetComponent<NavMeshAgent>();
            playerSpeed = navMesh.speed;
            playerTurningSpeed = navMesh.angularSpeed;
            StartCoroutine(EnemyFSM());




        }
        private void FixedUpdate()
        {
            CheckForPlayer();
            //AnimateWheels(playerSpeed, playerTurningSpeed);
            //AnimateTracks(playerSpeed, rightTrackRenderer, leftTrackRender);

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
                navMesh.destination = playerPosition.position;

            }



        }
        public override void AnimateTracks(float playerSpeed, float movementInputValue, float turnInputValue, Renderer rightTrackRenderer, Renderer leftTrackRenderer)
        {
            base.AnimateTracks(playerSpeed, movementInputValue, turnInputValue, rightTrackRenderer, leftTrackRenderer);


        }
        public override void AnimateWheels(float playerSpeed, float playerTurningSpeed, float movementInputValue, float turnInputValue)
        {
            base.AnimateWheels(playerSpeed, playerTurningSpeed, movementInputValue, turnInputValue);


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
                yield return new WaitForSeconds(5.0f);

                Debug.Log("Sitting here...");

                yield return new WaitForSeconds(5.0f);

                Debug.Log("...and waiting!");
            }

            // EXIT THE IDLE STATE

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
