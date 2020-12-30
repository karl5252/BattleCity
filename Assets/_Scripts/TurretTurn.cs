using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleCity
{
    public class TurretTurn : MonoBehaviour
    {

        public int playerID;
        public float turretTurningSpeed = 2f;

        //bool left;
        //bool right;
        bool rotate;
        private string turnAxisName;              
        private string TurretLockTriggerButton;
        private float turnInputValue;

        private Vector3 initialRotation;

        private void RotationAllowed()
        {
            if (Input.GetButtonDown(TurretLockTriggerButton))
            {
                Debug.Log("Button down!");
                rotate = true;

            }
            if (Input.GetButtonUp(TurretLockTriggerButton))
            {
                Debug.Log("Button up!");
                rotate = false;
  
            }
            

        }
        private void ResetRotation()
        {
            Debug.Log("current rotation y " + this.transform.localRotation.eulerAngles.y);
            //Debug.Log("current rotation x " + this.transform.localRotation.eulerAngles.x);
            //Debug.Log("current rotation z " + this.transform.localRotation.eulerAngles.z);

            if (this.transform.localRotation.eulerAngles.y == initialRotation.y /*|| this.transform.localRotation.eulerAngles.y == 360*/)
            {
                return;
            }
            else
            {
                transform.Rotate(0, turretTurningSpeed, 0);
            }
            /*do
            {
                float turn = turnInputValue * turretTurningSpeed * Time.deltaTime;
                Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
                transform.Rotate(0, turretTurningSpeed, 0);

            } while (this.transform.localRotation.eulerAngles.y != 0.0f);*/
        }




        /*public void RotateRight()
        {
            right = true;
        }*/
        private void Start()
        {
            turnAxisName = "Horizontal" + playerID;
            TurretLockTriggerButton = "lockTurret_" + playerID;
            Debug.Log("TurretLockTriggerButton " + playerID);
            initialRotation = this.transform.localRotation.eulerAngles; //this.transform.rotation;
            Debug.Log("initial rotation y " + initialRotation.y);
            //rotate = false;

        }
        private void FixedUpdate()
        {
            //Debug.Log("rotate turret?: " + rotate);
            RotationAllowed();
            if (rotate == true)
            {
                turnInputValue = Input.GetAxis(turnAxisName);
                transform.Rotate(0, turretTurningSpeed * turnInputValue, 0);
            }
            if (rotate == false)
            {
                ResetRotation();
            }


        }










            /* public int playerID;
             public bool isRotatable = true;
             private bool rotationEnabled = false;
             public float turretTurningSpeed = 80f;
             private float turnInputValue;             // The current value of the turn input.
             private Quaternion initialRotation;
             private string turnAxisName;
             private string TurretLockTriggerButton;


             //public PlayerMovement playerMovement;


             private void Awake()
             {
                 //playerMovement = GetComponent<PlayerMovement>();
                 //
                 turnAxisName = "Horizontal" + playerID;
                 TurretLockTriggerButton = "lockTurret_" + playerID;
                 initialRotation = this.transform.rotation;
             }
             private void FixedUpdate()
             {

                 //transform.localRotation = Quaternion.identity;
                 if (Input.GetButtonDown(TurretLockTriggerButton))
                 {
                     rotationEnabled = true;
                 }
                 if (Input.GetButtonUp(TurretLockTriggerButton))
                 {
                     this.transform.rotation = initialRotation;
                     rotationEnabled = false;
                 }
                 TurnTurret();

             }
             void OnDrawGizmos()
             {
                 // Draws a 5 unit long red line in front of the object
                 Gizmos.color = Color.red;
                 Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
                 Gizmos.DrawRay(transform.position, direction);
             }
             private void TurnTurret()
             {




                 do
                 {
                     float turn = turnInputValue * turretTurningSpeed * Time.deltaTime;
                     Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
                     transform.Rotate(0, turretTurningSpeed, 0);

                 } while ( isRotatable == true && rotationEnabled == true);




             }*/
        }
}

