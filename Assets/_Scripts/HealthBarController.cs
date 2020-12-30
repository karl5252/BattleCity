using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCity
{
    public class HealthBarController : MonoBehaviour
    {
        private PlayerStats playerStats;
        public Slider healthBarSlider;
        public Image image;
        public Color fullHealth = Color.green;
        //public Color midHealth = Color.yellow;
        public Color noneHealth = Color.yellow;

     
        // Start is called before the first frame update
        void Start()
        {
            playerStats = this.GetComponent<PlayerStats>();

        }

        // Update is called once per frame
        void Update()
        {
            float health = playerStats.GetHealth();
            healthBarSlider.value = health;
            image.color = Color.Lerp(noneHealth, fullHealth, health / 100f);


        }

        
    }
}

