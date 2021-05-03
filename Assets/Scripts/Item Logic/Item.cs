using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string PrefabName;
    
    public string Title;
    public string Lore;

    #region stats
    public float health = 0;
    public float maxHealth = 0;
    public int money = 0;
    public float movementSpeedModifier = 0;
    public float dashDistanceModifier = 0;
    public float dashCooldownModifier = 0;

    public float bulletDamageModifier = 0;
    public float bulletSizeModifier = 0;
    public float attackSpeedModifier = 0;

    public GameObject AOE;
    public PlayerStats.BulletType bulletType = PlayerStats.BulletType.None;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
