using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Sprite buttonPressedSprite;
    ParticleSystem buttonFeatherParticles;
    Image imageComponent;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options(bool ToOptions)
    {
        
        animator.SetBool("ToOptions", ToOptions);
    }

    public void spawnParticles(GameObject thisButton)
    {
        //thisButton.GetComponent<Image>().sprite = buttonPressedSprite;
        //thisButton.GetComponentInChildren<ParticleSystem>().Play();
    }



}
