using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleCity
{
    public class PlayerStats : MonoBehaviour
    {
        public int playerID;
        private int score = 0;
        private int highScore = 0;
        [SerializeField] private int level;
        [SerializeField] private float health = 0f;
        private float maxHealth = 100f;
        private int lives = 3;
        private bool isAlive;
        
  
        public string playerName;
        private int currentPlayerLevel;

        public GameObject deathAnimationPrefab;
        private ParticleSystem deathAnimationParticles;
        private AudioSource deathAnimationAudio;

        public Transform[] turretPrefabs;
        public Transform turretMountPoint;

        private Rigidbody turretInstance;

        private PlayerShooting playerShooting;
        private TurretTurn turretTurn;

        
        //private playerManager playerManager;



        private void Awake()
        {
            // Instantiate the explosion prefab and get a reference to the particle system on it.
            deathAnimationParticles = Instantiate(deathAnimationPrefab).GetComponent<ParticleSystem>();
            // Get a reference to the audio source on the instantiated prefab.
            deathAnimationAudio = deathAnimationParticles.GetComponent<AudioSource>();
            //playerManager = playerManager.GetComponentInParent<playerManager>();
            // Disable the prefab so it can be activated when it's required.
            deathAnimationParticles.gameObject.SetActive(false);




        }
        private void OnEnable()
        {
            OnSpawn();

        }

       

        public void DamageTaken(float damageAmount)
        {
            ReduceHealth(damageAmount);
            Debug.Log(damageAmount);
           
        }
        public void LevelUp()
        {
            AddMaximumHealth(25);
            RaiseLevel(1);
            currentPlayerLevel = GetLevel();
            Debug.Log("leveled up! current level is: " + currentPlayerLevel);
            changeTurret(currentPlayerLevel);

        }
        public void OnSpawn()
        {
            if(lives == 0)
            {
                return;
            }
            SetLevel(1);
            currentPlayerLevel = GetLevel();
            Debug.Log("Player is instantiated and has level : " + currentPlayerLevel);
            changeTurret(currentPlayerLevel);
            //DisableChildObjectsWithTag(transform, "Turret", 0, true);
            //turretInstance =
            //  Instantiate(turretPrefabs[currentPlayerLevel-1], turretMountPoint.position, turretMountPoint.rotation, turretMountPoint) as Rigidbody;
            isAlive = true;
            SetHealth(100);
        }

        public void OnDeath( GameObject tankModel)
        {
            isAlive = false;
            SetLevel(1);
            lives--;
            ResetMaximumHealth();
            //Destroy(gameObject);
            gameObject.SetActive(false);
            deathAnimationParticles.transform.position = transform.position;
            deathAnimationParticles.gameObject.SetActive(true);
            deathAnimationParticles.Play();
            deathAnimationAudio.Play();
            
            
        }
        public void OnRespawn(GameObject tankModel) //particles and stuffs
        {
            gameObject.SetActive(true);
        }

            public void changeTurret(int num)
        {
            int playerLevel;
            playerLevel = num-1;
            for (int i = 0; i < turretPrefabs.Length; i++)
            {
                playerShooting = turretPrefabs[i].GetComponent<PlayerShooting>();
                turretTurn = turretPrefabs[i].GetComponent<TurretTurn>();
                playerShooting.playerID = playerID;
                turretTurn.playerID = playerID;
                if (i == playerLevel) {
                   
                    turretPrefabs[i].gameObject.SetActive(true);



                }    
                else turretPrefabs[i].gameObject.SetActive(false);
                    
            }
        }

        public void DisableChildObjectsWithTag(Transform parent, string tag, bool shouldBeActive)
        {
            for (int x = 0; x < parent.childCount; x++)
            {
                Transform child = parent.GetChild(x);
                if (child.tag == tag)
                {
                    child.gameObject.SetActive(shouldBeActive);

                }
                DisableChildObjectsWithTag(child, tag, shouldBeActive);
            }
        }
        public void DisableChildObjectsWithTag(Transform parent, string tag, int nthChild, bool shouldBeActive)
        {
            for (int x = 0; x < parent.childCount; x++)
            {
                Transform child = parent.GetChild(x);
                if (child.tag == tag && x == nthChild)
                {
                    child.gameObject.SetActive(shouldBeActive);
                }
                DisableChildObjectsWithTag(child, tag, nthChild, shouldBeActive);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetLevel(int num)
        {
            level = num;
        }
        public int GetLevel()
        {
            return level;
        }
        public void SetName(int num)
        {
            playerName = "Player " + num;
        }
        public int GetHighScore()
        {
            return highScore;
        }
        public int GetScore()
        {
            return score;
        }
        public virtual void AddScore(int anAmount)
        {
            score += anAmount;
        }
        public void SetScore(int num)
        {
            score = num;
        }
        public float GetHealth()
        {
            return health;
        }
        public void AddHealth(int num)
        {
            health += num;
            if (health > maxHealth)
                health = maxHealth;
        }
        public void ReduceHealth(float num)
        {
            health -= num;
            if (health == 0)
                health = 0;
        }
        public void SetLives(int num)
        {
            lives = num;
        }
        public int GetLives()
        {
            return lives;
        }
        public void AddLives(int num)
        {
            lives -= num;
        }
        public void RemoveLives(int num)
        {
            lives += num;
        }

        public void SetHealth(int num)
        {
            health = num;
        }
        public void AddMaximumHealth(float num)
        {
            maxHealth = maxHealth + num;
        }
        public void ResetMaximumHealth()
        {
            maxHealth = 100f;
        }
        public virtual void RaiseLevel(int num)
        {
            level += num;
        }
    }

}

