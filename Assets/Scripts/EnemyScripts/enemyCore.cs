using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AIPath))]

public class enemyCore : MonoBehaviour
{
    // just in this script, if you want a variable to show up in inspect you have to:
    // 1. set it as public
    // 2. add it to the string list below

    public static string[] AddToInspector = new string[] 
    {
        "health",
        "SP",
        "directionalBodies",
        "IsAgro"
    };


    public float health;

    public SpriteRenderer SP;

    public Sprite[] directionalBodies;
    public bool hasAgroSprites;
    public Sprite[] agroDirectionalBodies;


    private Animator anim;
    private AIPath aiPath;


    public bool IsAgro;


    private void Start()
    {
        anim = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        if (aiPath.velocity != Vector3.zero)    //When AI is Moving
        {
            anim.SetBool("Walking", true);
        }
        else                                    //Whenn AI is Still              
        {
            anim.SetBool("Walking", false);
        }

        int x = Mathf.RoundToInt(aiPath.velocity.normalized.x);
        int y = Mathf.RoundToInt(aiPath.velocity.normalized.y);

        switch(new Vector2(x, y))
        {
            case Vector2 v when v.Equals(new Vector2(0,1)):     //U
                ChangeSprite(0);
                break;

            case Vector2 v when v.Equals(new Vector2(1, 1)):    //UR
                ChangeSprite(1);
                break;

            case Vector2 v when v.Equals(new Vector2(1, 0)):    //R
                ChangeSprite(2);
                break;

            case Vector2 v when v.Equals(new Vector2(1, -1)):   //DR
                ChangeSprite(3);
                break;

            case Vector2 v when v.Equals(new Vector2(0, -1)):   //D
                ChangeSprite(4);
                break;

            case Vector2 v when v.Equals(new Vector2(-1, -1)):  //DL
                ChangeSprite(5);
                break;

            case Vector2 v when v.Equals(new Vector2(-1, 0)):   //L
                ChangeSprite(6);
                break;

            case Vector2 v when v.Equals(new Vector2(-1, 1)):   //UL
                ChangeSprite(7);
                break;
        }
    }

    private void ChangeSprite(int direction)
    {
        Sprite normalSprite = directionalBodies[direction];
        Sprite agroSrite;

        if (IsAgro) {
            if (hasAgroSprites)
            {
                agroSrite = agroDirectionalBodies[direction];
                SP.sprite = agroSrite;
            }
            else
            {
                SP.sprite = normalSprite;
            }
        }
        else
        {
           
            SP.sprite = normalSprite;
            print($"setting sprite to {normalSprite.name}");
        }
    }
}
