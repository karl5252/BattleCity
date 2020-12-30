using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleCity
{
    public class Movement : MonoBehaviour
    {

        

        //public float playerSpeed = 4f;
       // public float playerTurningSpeed = 80f;

        public AudioSource playerMovementAudio;
        public AudioClip playerEngineDown;
        public AudioClip playerEngineUp;
        public float enginePitchVary = 0.5f;


        private new Rigidbody rigidbody;              // Reference used to move the tank.
       
                    // The pitch of the audio source at the start of the scene.
 


        public GameObject[] rightSideWwheels;
        public GameObject[] leftSideWwheels;

        public GameObject leftTankTrack;
        public GameObject rightTankTrack;
        Renderer leftTrackRenderer;
        Renderer rightTrackRenderer;
               

        public void EngineAudio(float originalPitch, float movementInputValue, float turnInputValue)
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





        public void Move(float movementInputValue, float playerSpeed)
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * movementInputValue * playerSpeed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            rigidbody.MovePosition(rigidbody.position + movement);
        }


        public void Turn(float turnInputValue, float playerTurningSpeed)
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = turnInputValue * playerTurningSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
        }

        public void AnimateWheels(float playerSpeed, float playerTurningSpeed , float movementInputValue, float turnInputValue)
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
                    leftSideWwheels[i].transform.Rotate(-playerTurningSpeed, 0.0f, 0.0f, Space.Self);
                }
            }
            else if (turnInputValue < 0)
            {
                for (int i = 0; i < rightSideWwheels.Length; ++i)
                {
                    rightSideWwheels[i].transform.Rotate(-playerTurningSpeed, 0.0f, 0.0f, Space.Self);
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
                    leftSideWwheels[i].transform.Rotate(-playerSpeed, 0.0f, 0.0f, Space.Self);
                }
            }
        }

        public void AnimateTracks(float playerSpeed, float movementInputValue, float turnInputValue)
        {
            float offset = Time.time * playerSpeed;
            //turn left
            if (turnInputValue > 0)
            {
                rightTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(-offset, 0);
            }
            else if (turnInputValue < 0)
            {
                rightTrackRenderer.material.mainTextureOffset = new Vector2(-offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);
            }
            if (movementInputValue > 0)
            {


                //leftTrackRenderer.material.SetTextureOffset(leftTrackName, new Vector2(offset, 0));
                //rightTrackRenderer.material.SetTextureOffset(rightTrackName, new Vector2(offset, 0));
                rightTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(offset, 0);

            }
            else if (movementInputValue < 0)
            {
                //leftTrackRenderer.material.SetTextureOffset(leftTrackName, new Vector2(offset, 0));
                //rightTrackRenderer.material.SetTextureOffset(rightTrackName, new Vector2(offset, 0));
                rightTrackRenderer.material.mainTextureOffset = new Vector2(-offset, 0);
                leftTrackRenderer.material.mainTextureOffset = new Vector2(-offset, 0);

            }

        }

    }
}