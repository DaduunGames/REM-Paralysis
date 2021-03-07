using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator Anim;
    Rigidbody2D RB;

    public float WalkSpeed = 1;

    public KeyCode[] NorthKeys;
    public KeyCode[] EastKeys;
    public KeyCode[] SouthKeys;
    public KeyCode[] WestKeys;

    void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        #region: North
        if (
            KCheck(NorthKeys) && 
            !KCheck(EastKeys) && 
            !KCheck(SouthKeys) && 
            !KCheck(WestKeys)
           )
        {
            SetAnimDirection(0);
            SetVelocity(0, 1);
        }
        #endregion
        #region: North East
        else if (
            KCheck(NorthKeys) &&
            KCheck(EastKeys) &&
            !KCheck(SouthKeys) &&
            !KCheck(WestKeys)
           )
        {
            SetAnimDirection(1);
            SetVelocity(1, 1);
        }
        #endregion
        #region: East
        else if (
            !KCheck(NorthKeys) &&
            KCheck(EastKeys) &&
            !KCheck(SouthKeys) &&
            !KCheck(WestKeys)
           )
        {
            SetAnimDirection(2);
            SetVelocity(1, 0);
        }
        #endregion
        #region: South East
        else if (
            !KCheck(NorthKeys) &&
            KCheck(EastKeys) &&
            KCheck(SouthKeys) &&
            !KCheck(WestKeys)
           )
        {
            SetAnimDirection(3);
            SetVelocity(1, -1);
        }
        #endregion
        #region: South
        else if (
            !KCheck(NorthKeys) &&
            !KCheck(EastKeys) &&
            KCheck(SouthKeys) &&
            !KCheck(WestKeys)
           )
        {
            SetAnimDirection(4);
            SetVelocity(0, -1);
        }
        #endregion
        #region: South West
        else if (
            !KCheck(NorthKeys) &&
            !KCheck(EastKeys) &&
            KCheck(SouthKeys) &&
            KCheck(WestKeys)
           )
        {
            SetAnimDirection(5);
            SetVelocity(-1, -1);
        }
        #endregion
        #region: West
        else if (
            !KCheck(NorthKeys) &&
            !KCheck(EastKeys) &&
            !KCheck(SouthKeys) &&
            KCheck(WestKeys)
           )
        {
            SetAnimDirection(6);
            SetVelocity(-1, 0);
        }
        #endregion
        #region: North West
        else if (
            KCheck(NorthKeys) &&
            !KCheck(EastKeys) &&
            !KCheck(SouthKeys) &&
            KCheck(WestKeys)
           )
        {
            SetAnimDirection(7);
            SetVelocity(-1, 1);
        }
        #endregion
        #region: Not Moving
        else
        {
            SetVelocity(0, 0);
        }
        #endregion
    }

    private void SetAnimDirection(int direction)
    {
        Anim.SetInteger("Direction", direction);
    }
 
    private bool KCheck(KeyCode[] keys)
    {
        foreach(KeyCode key in keys)
        {
            if (Input.GetKey(key))
            {
                return true;
            } 
        }
        return false;
    }

    private void SetVelocity(int x, int y)
    {
        RB.velocity = new Vector2(x, y).normalized * WalkSpeed;
    }


}
