using UnityEngine;
namespace BattleCity
{
    public class PlayerMovement : MonoBehaviour
    {
        //make it inherit from MAIN MOVMENT CLASS and this as child along with enemy movement as child class as well

        public int playerID;

        public float playerSpeed = 4f;
        public float playerTurningSpeed = 80f;

        public AudioSource playerMovementAudio;
        public AudioClip playerEngineDown;
        public AudioClip playerEngineUp;
        public float enginePitchVary = 0.5f;

        private string movementAxisName;          // The name of the input axis for moving forward and back.
        private string turnAxisName;              // The name of the input axis for turning.
        private new Rigidbody rigidbody;              // Reference used to move the tank.
        private float movementInputValue;         // The current value of the movement input.
        private float turnInputValue;             // The current value of the turn input.
        private float originalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks


        public GameObject[] rightSideWwheels;
        public GameObject[] leftSideWwheels;
       
        public GameObject leftTankTrack;
        public GameObject rightTankTrack;
        Renderer leftTrackRenderer;
        Renderer rightTrackRenderer;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }


        private void OnEnable()
        {
            // When the tank is turned on, make sure it's not kinematic.
            rigidbody.isKinematic = false;

            // Also reset the input values.
            movementInputValue = 0f;
            turnInputValue = 0f;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Play();
            }
        }


        private void OnDisable()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
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

            EngineAudio();
        }


        private void EngineAudio()
        {
            // If there is no input (the tank is stationary)...
            if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
            {
                // ... and if the audio source is currently playing the driving clip...
                if (playerMovementAudio.clip == playerEngineUp)
                {
                    // ... change the clip to idling and play it.
                    playerMovementAudio.clip = playerEngineDown;
                    playerMovementAudio.pitch = Random.Range(originalPitch - enginePitchVary, originalPitch + enginePitchVary);
                    playerMovementAudio.Play();
                }
            }
            else
            {
                // Otherwise if the tank is moving and if the idling clip is currently playing...
                if (playerMovementAudio.clip == playerEngineDown)
                {
                    // ... change the clip to driving and play.
                    playerMovementAudio.clip = playerEngineUp;
                    playerMovementAudio.pitch = Random.Range(originalPitch - enginePitchVary, originalPitch + enginePitchVary);
                    playerMovementAudio.Play();
                }
            }
        }


        private void FixedUpdate()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move();
            Turn();
            AnimateWheels();
            AnimateTracks();
        }


        private void Move()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * movementInputValue * playerSpeed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            rigidbody.MovePosition(rigidbody.position + movement);
        }


        private void Turn()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = turnInputValue * playerTurningSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
        }
        public void AnimateWheels()
        {
            //forwards
            //backcwards
            //turn right
            //turn left
            if (turnInputValue > 0)
            {
                for (int i = 0; i < rightSideWwheels.Length; ++i)
                {
                    rightSideWwheels[i].transform.Rotate(playerTurningSpeed, 0.0f, 0.0f, Space.Self);
                    leftSideWwheels[i].transform.Rotate(- playerTurningSpeed, 0.0f, 0.0f, Space.Self);
                }
            }
            else if (turnInputValue < 0)
            {
                for (int i = 0; i < rightSideWwheels.Length; ++i)
                {
                    rightSideWwheels[i].transform.Rotate(- playerTurningSpeed, 0.0f, 0.0f, Space.Self);
                    leftSideWwheels[i].transform.Rotate(playerTurningSpeed, 0.0f, 0.0f, Space.Self);
                }
            }

            if (movementInputValue > 0)
            {
                for (int i = 0; i < rightSideWwheels.Length; ++i)
                {
                    rightSideWwheels[i].transform.Rotate(-playerSpeed, 0.0f, 0.0f, Space.Self);
                    leftSideWwheels[i].transform.Rotate(playerSpeed, 0.0f, 0.0f, Space.Self);
                }
            }
            else if (movementInputValue < 0)
            {
                for (int i = 0; i < rightSideWwheels.Length; ++i)
                {
                    rightSideWwheels[i].transform.Rotate(playerSpeed, 0.0f, 0.0f, Space.Self);
                    leftSideWwheels[i].transform.Rotate(- playerSpeed, 0.0f, 0.0f, Space.Self);
                }
            }
        }

            public void AnimateTracks()
            {
            float offset = Time.time * playerSpeed;
            //turn left
            if (turnInputValue > 0)
            {
                rightTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(- offset, 0);
            }
            else if (turnInputValue < 0)
            {
                rightTrackRenderer.material.mainTextureOffset = new Vector2(- offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);
            }
            if (movementInputValue > 0)
            {


                //leftTrackRenderer.material.SetTextureOffset(leftTrackName, new Vector2(offset, 0));
                //rightTrackRenderer.material.SetTextureOffset(rightTrackName, new Vector2(offset, 0));
                rightTrackRenderer.material.mainTextureOffset= new Vector2(offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);

            }
            else if (movementInputValue < 0)
            {
                //leftTrackRenderer.material.SetTextureOffset(leftTrackName, new Vector2(offset, 0));
                //rightTrackRenderer.material.SetTextureOffset(rightTrackName, new Vector2(offset, 0));
                rightTrackRenderer.material.mainTextureOffset = new Vector2(- offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(- offset, 0);

            }

        }
        
    }
}