using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenControls : MonoBehaviour {

    private bool moveBool = false;
    GameObject targetedObject;
    List<GameObject> gums;
    private GridManager gManager;
    private StateChecker sChecker;

    int moveX, moveY;
  
    void Start ()
    {
	    gManager = GameObject.Find("Game_manager").GetComponent<GridManager>();
        sChecker = GameObject.Find("Game_manager").GetComponent<StateChecker>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //jos painaa ruutua, ammu hiiren kohdalta raycast 
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                print("Osui!!!");                           //jos osui johonkin
                targetedObject = hit.transform.gameObject;
                moveX = hit.transform.gameObject.GetComponent<LocationHolder>().xCoord;
                moveY = hit.transform.gameObject.GetComponent<LocationHolder>().yCoord;
                moveBool = true;

            }

        }
      

        Move();
		
	}

    private void Move()
    {


        if (Input.GetAxis("Mouse X") > 0f && moveBool && targetedObject && moveX < 9)
        {
            //liikkui oikealle
            //jos liikuttaa oikealle, pitää lähettää oikealle viesti että liikuta yksi vasemmalle
            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(3));
            FindGum(3);
            moveBool = false;
        }

        else if (Input.GetAxis("Mouse X") < 0f && moveBool && targetedObject && moveX > 0)
        {
            //liikkui vasemmalle

            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(2));
            FindGum(2);
            moveBool = false;
        }

        else if (Input.GetAxis("Mouse Y") > 0f && moveBool && targetedObject && moveY < 9)
        {
            //liikkui ylös

            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(1));
            FindGum(1);
            moveBool = false;
        }

        else if (Input.GetAxis("Mouse Y") < 0f && moveBool && targetedObject && moveY > 0)
        {
            //liikkui alas

            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(0));
            FindGum(0);
            moveBool = false;
        }

    }

    private void FindGum (int i)
    {
        bool listBool = GetList();
       // int targetY, targetX;

        switch(i)
        {
            case 0:
                //liikutti alas
                foreach(GameObject gum in gums)
                {
                    if(gum.GetComponent<LocationHolder>().yCoord - moveY == -1 && gum.GetComponent<LocationHolder>().xCoord == moveX)
                    {
                        StartCoroutine(gum.GetComponent<MovingScript>().MoveTowardsThis(1));
                        gum.GetComponent<LocationHolder>().SetY(moveY);                                 //päivitetään pallon koordinaattitiedot
                        targetedObject.gameObject.GetComponent<LocationHolder>().SetY(moveY - 1);       //päivitetään toisen pallon koordinaattitiedot
                        moveY = 0;
                        moveX = 0;

                       // CheckState();
                    }
                }
                break;

            case 1:
                //liikutti ylös
                foreach (GameObject gum in gums)
                {
                    if (gum.GetComponent<LocationHolder>().yCoord - moveY == 1 && gum.GetComponent<LocationHolder>().xCoord == moveX)
                    {
                        StartCoroutine(gum.GetComponent<MovingScript>().MoveTowardsThis(0));
                        gum.GetComponent<LocationHolder>().SetY(moveY);                                 //päivitetään pallon koordinaattitiedot
                        targetedObject.gameObject.GetComponent<LocationHolder>().SetY(moveY + 1);       //päivitetään toisen pallon koordinaattitiedot
                        moveY = 0;
                        moveX = 0;

                    //    CheckState();
                    }
                }
                break;

            case 2:
                //liikutti vasemmalle
                foreach (GameObject gum in gums)
                {
                    if (gum.GetComponent<LocationHolder>().xCoord - moveX == -1 && gum.GetComponent<LocationHolder>().yCoord == moveY)
                    {
                        StartCoroutine(gum.GetComponent<MovingScript>().MoveTowardsThis(3));
                        gum.GetComponent<LocationHolder>().SetX(moveX);                                 //päivitetään pallon koordinaattitiedot
                        targetedObject.gameObject.GetComponent<LocationHolder>().SetX(moveX - 1);       //päivitetään toisen pallon koordinaattitiedot
                        moveY = 0;
                        moveX = 0;

                      //  CheckState();
                    }
                }
                break;

            case 3:
                //liikutti oikealle
                foreach (GameObject gum in gums)
                {
                    if (gum.GetComponent<LocationHolder>().xCoord - moveX == 1 && gum.GetComponent<LocationHolder>().yCoord == moveY)
                    {
                        StartCoroutine(gum.GetComponent<MovingScript>().MoveTowardsThis(2));
                        gum.GetComponent<LocationHolder>().SetX(moveX);                                 //päivitetään pallon koordinaattitiedot
                        targetedObject.gameObject.GetComponent<LocationHolder>().SetX(moveX + 1);       //päivitetään toisen pallon koordinaattitiedot
                        moveY = 0;
                        moveX = 0;

                      //  CheckState();
                    }
                }
                break;


        }
    }

    private void CheckState ()
    {
        sChecker.CheckState();
    }

    public bool GetList()
    {
        gums = gManager.gums;
        return true;
    }
}
