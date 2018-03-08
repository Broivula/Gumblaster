using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour {

    public float speed = 10;
    private int x, y;
    private StateChecker sChecker;

    private void Start()
    {
        //  StartCoroutine(MoveTowardsThis(0));
        sChecker = gameObject.GetComponent<StateChecker>();
    }

    /*
    public void MoveDownwards ()
    {
        x = gameObject.GetComponent<LocationHolder>().getX();
        y = gameObject.GetComponent<LocationHolder>().getY();

      


        List<GameObject> list = sChecker.GetListOfObjects();
        
        foreach(GameObject gum in list)
        {
            int targetY = gum.GetComponent<LocationHolder>().getY();
            int targetX = gum.GetComponent<LocationHolder>().getX();
            if (targetY > -1 && targetX == x)
            {

            }
        }

    }
    */
    public IEnumerator MoveTowardsThis (int direction)
    {
        Vector3 moveHere;
        

        switch(direction)
        {
            case 0:         //alas
                moveHere = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - 1 , gameObject.transform.position.z);
                while(transform.position != moveHere)
                {
                    transform.position = Vector3.MoveTowards(transform.position, moveHere, speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
               
                break;

            case 1:         //ylös
                moveHere = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
                while (transform.position != moveHere)
                {
                    transform.position = Vector3.MoveTowards(transform.position, moveHere, speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }

                break;

            case 2:         //vasen
                moveHere = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z);
                while (transform.position != moveHere)
                {
                    transform.position = Vector3.MoveTowards(transform.position, moveHere, speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }

                break;

            case 3:         //oikea
                moveHere = new Vector3(gameObject.transform.position.x +1, gameObject.transform.position.y, gameObject.transform.position.z);
                while (transform.position != moveHere)
                {
                    transform.position = Vector3.MoveTowards(transform.position, moveHere, speed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }

                break;

        }

        yield return null;
    }

    
}
