using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationHolder : MonoBehaviour {


    public int xCoord, yCoord, value;

    public void Start()
    {
        if (gameObject.tag == "Blue")
            value = 1;
        else if (gameObject.tag == "Red")
            value = 2;
        else if (gameObject.tag == "Green")
            value = 3;
        else if (gameObject.tag == "Pink")
            value = 4;
        else if (gameObject.tag == "Yellow")
            value = 5;
    }

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
