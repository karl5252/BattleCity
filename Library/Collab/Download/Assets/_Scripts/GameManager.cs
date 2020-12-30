using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //4text
using UnityEngine.SceneManagement;


namespace BattleCity
{
    public class GameManager : MonoBehaviour
    {
        public float startDelayTime = 3f;

        public Text messagesText;

        public GameObject vehiclePrefab;
       
        
        public playerManager[] playersManager;
        public Scene scene;
        public int gameLevelNumber;
        private int levelsCount;

        List<int> deadPlayersQueue = new List<int>();

        

        bool pausedGame;
       

        private WaitForSeconds WaitOnStart;


        public virtual void Start()
        {
            levelsCount =  SceneManager.sceneCountInBuildSettings;

            WaitOnStart = new WaitForSeconds(startDelayTime);
            scene = SceneManager.GetActiveScene();
            gameLevelNumber = scene.buildIndex;
            Debug.Log("Active Scene is '" + scene.buildIndex + "'.");
            SpawnAll();
            //change SPawnmAll to SpawnPlayers and SpawnBots

        }

        private void Update()
        {
            for (int i = 0; i < playersManager.Length; i++)
            {
                playersManager[i].monitorHealth();
                Respawn(i);
            }
            // check player level
            //deinstantiate turret
            //instantiate turret according to player level
        }

        private void SpawnAll() //divide into two maybe? Array of players and array of enemies + tank prefabs hence enemy can be spawned as multiple types and player can only be spawned the one he choose
        {
            for (int i = 0; i < playersManager.Length; i++)
            {
                // ... create them, set their player number and references needed for control.
                playersManager[i].m_Instance = Instantiate(vehiclePrefab, playersManager[i].SpawnPoint.position, playersManager[i].SpawnPoint.rotation) as GameObject;
                //cache reference for turret Transform
                //instantiate turret based on level
                playersManager[i].playerID = i + 1;
                playersManager[i].Setup();
            }
        }
        private void Respawn(int i)
        {
            //for (int i = 0; i < playersManager.Length; i++)
            //{
                if(playersManager[i].playerDead == true && playersManager[i].canBeSpawned == true)
                {
                // playersManager[i].Setup();
                
                Debug.Log("Respawning player " + playersManager[i].playerID);
                //playersManager[i].m_Instance = Instantiate(vehiclePrefab, playersManager[i].SpawnPoint.position, playersManager[i].SpawnPoint.rotation) as GameObject;

                playersManager[i].m_Instance.transform.position = playersManager[i].SpawnPoint.position;
                playersManager[i].m_Instance.transform.rotation = playersManager[i].SpawnPoint.rotation;
                
                playersManager[i].DoOnRespawn();  //activates and changes flag to FALSE
                //playersManager[i].gameObject.SetActive(true); 
                //playersManager[i].playerDead = false;
                
                }
                //int deadPlayerId = playersManager[i].playerID;
               
           // }
        }



        public virtual void RestartGame()
        {
            SceneManager.LoadScene(gameLevelNumber);
        }
        public void StartNextLevel()
        {
            
            if (gameLevelNumber >= levelsCount)
                gameLevelNumber = 0;
            SceneManager.LoadScene(gameLevelNumber);
            gameLevelNumber++;
        }
        public bool Paused
        {
            get
            {
                return Paused;
            }
            set
            {
                pausedGame = value;
                if (pausedGame)
                {
                    Time.timeScale = 0.0f;
                }
                else
                {
                    Time.timeScale = 1.0f;
                }
            }
        }
    }

}
