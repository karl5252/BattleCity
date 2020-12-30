using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCity
{
    public class Collectibles : MonoBehaviour
    {
        private float maxLifeTime = 15f;

        public int collectiblesID;
        public GameObject powerUpPartPrefab;
        private ParticleSystem powerUpParticles;

        public AudioSource powerUpAudio;
        public AudioClip powerUpSound;

        private PlayerStats player;


        private void Awake()
        {
            powerUpPartPrefab = this.gameObject;
            powerUpParticles = Instantiate(powerUpPartPrefab).GetComponent<ParticleSystem>();
            powerUpParticles.gameObject.SetActive(false);
            
           
        }
        // Start is called before the first frame update
        private void Start()
        {
            // If it isn't destroyed by then, destroy the shell after it's lifetime.
            Destroy(gameObject, maxLifeTime);
            
            
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided with: " + other.name);
            player = other.GetComponentInChildren<PlayerStats>();
            if (player == null) Debug.Log("Player not found");
            //handle to the component
            if (other.tag == "Player")
            {
                if (collectiblesID == 0 || collectiblesID == 1 || collectiblesID == 2)
                {
                    Destroy(this.gameObject);
                }
                //access the player

                //Rigidbody rb = other.GetComponent<Rigidbody>();
                //if (player != null)
               // {
                    ///0 add points
                    ///1 add life
                    ///2 destroy
                    ///3 speed up
                    ///4 extend time
                    ///5 jump
                    ///6 finish
                    switch (collectiblesID)
                    {
                        case 0:
                            player.AddScore(10);
                            //powerUpParticles.Play();
                            //powerUpAudio.Play();
                            break;
                        case 1:
                            player.AddHealth(250);
                            //powerUpParticles.Play();
                           // powerUpAudio.Play();
                            break;
                        case 2:
                            player.LevelUp();
                            //powerUpParticles.Play();
                           // powerUpAudio.Play();
                            break;

                    }







               // }
                
            }
        }
    }
            

}

