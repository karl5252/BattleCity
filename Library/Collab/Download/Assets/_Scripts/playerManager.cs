using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleCity
{
    public class playerManager : MonoBehaviour
    {

        public Color playerColor;
        public bool playerControlled;


        [HideInInspector] public int playerID;            
        [HideInInspector] public string m_ColoredPlayerText;   
        [HideInInspector] public GameObject m_Instance;

        private float health;
        public bool playerDead;
        public bool canBeSpawned = true;
        
        
        private Vector3 defaultPosition;

        //public int [] deadPlayersQueue = new int [1];
        //List<int> deadPlayersQueue = new List<int>();

        private PlayerMovement playerMovement;
        private EnemyMovement enemyMovement;


        private PlayerStats playerStats;  //GetComponent<PlayerStats>();
        private PlayerShooting playerShooting;
        private EnemyShooting enemyShooting;
        private NavMeshAgent navMesh;

        private TurretTurn turretTurn;
        //private PlayerStats playerStatistics;
        //private WaitForSecondsRealtime CountdownToRespawn; 
      

        public Transform SpawnPoint;
        

        public void Setup()
        {


            // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerID + "</color>";
            //Debug.Log("Player color id " + playerColor);
            // Get all of the renderers of the tank.
            //MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
            playerStats = m_Instance.GetComponent<PlayerStats>();
            playerStats.playerID = playerID;
            //playerStats.playerColor = playerColor;
            playerMovement = m_Instance.GetComponent<PlayerMovement>();
            playerMovement.playerID = playerID;
            playerShooting = m_Instance.GetComponentInChildren<PlayerShooting>();
            playerShooting.playerID = playerID;
            turretTurn = m_Instance.GetComponentInChildren<TurretTurn>();
            turretTurn.playerID = playerID;
            enemyMovement = m_Instance.GetComponent<EnemyMovement>();
            enemyShooting = m_Instance.GetComponent<EnemyShooting>();
            enemyShooting.playerID = playerID;
            defaultPosition = m_Instance.transform.position;
            if(playerControlled == true)
            {
                m_Instance.tag = "Player";
                enemyMovement.enabled = false;
                enemyShooting.enabled = false;
            }
            else
            {
                m_Instance.tag = "Bot";

                navMesh = m_Instance.GetComponent<NavMeshAgent>();
                if(navMesh == null)
                {
                    NavMeshAgent nva = m_Instance.AddComponent<NavMeshAgent>() as NavMeshAgent;
                    
                }navMesh = m_Instance.GetComponent<NavMeshAgent>();
                    navMesh.name = "EnemyTank";
                    navMesh.baseOffset = 1;
                    AiControl();
                
                


            }
            setColor();


            // Go through all the renderers...
            /* for (int i = 0; i < renderers.Length; i++)
             {
                 // ... set their material color to the color specific to this tank.
                 renderers[i].material.color = playerColor;




             }*/
        }
        public void DoOnRespawn()
        {
            //CountdownToRespawn = new WaitForSecondsRealtime(8);
            StartCoroutine(RespawnTimer());
           
        }
        public void setColor()
        {
            MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = playerColor;
                Debug.Log("renderer element: " + renderers.Length);
                Debug.Log("renderer name: " + renderers[i].name);




            }
        }
       public void monitorHealth()
        {
            health = playerStats.GetHealth();
            Debug.Log(health);
            if(health < 0)
            {
                playerDead = true;
                int playerLives;
                playerLives = playerStats.GetLives();
                if (playerLives == 0)
                {
                    canBeSpawned = false;
                    Debug.Log("player " + playerID + " you have died last one time");
                }
                else
                {
                    canBeSpawned = true;
                    playerStats.OnDeath(this.m_Instance);
                    //CountdownToRespawn = new WaitForSeconds(5);
                    Debug.Log("player " + playerID + " is dead! Respawn will happen shortly");
                    playerStats.OnSpawn(); //reset player stats
                                           //int arrayLen = deadPlayersQueue.Length;
                                           //deadPlayersQueue = Array.Resize(ref deadPlayersQueue, arrayLen + 1);
                }







            }


        }


        // Used during the phases of the game where the player shouldn't be able to control their tank.
        public void DisableControl()
        {
            playerMovement.enabled = false;
            playerShooting.enabled = false;
            enemyMovement.enabled = false;
            enemyShooting.enabled = false;

            //m_CanvasGameObject.SetActive(false);
        }


        // Used during the phases of the game where the player should be able to control their tank.
        public void EnableControl()
        {
            playerMovement.enabled = true;
            playerShooting.enabled = true;


            //m_CanvasGameObject.SetActive(true);
        }
        public void AiControl()
        {

            
            playerMovement.enabled = false;
            playerShooting.enabled = false;
            enemyMovement.enabled = true;
            enemyShooting.enabled = true;
        }


        public void Reset()
        {
            m_Instance.transform.position = SpawnPoint.position;
            m_Instance.transform.rotation = SpawnPoint.rotation;

            m_Instance.SetActive(false);
            m_Instance.SetActive(true);
        }
        IEnumerator RespawnTimer()
        {
            //Print the time of when the function is first called.
            Debug.Log("Started respawn countdown : " + Time.time);

            //yield on a new YieldInstruction that waits for 8 seconds.
            yield return new WaitForSecondsRealtime(5);
            playerDead = false;
            playerStats.OnRespawn(this.m_Instance);

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }


    }
    
}