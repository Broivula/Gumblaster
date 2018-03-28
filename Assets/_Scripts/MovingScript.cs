using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

    public float speed;
    private int x, y;
    private StateChecker sChecker;
    LocationHolder locHol;
    GridManager gridManag;
    List<GameObject> ownRow;
    public bool amIMoving = false;
    private int counter = 0;



    private void Awake()
    {
        //  StartCoroutine(MoveTowardsThis(0));
        sChecker = GameObject.Find("Game_manager").GetComponent<StateChecker>();
        locHol = gameObject.GetComponent<LocationHolder>();
        gridManag = GameObject.Find("Game_manager").GetComponent<GridManager>();
    }

    public void AddCounter ()
    {
        counter++;
        
    }
    /*
    public bool IsThereSomethingUnderMe(List<GameObject> list)
    {
        bool isThere = false;
        foreach(GameObject gum in list)
        {
            if (locHol.getY() - gum.GetComponent<LocationHolder>().getY() != 1)
            {
                //alla ei ole mitään, liiku
             
                isThere = true;
            }
            else
            {
                isThere = false;
            }
                
        }

        return isThere;
    }
    
    */



    public IEnumerator MoveTowardsThis (int direction, int amount)
    {
        Vector3 moveHere;
        amIMoving = true;

     //   Debug.Log("gum " + gameObject + " liikkuuko");
        switch (direction)
        {
            case 0:         //alas
                if (locHol.getY() > 0)
                {
                    
                    moveHere = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - amount, gameObject.transform.position.z);
                    while (transform.position != moveHere)
                    {
                        if (gameObject.GetComponent<MovingScript>() == true)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, moveHere, (speed * Time.deltaTime));
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    locHol.SetY(locHol.getY() - amount);
                    amIMoving = false;

                            // print("kasky lahtee2");
                }
                break;
   

            case 1:         //ylös
                if(locHol.getY() < 9)
                {
                    moveHere = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + amount, gameObject.transform.position.z);
                    while (transform.position != moveHere)
                    {
                        if (gameObject.GetComponent<MovingScript>() == true)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, moveHere, (speed * Time.deltaTime));
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    locHol.SetY(locHol.getY() + 1);
                    amIMoving = false;
                }
      

                break;

            case 2:         //vasen
                if(locHol.getX() > 0)
                {
                    moveHere = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                    while (transform.position != moveHere)
                    {
                        if (gameObject.GetComponent<MovingScript>() == true)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, moveHere, (speed * Time.deltaTime));
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    locHol.SetX(locHol.getX() - 1);
                    amIMoving = false;

                }
                break;

            case 3:         //oikea
                if(locHol.getX() < 9)
                {
                    moveHere = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
                    
                    while (transform.position != moveHere)
                    {
                        if (gameObject.GetComponent<MovingScript>() == true)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, moveHere, (speed * Time.deltaTime));
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    locHol.SetX(locHol.getX() + 1);
                    amIMoving = false;



                }
                break;

            case 4:         //kun pallo tuhotaa, liikutetaan se ensin pois kentästä efektin vuoksi
                moveHere = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1);
                while(transform.position != moveHere)
                {
                    if (gameObject.GetComponent<MovingScript>() == true)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, moveHere, (speed * Time.deltaTime) * 2);
                        yield return new WaitForEndOfFrame();
                    }
                }
               
                break;

        }

        gridManag.ListSorter();

        
    }


    

    
}
