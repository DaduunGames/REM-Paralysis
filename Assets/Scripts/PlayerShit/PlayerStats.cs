using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    
    #region stats
    public float health = 3;
    public float maxHealth = 3;
    public int money = 0;
    public float movementSpeedModifier = 1 ;
    public float dashDistanceModifier = 1;
    public float dashCooldownModifier = 2;

    public float bulletDamageModifier = 1;
    public float bulletSizeModifier = 1;
    public float attackSpeedModifier = 1;

    public GameObject AOE;
    public BulletType bulletType;
    #endregion


    public int __________________________________________________;

    public List<GameObject> itemInventory;

    public float pickupDistance = 3;


    public GameObject Tooltip;
   
    public Text titleText;
    public Text loreText;
    public Text PositiveEffectText;
    public Text NegativeEffectText;

    string posText;
    string negText;


    

    private MyGameController myGameController;

    public enum BulletType
    {
        None,
        Regular,
        Charged,
        Shotgun
    }


    void Start()
    {
        if(!Tooltip.Equals(null)) Tooltip.SetActive(false);

        myGameController = FindObjectOfType<MyGameController>();
    }


    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {

            if (hit.transform.gameObject.tag == "Item")
            {
                Item item = hit.transform.GetComponent<Item>();
                if (!Tooltip.Equals(null))
                {
                    Tooltip.SetActive(true);
                    Tooltip.transform.position = Input.mousePosition + new Vector3(0, 20, 0);


                    titleText.text = item.Title;
                    loreText.text = item.Lore;

                    posText = "";
                    negText = "";
                    AppendEffectText(item.health > 0, item.health < 0, $"Heal: +{item.health}", $"Heal: {item.health}");
                    AppendEffectText(item.maxHealth > 0, item.maxHealth < 0, $"Max Health: +{item.maxHealth}", $"Max Health: {item.maxHealth}");
                    AppendEffectText(item.money > 0, item.money < 0, $"Money: +{item.money}", $"Money: {item.money}");
                    AppendEffectText(item.movementSpeedModifier > 0, item.movementSpeedModifier < 0, $"Move Speed: +{item.movementSpeedModifier * 100}%", $"Move Speed: {item.movementSpeedModifier * 100}%");
                    AppendEffectText(item.dashDistanceModifier > 0, item.dashDistanceModifier < 0, $"Dash Distance: +{item.dashDistanceModifier * 100}%", $"Dash Distance: {item.dashDistanceModifier * 100}%");
                    AppendEffectText(item.dashCooldownModifier < 0, item.dashCooldownModifier > 0, $"Dash Cooldown: {item.dashCooldownModifier * 100}%", $"Dash Cooldown: +{item.dashCooldownModifier * 100}%");
                    AppendEffectText(item.bulletDamageModifier > 0, item.bulletDamageModifier < 0, $"Feather Damage: +{item.bulletDamageModifier * 100}%", $"Feather Damage: {item.bulletDamageModifier * 100}%");
                    AppendEffectText(item.bulletSizeModifier > 0, item.bulletSizeModifier < 0, $"Feater Size: +{item.bulletSizeModifier * 100}%", $"Feather Size: {item.bulletSizeModifier * 100}%");
                    AppendEffectText(item.attackSpeedModifier > 0, item.attackSpeedModifier < 0, $"Attack Speed: +{item.attackSpeedModifier * 100}%", $"Attack Speed: {item.attackSpeedModifier * 100}%");
                    //AppendTipText(!item.AOE.Equals(null), false,$"AOE Effect: {item.AOE.name}\n", "");
                    AppendEffectText(item.bulletType != BulletType.None, false, $"Feather Type: {item.bulletType}", "");

                    if (posText != "")
                    {
                        PositiveEffectText.text = posText;
                        PositiveEffectText.enabled = true;
                    }
                    else
                    {
                        PositiveEffectText.enabled = false;
                    }


                    if (negText != "")
                    {
                        NegativeEffectText.text = negText;
                        NegativeEffectText.enabled = true;
                    }

                    else
                    {
                        NegativeEffectText.enabled = false;
                    }
                }
                else
                {
                    if (!Tooltip.Equals(null)) Tooltip.SetActive(false);

                }
            }
            else
            {
                if (!Tooltip.Equals(null)) Tooltip.SetActive(false);
            }
        }


        
    }


    private void AppendEffectText(bool Positive, bool Negative, string posString, string negString)
    {
        
        if (Positive) 
        {
            string prevStatePos = posText;
            posText = posText + posString;
            if (prevStatePos != "") posText = posText + "\n";
        }
        
        if (Negative)
        {
            string prevStateNeg = negText;
            negText = negText + negString;
            if (prevStateNeg != "") negText = negText + "\n";
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            //itemInventory.Add((GameObject)Resources.Load(col.name.ToString().Replace("(Clone)", "")));
            AddItem((GameObject)Resources.Load(col.GetComponent<Item>().PrefabName));
            
            Destroy(col.gameObject);
        }

        
    }

    public void AddItem(GameObject itemObj)
    {
        itemInventory.Add(itemObj);
        Item item = itemObj.GetComponent<Item>();

        //plusing a modifier
        maxHealth += item.maxHealth;
        health = Mathf.Clamp(health + item.health, 0 , maxHealth);
        money += item.money;
        movementSpeedModifier += item.movementSpeedModifier;
        dashDistanceModifier += item.dashDistanceModifier;
        dashCooldownModifier += item.dashCooldownModifier;
        bulletDamageModifier += item.bulletSizeModifier;
        bulletSizeModifier += item.bulletSizeModifier;
        //completely changing to item value
        AOE = item.AOE;
        bulletType = item.bulletType;

    }

    public void RemoveItem(GameObject itemObj)
    {
        itemInventory.Remove(itemObj);
        Item item = itemObj.GetComponent<Item>();

        //plusing a modifier
        health = Mathf.Clamp(health - item.health, 0, maxHealth);
        maxHealth -= item.maxHealth;
        money -= item.money;
        movementSpeedModifier -= item.movementSpeedModifier;
        dashDistanceModifier -= item.dashDistanceModifier;
        dashCooldownModifier -= item.dashCooldownModifier;
        bulletDamageModifier -= item.bulletSizeModifier;
        bulletSizeModifier -= item.bulletSizeModifier;

        //TODO: mor elogic needed for these cases
        //completely changing to item value
        //AOE = item.AOE;
        //bulletType = item.bulletType;
    }



}
