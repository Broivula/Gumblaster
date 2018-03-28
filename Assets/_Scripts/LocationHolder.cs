using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationHolder : MonoBehaviour {


    public int xCoord, yCoord, value;

    private Animator myAnim;

    public void Awake()
    {
        if (gameObject.tag == "Blue")
            value = 1;
        else if (gameObject.tag == "Red")
            value = 0;
        else if (gameObject.tag == "Green")
            value = 2;
        else if (gameObject.tag == "Pink")
            value = 3;
        else if (gameObject.tag == "Yellow")
            value = 4;

        myAnim = gameObject.GetComponent<Animator>();
    }
    /*
    public void DestroyAnim ()
    {
        Debug.Log("RAJAAH");
        myAnim.SetTrigger("destroyed");
        if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Ball_pop")) 
        {
            //Avoid any reload.
            Destroy(gameObject);
        }
    }
    */
    public void SetX (int x)
    {
        this.xCoord = x;
    }

    public void SetY(int y)
    {
        this.yCoord = y;
    }

    public int getY ()
    {
        return this.yCoord;
    }

    public int getX ()
    {
        return this.xCoord;
    }

    public int getValue()
    {
        return this.value;
    }
}
