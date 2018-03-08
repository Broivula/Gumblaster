using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChecker : MonoBehaviour {

    public List<GameObject> gums;
    public GridManager gManager;


    public List<int> vertaisLista;
    public List<GameObject> destroyThese;

    public List<GameObject> verticalList;
    public List<GameObject> horizontalList;
    public List<List<GameObject>> allRows;


    private Dictionary<int, List<GameObject>> coordWithObj;
    private Dictionary<int, List<int>> coordinatesList;




    public void Start ()
    {
        gManager = GameObject.Find("Game_manager").GetComponent<GridManager>();
        coordWithObj = new Dictionary<int, List<GameObject>>();
        coordinatesList = new Dictionary<int, List<int>>();
       

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            CheckState();
        }

    }


    public void CheckState ()
    {
        vertaisLista = new List<int>();
        destroyThese = new List<GameObject>();
        verticalList = new List<GameObject>();
        horizontalList = new List<GameObject>();
        GetList();                                                        //hakee listan, jossa on kaikki spawnatut gameobjektit








        HorizontalList();
                // ------ HORIZONTAL CHECK ------
                
        for (int i = 0; i < gums.Count; i++)
        {
            if (i > 1 && i<gums.Count-1)
            {
                if (GetValue(horizontalList[i]) == GetValue(horizontalList[i + 1]) && GetValue(horizontalList[i]) == GetValue(horizontalList[i - 1]))
                {
                    if (YCoordinate(horizontalList[i]) == YCoordinate(horizontalList[i - 1]) && YCoordinate(horizontalList[i + 1]) == YCoordinate(horizontalList[i]))
                    {
                        Debug.Log("Kolme peräkkäin!");
                            if(!destroyThese.Contains(horizontalList[i - 1]) && !destroyThese.Contains(horizontalList[i]) && !destroyThese.Contains(horizontalList[i + 1]))
                        {
                            destroyThese.Add(horizontalList[i - 1]);
                            destroyThese.Add(horizontalList[i]);
                            destroyThese.Add(horizontalList[i + 1]);
                        }else if (!destroyThese.Contains(horizontalList[i - 1]) && !destroyThese.Contains(horizontalList[i]) && destroyThese.Contains(horizontalList[i + 1]))
                        {
                            destroyThese.Add(horizontalList[i - 1]);
                            destroyThese.Add(horizontalList[i]);
                        }else if (!destroyThese.Contains(horizontalList[i - 1]) && destroyThese.Contains(horizontalList[i]) && !destroyThese.Contains(horizontalList[i + 1]))
                        {
                            destroyThese.Add(horizontalList[i - 1]);
                            destroyThese.Add(horizontalList[i + 1]);
                        }else if(destroyThese.Contains(horizontalList[i - 1]) && destroyThese.Contains(horizontalList[i]) && !destroyThese.Contains(horizontalList[i + 1]))
                        {
                            destroyThese.Add(horizontalList[i]);
                            destroyThese.Add(horizontalList[i + 1]);
                        }
                        else
                        {

                            Debug.Log("Kaikki olivat jo tuhottu");
                        }

  
                        
         
                    }
                }
            }
        }
        
        VerticalList();




        // --------- VERTICAL CHECK -----------

        
        for (int i = 0; i < verticalList.Count; i++)
        {
            if (i > 1 && i < verticalList.Count - 1)
            {
                if (GetValue(verticalList[i]) == GetValue(verticalList[i + 1]) && GetValue(verticalList[i]) == GetValue(verticalList[i - 1]))
                {
                    if (XCoordinate(verticalList[i]) == XCoordinate(verticalList[i - 1]) && XCoordinate(verticalList[i + 1]) == XCoordinate(verticalList[i]))
                    {
                        Debug.Log("Kolme peräkkäin pystysuunnassa!");

                        if (!destroyThese.Contains(verticalList[i - 1]) && !destroyThese.Contains(verticalList[i]) && !destroyThese.Contains(verticalList[i + 1]))
                        {
                            destroyThese.Add(verticalList[i - 1]);
                            destroyThese.Add(verticalList[i]);
                            destroyThese.Add(verticalList[i + 1]);
                        }
                        else if (!destroyThese.Contains(verticalList[i - 1]) && !destroyThese.Contains(verticalList[i]) && destroyThese.Contains(verticalList[i + 1]))
                        {
                            destroyThese.Add(verticalList[i - 1]);
                            destroyThese.Add(verticalList[i]);
                        }
                        else if (!destroyThese.Contains(verticalList[i - 1]) && destroyThese.Contains(verticalList[i]) && !destroyThese.Contains(verticalList[i + 1]))
                        {
                            destroyThese.Add(verticalList[i - 1]);
                            destroyThese.Add(verticalList[i + 1]);
                        }
                        else if (destroyThese.Contains(verticalList[i - 1]) && destroyThese.Contains(verticalList[i]) && !destroyThese.Contains(verticalList[i + 1]))
                        {
                            destroyThese.Add(verticalList[i]);
                            destroyThese.Add(verticalList[i + 1]);
                        }
                        else
                        {

                            Debug.Log("Kaikki olivat jo tuhottu");
                        }
                    }
                }
            }
        }
        

        foreach(GameObject gum in destroyThese)
        {
            Destroy(gum);
        }


        //käske kaikkia jäljellä olevia liikkumaan alaspäin

        foreach(GameObject gum in gums)
        {
        //lähetä movingscriptiin käsky, joka liikuttaa niitä alaspäin
        }

    }

    public bool HorizontalList()
    {
        int counterX = 0;
        int counterY = 0;


        while (horizontalList.Count < 100)
        {
   
            foreach (GameObject gum in gums)
            {
               if (YCoordinate(gum) == counterY)
               {
                  if (XCoordinate(gum) == counterX)
                    {
                        horizontalList.Add(gum);
                        counterX = counterX + 1;

                        if(counterX > 9)
                        {
                            counterY = counterY + 1;
                            counterX = 0;
                        }

                    }
                       
               }
                
            }
        }

        return true;
    }

    public bool VerticalList()
    {

        verticalList.Clear();

        int counterX = 9;
        int counterY = 9;

        while (verticalList.Count < 100)
        {
            foreach (GameObject gum in gums)
            {
                if (XCoordinate(gum) == counterX)
                {
                    if (YCoordinate(gum) == counterY)
                    {
                        
                       // Debug.Log("tämä on valittu" + gum);
                        verticalList.Add(gum);
                        counterY = counterY - 1;

                        if (counterY == -1)
                        {
                            counterX = counterX - 1;
                            counterY = 9;
                        }
                    }


                }
            }

        }
        
        return true;
    }


    public bool VertaisLista ()
    {
        for (int i = 0; i < 10; i++)
        {

            for (int j = 0; j < 10; j++)
            {

                vertaisLista.Add(GetValue(ListInQuestion(i)[j]));

                //   Debug.Log("lista " + ListInQuestion(i));
                // Debug.Log("vertais " + GetValue(ListInQuestion(i)[j]) + " ja ii : " + i);
                //Debug.Log("name " + name);
            }
        }
        return true;
    }

    public List<int> VertaisLista (int i)
    {
        List<int> l = coordinatesList[i];

        return l;
    }


    public List<GameObject> ListInQuestion(int i)
    {
        List<GameObject> l = coordWithObj[i];
        //  Debug.Log("allrows " + allRows[i]);

        return l;
    }

/*
    public bool SetLists()
    {
        //allRows = new List<List<GameObject>>();

        foreach (GameObject gum in gums)
        {
            int Ykoord = YCoordinate(gum);

            switch(Ykoord)
            {
                case 0:
                    rows0.Add(gum);
                    vert0.Add(GetValue(gum));
                    if (rows0.Count > 9)
                    {
                        coordWithObj.Add(0, rows0);
                        coordinatesList.Add(0, vert0);
                    }
                    break;

                case 1:
                    rows1.Add(gum);
                    vert1.Add(GetValue(gum));
                    if (rows1.Count > 9)
                    {
                        coordWithObj.Add(1, rows1);
                        coordinatesList.Add(1, vert1);
                    }
                    break;

                case 2:
                    rows2.Add(gum);
                    vert2.Add(GetValue(gum));
                    if (rows2.Count > 9)
                    {
                        coordWithObj.Add(2, rows2);
                        coordinatesList.Add(2, vert2);
                    }
                    break;

                case 3:
                    rows3.Add(gum);
                    vert3.Add(GetValue(gum));
                    if (rows3.Count > 9)
                    {
                        coordWithObj.Add(3, rows3);
                        coordinatesList.Add(3, vert3);
                    }
                    break;

                case 4:
                    rows4.Add(gum);
                    vert4.Add(GetValue(gum));
                    if (rows4.Count > 9)
                    {
                        coordWithObj.Add(4, rows4);
                        coordinatesList.Add(4, vert4);
                    }
                    break;

                case 5:
                    rows5.Add(gum);
                    vert5.Add(GetValue(gum));
                    if (rows5.Count > 9)
                    {
                        coordWithObj.Add(5, rows5);
                        coordinatesList.Add(5, vert5);
                    }
                    break;

                case 6:
                    rows6.Add(gum);
                    vert6.Add(GetValue(gum));
                    if (rows6.Count > 9)
                    {
                        coordWithObj.Add(6, rows6);
                        coordinatesList.Add(6, vert6);
                    }
                    break;

                case 7:
                    rows7.Add(gum);
                    vert7.Add(GetValue(gum));
                    if (rows7.Count > 9)
                    {
                        coordWithObj.Add(7, rows7);
                        coordinatesList.Add(7, vert7);
                    }
                    break;

                case 8:
                    rows8.Add(gum);
                    vert8.Add(GetValue(gum));
                    if (rows8.Count > 9)
                    {
                        coordWithObj.Add(8, rows8);
                        coordinatesList.Add(8, vert8);
                    }
                    break;

                case 9:
                    rows9.Add(gum);
                    vert9.Add(GetValue(gum));
                    if (rows9.Count > 9)
                    {
                        coordWithObj.Add(9, rows9);
                        coordinatesList.Add(9, vert9);
                    }
                    break;


            }

        }

        return true;

    }
    */
    

    static int YCoordinate(GameObject o)
    {
     //   Debug.Log("eka y koord " + o.GetComponent<LocationHolder>().getY());
        return o.GetComponent<LocationHolder>().getY();
       
    }

    static int XCoordinate(GameObject o)
    {
        //   Debug.Log("eka y koord " + o.GetComponent<LocationHolder>().getY());
        return o.GetComponent<LocationHolder>().getX();

    }

    static int GetValue(GameObject o)
    {
        return o.GetComponent<LocationHolder>().getValue();
    }

    public List<GameObject> GetListOfObjects ()
    {
        List<GameObject> l = gums;
        return l;
    }

    public void GetList ()
    {
        gums = gManager.gums;

    }
}
