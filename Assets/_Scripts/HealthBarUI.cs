using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCity
{
    public class HealthBarUI : MonoBehaviour
    {
        public bool UseRelRotation = true;
        private Quaternion RelRotation;
        // Start is called before the first frame update
        void Start()
        {
            RelRotation = transform.parent.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (UseRelRotation)
            {
                transform.rotation = RelRotation;
            }
        }
    }
}

