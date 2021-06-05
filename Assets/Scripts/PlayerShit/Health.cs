using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image heart;

    public Sprite[] sprites;

    public PlayerStats playerStats;

    float currentHealth = 0f;
    float previousHealth = 0f;

    public AudioSource getHealth;
    public AudioSource getHurt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heart.sprite = sprites[(int)playerStats.health - 1];

        currentHealth = playerStats.health;

        if (currentHealth > previousHealth)
        {
            getHealth.Play();
        }

        if (currentHealth < previousHealth)
        {
            getHurt.Play();
        }

        previousHealth = currentHealth;
    }
}
