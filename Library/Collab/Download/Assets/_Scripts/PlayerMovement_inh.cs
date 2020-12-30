using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleCity
{
    public class PlayerMovement_inh : Movement
    {
        public int playerID;

        public float playerSpeed = 4f;
        public float playerTurningSpeed = 80f;

        //public AudioSource playerMovementAudio;
        //public AudioClip playerEngineDown;
       // public AudioClip playerEngineUp;
        //public new float enginePitchVary = 0.5f;

        private string movementAxisName;          // The name of the input axis for moving forward and back.
        private string turnAxisName;              // The name of the input axis for turning.
        private  Rigidbody thisRigidbody;              // Reference used to move the tank.
        private float movementInputValue;         // The current value of the movement input.
        private float turnInputValue;             // The current value of the turn input.
        private float originalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] particleSystems; // References to all the particles systems used by the Tanks


        //public GameObject[] rightSideWwheels;
        //public GameObject[] leftSideWwheels;

        public GameObject leftTankTrack;
        public GameObject rightTankTrack;
        Renderer leftTrackRenderer;
        Renderer rightTrackRenderer;


        private void Awake()
        {
            thisRigidbody = this.GetComponent<Rigidbody>();
        }


        private void OnEnable()
        {
            // When the tank is turned on, make sure it's not kinematic.
            thisRigidbody.isKinematic = false;

            // Also reset the input values.
            movementInputValue = 0f;
            turnInputValue = 0f;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particleSystems.Length; ++i)
            {
                particleSystems[i].Play();
            }
        }


        private void OnDisable()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            thisRigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for (int i = 0; i < particleSystems.Length; ++i)
            {
                particleSystems[i].Stop();
            }
        }


        private void Start()
        {
            // The axes names are based on player number.
            movementAxisName = "Vertical" + playerID;
            turnAxisName = "Horizontal" + playerID;
            leftTrackRenderer = leftTankTrack.GetComponent<Renderer>();
            rightTrackRenderer = rightTankTrack.GetComponent<Renderer>();

            // Store the original pitch of the audio source.
            originalPitch = playerMovementAudio.pitch;
        }


        private void Update()
        {
            // Store the value of both input axes.
            movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);

            EngineAudio( originalPitch, movementInputValue, turnInputValue);
        }


       
        


        private void FixedUpdate()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move(movementInputValue, playerSpeed);
            Turn(turnInputValue, playerTurningSpeed);
            AnimateWheels( playerSpeed,  playerTurningSpeed,  movementInputValue,  turnInputValue);
            AnimateTracks( playerSpeed, movementInputValue, turnInputValue, rightTrackRenderer, leftTrackRenderer);
        }


        


       

    }
}

