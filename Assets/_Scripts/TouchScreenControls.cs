using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenControls : MonoBehaviour {

    private bool moveBool = false;
    private bool checkingBool = false;
    private GameObject targetedObject;
    private GameObject otherTargetedObject;
    private Vector3 originalPosition, otherOriginalPosition;
    List<GameObject> gums;
    private GridManager gManager;
    private StateChecker sChecker;
    private int otherX, otherY;
    private Vector3 firstTouch, lastTouch;
    public float speed;
    private float dragDist;
    int moveX, moveY;
  
    void Start ()
    {
	    gManager = GameObject.Find("Game_manager").GetComponent<GridManager>();
        sChecker = GameObject.Find("Game_manager").GetComponent<StateChecker>();

        dragDist = Screen.height * 5 / 100;
    }
	
	// Update is called once per frame
	void Update ()
    {


#if UNITY_ANDROID
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                firstTouch = touch.position;
                Ray ray = Camera.main.ScreenPointToRay(firstTouch);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {

                    print("Osui!!!");                           //jos osui johonkin
                    targetedObject = hit.transform.gameObject;
                    moveX = hit.transform.gameObject.GetComponent<LocationHolder>().xCoord;
                    moveY = hit.transform.gameObject.GetComponent<LocationHolder>().yCoord;
                    moveBool = true;
                    originalPosition = targetedObject.transform.position;



                }
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                lastTouch = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                lastTouch = touch.position;

                //katotaan paljonko sormea liikutettiin ja mihin suuntaan
                if (Mathf.Abs(lastTouch.x - firstTouch.x) > dragDist || Mathf.Abs(lastTouch.y - firstTouch.y) > dragDist)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lastTouch.x - firstTouch.x) > Mathf.Abs(lastTouch.y - firstTouch.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lastTouch.x > firstTouch.x)  && moveX < 5)  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            moveBool = false;
                            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(3, 1));
                            FindGum(3);
                            checkingBool = true;
                        }
                        else if (moveX > 0)
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            moveBool = false;
                            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(2, 1));
                            FindGum(2);
                            checkingBool = true;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lastTouch.y > firstTouch.y && moveY < 8)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            moveBool = false;
                            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(1, 1));
                            FindGum(1);
                            checkingBool = true;
                        }
                        else if (moveY > 0)
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            moveBool = false;
                            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            FindGum(0);
                            checkingBool = true;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");

                }

            }
        }
#endif

#if UNITY_EDITOR
      //    Move();
      
        if (Input.GetMouseButtonDown(0))
        {
            //jos painaa ruutua, ammu hiiren kohdalta raycast 
        //    firstTouch = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100) && !moveBool)
            {
          //      lastTouch = Input.mousePosition;
                
                print("Osui!!!");                           //jos osui johonkin
                targetedObject = hit.transform.gameObject;
                moveX = hit.transform.gameObject.GetComponent<LocationHolder>().xCoord;
                moveY = hit.transform.gameObject.GetComponent<LocationHolder>().yCoord;
                moveBool = true;
                originalPosition = targetedObject.transform.position;

             

            }

        }



#endif
    }

    private void Move()
    {


        if (Input.GetAxis("Mouse X") > 0f && moveBool && !checkingBool && targetedObject && moveX < 9)
        {
            //liikkui oikealle
            //jos liikuttaa oikealle, pitää lähettää oikealle viesti että liikuta yksi vasemmalle
            moveBool = false;
            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(3, 1));
            FindGum(3);
            checkingBool = true;
        }

        else if (Input.GetAxis("Mouse X") < 0f && moveBool && !checkingBool && targetedObject && moveX > 0)
        {
            //liikkui vasemmalle
            moveBool = false;
            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(2, 1));
            FindGum(2);
            checkingBool = true;
        }

        else if (Input.GetAxis("Mouse Y") > 0f && moveBool && !checkingBool &&  targetedObject && moveY < 9)
        {
            //liikkui ylös
            moveBool = false;
            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(1, 1));
            FindGum(1);
            checkingBool = true;
        }

        else if (Input.GetAxis("Mouse Y") < 0f && moveBool && !checkingBool &&  targetedObject && moveY > 0)
        {
            //liikkui alas
            moveBool = false;
            StartCoroutine(targetedObject.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
            FindGum(0);
            checkingBool = true;
        }
        else
        {
            //ei tehdä mitään
            return;
        }
       
       

    }

    private void FindGum (int i)
    {
        bool listBool = GetList();
      //  Debug.Log("gums " + gums.Count);
       // int targetY, targetX;

        switch(i)
        {
            case 0:
                //liikutti alas
                foreach(GameObject gum in gums)
                {
                    if(gum.GetComponent<LocationHolder>().yCoord - moveY == -1 && gum.GetComponent<LocationHolder>().xCoord == moveX)
                    {
                        MovingScript mScript = gum.GetComponent<MovingScript>();
                        otherTargetedObject = gum;
                        otherOriginalPosition = otherTargetedObject.transform.position;
                        otherX = otherTargetedObject.GetComponent<LocationHolder>().getX();
                        otherY = otherTargetedObject.GetComponent<LocationHolder>().getY();

                        StartCoroutine(mScript.MoveTowardsThis(1, 1));
                        StartCoroutine(IsAnythingMoving(mScript));
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
                        MovingScript mScript = gum.GetComponent<MovingScript>();
                        otherTargetedObject = gum;
                        otherOriginalPosition = otherTargetedObject.transform.position;
                        otherX = otherTargetedObject.GetComponent<LocationHolder>().getX();
                        otherY = otherTargetedObject.GetComponent<LocationHolder>().getY();

                        StartCoroutine(mScript.MoveTowardsThis(0, 1));
                        StartCoroutine(IsAnythingMoving(mScript));

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
                        MovingScript mScript = gum.GetComponent<MovingScript>();
                        otherTargetedObject = gum;
                        otherOriginalPosition = otherTargetedObject.transform.position;
                        otherX = otherTargetedObject.GetComponent<LocationHolder>().getX();
                        otherY = otherTargetedObject.GetComponent<LocationHolder>().getY();

                        StartCoroutine(mScript.MoveTowardsThis(3, 1));
                        StartCoroutine(IsAnythingMoving(mScript));

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
                        MovingScript mScript = gum.GetComponent<MovingScript>();
                        otherTargetedObject = gum;
                        otherOriginalPosition = otherTargetedObject.transform.position;
                        otherX = otherTargetedObject.GetComponent<LocationHolder>().getX();
                        otherY = otherTargetedObject.GetComponent<LocationHolder>().getY();

                        StartCoroutine(mScript.MoveTowardsThis(2, 1));
                        StartCoroutine(IsAnythingMoving(mScript));


                        //  CheckState();
                    }
                }
                break;


        }

      

       
        
    }

    private IEnumerator IsAnythingMoving (MovingScript mS)
    {
        while(mS.amIMoving)
        {
         //   Debug.Log(mS.amIMoving + "amimoving");
            if(!mS.amIMoving)
            {
              //  CheckState();
            }
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CheckState());
      //  Debug.Log("isanythingmoving : tehty");
    }

    private IEnumerator CheckState()
    {
        checkingBool = sChecker.CheckState();

        if (!checkingBool)
        {
            //jos tsekki oli valetta, palauta pallot
            while(targetedObject.transform.position != originalPosition)
            {
                targetedObject.transform.position = Vector3.MoveTowards(targetedObject.transform.position, originalPosition, (speed * Time.deltaTime));
                otherTargetedObject.transform.position = Vector3.MoveTowards(otherTargetedObject.transform.position, otherOriginalPosition, (speed * Time.deltaTime));


                yield return new WaitForEndOfFrame();
            }

            targetedObject.GetComponent<LocationHolder>().SetX(moveX);
            targetedObject.GetComponent<LocationHolder>().SetY(moveY);

            otherTargetedObject.GetComponent<LocationHolder>().SetX(otherX);
            otherTargetedObject.GetComponent<LocationHolder>().SetY(otherY);

        }

        checkingBool = false;
      
    }

    public bool GetList()
    {
        gums = gManager.gums;
        return true;
    }
}
