using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponHandler : MonoBehaviour
{
    int totalWeapons = 1;
    public int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;

    public AudioSource eAudio;
    public AudioSource qAudio;

    public Animator animator;

    public float SwitchDelay = 3;
    float SwitchDelayTimer;
    public Image radialDelayImage;
    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        radialDelayImage.fillAmount = SwitchDelayTimer/SwitchDelay;

        if (SwitchDelayTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //next Weapon
                if (currentWeaponIndex < totalWeapons - 1)
                {
                    guns[currentWeaponIndex].SetActive(false);
                    currentWeaponIndex += 1;
                    guns[currentWeaponIndex].SetActive(true);
                    currentGun = guns[currentWeaponIndex];

                    animator.SetInteger("GunType", animator.GetInteger("GunType") + 1);
                    eAudio.Play();

                    SwitchDelayTimer = SwitchDelay;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //previous Weapon
                if (currentWeaponIndex > 0)
                {
                    guns[currentWeaponIndex].SetActive(false);
                    currentWeaponIndex -= 1;
                    guns[currentWeaponIndex].SetActive(true);

                    animator.SetInteger("GunType", animator.GetInteger("GunType") - 1);
                    qAudio.Play();

                    SwitchDelayTimer = SwitchDelay;
                }
            }
        }
        else
        {
            SwitchDelayTimer -= Time.deltaTime;
        }
    }
}
