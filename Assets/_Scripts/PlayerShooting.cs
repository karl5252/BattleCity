using UnityEngine;

namespace BattleCity
{
    public class PlayerShooting : MonoBehaviour
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

        private string fire1TriggerButton;
        private string fire2TriggerButton;
        private float reloadTimeFire1;
        public float reloadOffset = 3f;
        private float reloadTimeFire2;

        private bool fired1;
        private bool fired2;


        private void Start()
        {
            fire1TriggerButton = "Fire1_" + playerID;
            fire2TriggerButton = "Fire2_" + playerID;
        }
        private void Update()
        {
            if (Input.GetButtonUp(fire1TriggerButton) && !fired1 && Time.time >= reloadTimeFire1)
            {
                // ... launch the shell.
                Fire1();
                reloadTimeFire1 = Time.time + reloadOffset;
                fired1 = false;
            }
        }



        private void Fire1()
        {
            // Set the fired flag so only Fire is only called once.
            fired1 = true;
            shootingAudio.clip = playerFireClip;
            shootingAudio.Play();

            // Create an instance of the shell and store a reference to it's rigidbody.
            /*Rigidbody shellInstance =
                Instantiate(shellPrefab, barrelBore.position, barrelBore.rotation) as Rigidbody;*/

          

            //shootingAudio.clip = playerReloadClip;
            //Instantiate(playerBullet, turret.transform.position, turret.transform.rotation);

         GameObject shell = ObjectsLake.SharedInstance.GetPooledObject("tankShell");
            if (shell != null)
            {
                shell.transform.position = barrelBore.transform.position;
                shell.transform.rotation = barrelBore.transform.rotation;
                shell.GetComponent<Rigidbody>().velocity = launchForce * barrelBore.forward;
                shell.SetActive(true);

            }


  // Set the shell's velocity to the launch force in the fire position's forward direction.
            //shellInstance.velocity = launchForce * barrelBore.forward;
            //barrelSmoke = Instantiate(barrelSmoke, barrelBore.position, barrelBore.rotation) as GameObject;


        }
        private void Fire2()
        {
            // Set the fired flag so only Fire is only called once.
            fired2 = true;

        }
    }
}