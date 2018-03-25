using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChecker : MonoBehaviour {

    public List<GameObject> gums;
    public GridManager gManager;

    public GameObject[] gumPrefabs;
    private GameObject gum_parent;

    public List<int> vertaisLista;
    public List<GameObject> destroyThese;

    public List<GameObject> verticalList;
    public List<GameObject> horizontalList;
    public List<List<GameObject>> allRows;

    [SerializeField]
    private float SecretWaitTime;

    public Transform[] spawnPoints;

    [SerializeField]
    private List<int> row0, row1, row2, row3, row4, row5, row6, row7, row8, row9;
    private List<List<int>> allrows;
    private Dictionary<int, List<GameObject>> coordWithObj;

    




    public void Start ()
    {
        gManager = GameObject.Find("Game_manager").GetComponent<GridManager>();
        gum_parent = GameObject.Find("Gum_parent");
     
      //  coordinatesList = new Dictionary<int, List<int>>();

        allrows = new List<List<int>>();
        row0 = new List<int>();
        allrows.Add(row0);
        row1 = new List<int>();
        allrows.Add(row1);
        row2 = new List<int>();
        allrows.Add(row2);
        row3 = new List<int>();
        allrows.Add(row3);
        row4 = new List<int>();
        allrows.Add(row4);
        row5 = new List<int>();
        allrows.Add(row5);
       // row6 = new List<int>();
       // allrows.Add(row6);
       // row7 = new List<int>();
      //  allrows.Add(row7);
      //  row8 = new List<int>();
      //  allrows.Add(row8);
      //  row9 = new List<int>();
      //  allrows.Add(row9);


        vertaisLista = new List<int>();
        destroyThese = new List<GameObject>();
        verticalList = new List<GameObject>();
        horizontalList = new List<GameObject>();


    }

    private void Update()
    {
       if(Input.GetKeyUp(KeyCode.A))
        {
            CheckState();
        }
        
    }


    public bool CheckState ()
    {
        bool resultOfChecking;
        Debug.Log("checki alkaa");
        
        if(gums.Count <= 0)
        GetList();                                                        //hakee listan, jossa on kaikki spawnatut gameobjektit



        if(destroyThese.Count <= 0)
        {
            CheckStateQuick();
        }
        
   //     Debug.Log("horizontal list teko alkaa");

      //  if(horizontalList.Count <= 0)
      //  HorizontalList();

   //     Debug.Log("horizontal list tehty " + horizontalList.Count);
        // ------ HORIZONTAL CHECK ------

        

        /*
        for (int i = 0; i < gums.Count; i++)
        {
            if (i > 0 && i<gums.Count -1)
            {
                if (GetValue(horizontalList[i]) == GetValue(horizontalList[i + 1]) && GetValue(horizontalList[i]) == GetValue(horizontalList[i - 1]))
                {
                    if (YCoordinate(horizontalList[i]) == YCoordinate(horizontalList[i - 1]) && YCoordinate(horizontalList[i + 1]) == YCoordinate(horizontalList[i]))
                    {
                        Debug.Log("Kolme peräkkäin!");


                        if(destroyThese.IndexOf(horizontalList[i-1]) < 0)
                        {
                            destroyThese.Add(horizontalList[i - 1]);
                         //   gums.Remove(horizontalList[i - 1]);
                        }
                        if(destroyThese.IndexOf(horizontalList[i]) < 0)
                        {
                            destroyThese.Add(horizontalList[i]);
                          //  gums.Remove(horizontalList[i]);
                        }
                        if (destroyThese.IndexOf(horizontalList[i + 1]) < 0)
                        {
                            destroyThese.Add(horizontalList[i + 1]);
                          //  gums.Remove(horizontalList[i + 1]);
                        }

                    }
                }
            }
        }
        */
        // if(verticalList.Count <= 0)
        //  VerticalList();




        // --------- VERTICAL CHECK -----------

        /*  
           for (int i = 0; i < verticalList.Count; i++)
           {
               if (i > 0 && i < verticalList.Count -1)
               {
                   if (GetValue(verticalList[i]) == GetValue(verticalList[i + 1]) && GetValue(verticalList[i]) == GetValue(verticalList[i - 1]))
                   {


                       if (XCoordinate(verticalList[i]) == XCoordinate(verticalList[i + 1]) && XCoordinate(verticalList[i - 1]) == XCoordinate(verticalList[i]))
                       {
                           Debug.Log("Kolme peräkkäin pystysuunnassa!");

                           if(destroyThese.IndexOf(verticalList[i-1]) < 0)
                           {
                               destroyThese.Add(verticalList[i - 1]);
                            //   gums.Remove(verticalList[i - 1]);
                           }
                           if (destroyThese.IndexOf(verticalList[i]) < 0)
                           {
                               destroyThese.Add(verticalList[i]);
                            //   gums.Remove(verticalList[i]);
                           }
                           if (destroyThese.IndexOf(verticalList[i + 1]) < 0)
                           {
                               destroyThese.Add(verticalList[i + 1]);
                            //   gums.Remove(verticalList[i + 1]);
                           }

                       }
                   }
               }
           }


           */




        if (destroyThese.Count > 0)                              // --------JOS LÖYTYI TUHOTTAVAA ELI KOLME TAI ENEMMÄN RIVISSÄ
        {
            destroyThese.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.GetComponent<LocationHolder>().getY()).CompareTo(b.GetComponent<LocationHolder>().getY());
            });


            destroyThese.Reverse();
            StartCoroutine(DestroyInFashion());
            resultOfChecking = true;
        }
        else
        {                                                                                   //---------EI LÖYTYNYT, VAIHDA TAKAISIN PAIKOILLEEN
            resultOfChecking = false;                                                       // ----- koska kolmea samaa ei löytynyt, mutta aikaisemmin saattoi löytyä,                                            
                                                                                            // ----- spawnaa nyt uudet

            if(gums.Count < 54 )                    //eli jos on vähemmän kuin 54 palloa pelissä aka juttuja tuhoutui
            {

                for (int i = 0; i < allrows.Count; i++)
                {
                   
                    if (allrows[i].Count > 0)
                    {
                     //   Debug.Log("spawnataan uudet.");
                        for (int j = 0; j < allrows[i].Count; j++)
                        {
                            int random = Random.Range(0, gumPrefabs.Length);
                            GameObject newGum;
                            newGum = Instantiate(gumPrefabs[random], spawnPoints[i].position, spawnPoints[i].rotation) as GameObject;
                            newGum.transform.parent = gum_parent.transform;

                            gums.Add(newGum);

                            //uusi objekti on spawnattu, anna sille koordinaatit
                            newGum.GetComponent<LocationHolder>().SetX(i);
                            newGum.GetComponent<LocationHolder>().SetY(8);
                            StartCoroutine(newGum.GetComponent<MovingScript>().MoveTowardsThis(0, j));

                         //   Debug.Log("uusi gum " + newGum);
                        }
                     
                    }
                }
                foreach (List<int> lista in allrows)
                {
                    lista.Clear();
                }

              
            }
 
   
        }

       // StartCoroutine(DelayedCheck());
        return resultOfChecking;
    }

    private IEnumerator DelayedCheck ()
    {
        yield return new WaitForSeconds(1f);
        bool b = CheckStateQuick();
        if (!b)
        {
            yield return null;
        }
        else
        {
            Invoke("CheckState", 1f);
        }
    }

    

    private bool CheckStateQuick ()
    {
        for (int i = 0; i < gums.Count; i++)
        {
            int xi = XCoordinate(gums[i]);
            int yi = YCoordinate(gums[i]);
            int valueI = GetValue(gums[i]);

            for (int j = 0; j < gums.Count; j++)
            {
                int xj = XCoordinate(gums[j]);
                int yj = YCoordinate(gums[j]);
                int valueJ = GetValue(gums[j]);

                for (int k = 0; k < gums.Count; k++)
                {
                    int xk = XCoordinate(gums[k]);
                    int yk = YCoordinate(gums[k]);
                    int valueK = GetValue(gums[k]);

                    if (xi - xj == -1 && xi - xk == 1 && yi == yj && yi == yk && valueI == valueJ && valueI == valueK || yi - yj == -1 && yi - yk == 1 && xi == xj && xi == xk && valueI == valueJ && valueI == valueK)
                    {
                        Debug.Log("kolme rivissä");
                        if (!destroyThese.Contains(gums[j]))
                        {
                            destroyThese.Add(gums[j]);

                        }
                        if (!destroyThese.Contains(gums[i]))
                        {
                            destroyThese.Add(gums[i]);

                        }
                        if (!destroyThese.Contains(gums[k]))
                        {
                            destroyThese.Add(gums[k]);

                        }
                    }
                }
            }
        }

        if(destroyThese.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private IEnumerator DestroyInFashion ()
    {
        if(destroyThese.Count <= 0)
        {
            yield return null;
        }

        

        foreach(GameObject gum in destroyThese.ToArray())
        {
            int XCoord = XCoordinate(gum);
            int YCoord = YCoordinate(gum);
            List<GameObject> list;
            StartCoroutine(gum.GetComponent<MovingScript>().MoveTowardsThis(4, 1));
            yield return new WaitForSeconds(0.15f);
            //lähetä ylöspäin kaikille viesti, jotka eivät ole destroythese listassa, mutta samalla rivillä, että tulee yhden alaspäin
            switch(XCoord)
            {
                case 0:                 //jos tuhottu esine on x0 rivillä
                    list = gManager.ListGetter(0);
                    row0.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if(YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //        Debug.Log("rivillä 0.");     

                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 1:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(1);
                    row1.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //     Debug.Log("rivillä 1.");
                       
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 2:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(2);
                    row2.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //   Debug.Log("rivillä 2.");
                     
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;


                case 3:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(3);
                    row3.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //  Debug.Log("rivillä 3.");

                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 4:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(4);
                    row4.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //  Debug.Log("rivillä 4.");
                          
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 5:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(5);
                    row5.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //         Debug.Log("rivillä 5.");
                           
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 6:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(6);
                    row6.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //     Debug.Log("rivillä 6.");
                         
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 7:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(7);
                    row7.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //        Debug.Log("rivillä 7.");
                       
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 8:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(8);
                    row8.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //     Debug.Log("rivillä 8.");
                            
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;

                case 9:                 //jos tuhottu esine on x1 rivillä

                    list = gManager.ListGetter(9);
                    row9.Add(XCoord);
                    foreach (GameObject gumRow in list.ToArray())
                    {
                        if (YCoordinate(gumRow) >= YCoord && destroyThese.IndexOf(gumRow) < 0)        //eli jos on tuhotun yläpuolella ja ei ole destroythese listassa
                        {
                            //          Debug.Log("rivillä 9.");
                           
                            StartCoroutine(gumRow.GetComponent<MovingScript>().MoveTowardsThis(0, 1));
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;
            }
            //toista jokin ääniefekti
          //  Destroy(gum, 1.0f);
        }

        foreach(GameObject gum in destroyThese)
        {
            gums.Remove(gum);                                                                   //poista ensin listasta, joka sisältää kaikki pallot
            verticalList.Remove(gum);
            horizontalList.Remove(gum);
            Destroy(gum);
            
            yield return new WaitForSeconds(0.1f);
        }

        
        Debug.Log("lista lahetetty state heckerista");
        gManager.ListSorter();
        destroyThese.Clear();
        CheckState();
        yield return null;
    }

   
    /*
    public bool HorizontalList()
    {
        int counterX = 0;
        int counterY = 0;
        horizontalList.Clear();

      //  while (horizontalList.Count < gums.Count)
       // {


            /*
   
            foreach (GameObject gum in gums)
            {
                if (gum != null)
                {
                    if (YCoordinate(gum) == counterY)
                    {
                        if (XCoordinate(gum) == counterX)
                        {
                            horizontalList.Add(gum);
                            counterX = counterX + 1;

                            if (counterX > 9)
                            {
                                counterY = counterY + 1;
                                counterX = 0;
                            }

                        }

                    }
                }
                
            }
            
        //}

        return true;
    }

    public bool VerticalList()
    {

      //  verticalList.Clear();

        int counterX = 9;
        int counterY = 9;

        
        gums.Reverse();

        while (verticalList.Count < gums.Count)
        {
            foreach (GameObject gum in gums)
            {
                if (gum != null)
                {
                    //  Debug.Log("tämä on valittu1");
                    if (XCoordinate(gum) == counterX)
                    {
                        if (YCoordinate(gum) == counterY)
                        {

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
        }
          

        

        return true;
    }
    */

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
        List<GameObject> l = verticalList;
        return l;
    }

    public void GetList ()
    {
        gums = gManager.gums;

    }
}
