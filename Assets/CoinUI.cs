using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public PlayerStats playerStats;
    Animator anim;
    int currentMoney;
    public Text text;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(currentMoney != playerStats.money)
        {
            currentMoney = playerStats.money;



            anim.SetTrigger("Change");
        }
    }

    public void ChangeCoinGUI()
    {
        text.text = currentMoney.ToString();
    }
}
