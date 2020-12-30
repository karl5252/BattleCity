using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCity
{
    public class Shell : MonoBehaviour
    {
        [Tooltip("Layers this projectile can collide with")]
        public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
        [Tooltip("Particles to play on impact")]
        public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
        public GameObject explosionParticleEffect;
        //public Vector3 impactRotation;
        [Tooltip("Audio")]
        public AudioSource m_ExplosionAudio;
        public AudioClip shellExplosionClip;
        [Tooltip("Damage properties")]
        public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
        public float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
        //public float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.
        [Tooltip("Area of damage. Keep empty if you don<t want area damage")]
        public DamageArea areaOfDamage;
        [Tooltip("Color of the projectile radius debug view")]
        public Color radiusColor = Color.magenta * 0.2f;

        [Tooltip("Lifetime")]
        public float m_MaxLifeTime = 5f;                    // The time in seconds before the shell is removed.

        
        private Health health;

        void OnEnable()
        {
            Hide(gameObject, m_MaxLifeTime);
        }


        private void Start()
        {
            // If it isn't destroyed by then, destroy the shell after it's lifetime.
            //Destroy(gameObject, m_MaxLifeTime);
            m_ExplosionAudio = gameObject.GetComponent<AudioSource>();
            m_ExplosionAudio.clip = shellExplosionClip;
        }
        private void Hide(GameObject objectToBeHid, float maxLifeTime)
        {
            if (maxLifeTime > 0)
            {
                maxLifeTime -= Time.deltaTime;
            }
            objectToBeHid.gameObject.SetActive(false);
        }

        /*private void OnCollisionEnter(Collision collision)
        {
            m_ExplosionAudio.Play();
        }*/
            private void OnTriggerEnter(Collider other)
        {

          
            Debug.Log("Shell collided with: " + other.name);
            // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            // Go through all the colliders...
            for (int i = 0; i < colliders.Length; i++)
            {
                // ... and find their rigidbody.
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                // If they don't have a rigidbody, go on to the next collider.
                if (!targetRigidbody)
                    continue;

                // Add an explosion force.
                targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

                // Find the TankHealth script associated with the rigidbody.
                //PlayerStats targetHealth = targetRigidbody.GetComponentInChildren<PlayerStats>();
                playerStats = other.GetComponentInChildren<PlayerStats>();
                if (playerStats == null) Debug.Log("PlayerStats script not found");

                // If there is no TankHealth script attached to the gameobject, go on to the next collider.
                if (!playerStats)
                    continue;

                // Calculate the amount of damage the target should take based on it's distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);

                // Deal this damage to the tank.
                playerStats.DamageTaken(damage);
            }

            // Unparent the particles from the shell.
            //m_ExplosionParticles.transform.parent = null;  //could be ueful designing sabot -> unparent from main shell particle
            //explosionParticleEffect = Instantiate(m_ExplosionParticles) as GameObject;
           
            //explosionParticleEffect.transform.parent = null;
            explosionParticleEffect = Instantiate(explosionParticleEffect, transform.position, this.transform.rotation) as GameObject;

            //m_ExplosionAudio.clip = shellExplosionClip;
            //m_ExplosionAudio.Play();
            //Destroy(m_ExplosionAudio, m_MaxLifeTime);
            //m_ExplosionAudio.transform.parent = null;
           


            // Once the particles have finished, destroy the gameobject they are on.
            //ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
            //Destroy(m_ExplosionParticles.gameObject, mainModule.duration);
            
            //Destroy(explosionParticleEffect, m_MaxLifeTime);  //need to change this to kind of corouitne maybe using FX manager : ObjectsLake?


            // Destroy the shell.
            //Destroy(gameObject);
            //NOT anymore, we will pool it hence
            this.gameObject.SetActive(false);
        }


        private float CalculateDamage(Vector3 targetPosition)
        {
            // Create a vector from the shell to the target.
            Vector3 explosionToTarget = targetPosition - transform.position;

            // Calculate the distance from the shell to the target.
            float explosionDistance = explosionToTarget.magnitude;

            // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            // Calculate damage as this proportion of the maximum possible damage.
            float damage = relativeDistance * m_MaxDamage;

            // Make sure that the minimum damage is always 0.
            damage = Mathf.Max(0f, damage);

            return damage;
        }

    }
}

