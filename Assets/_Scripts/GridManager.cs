using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{

    public int leveys;
    public int korkeus;
    private int spawnVali = 1;
    private int gum;

    public List<GameObject> gums = new List<GameObject>();   

    public Transform spawnPoint;
    private Transform newSpawnPoint;
    public GameObject gumParent;
    public List<GameObject> row0, row1, row2, row3, row4, row5, row6, row7, row8, row9;
    public Dictionary<int, List<GameObject>> dicOfRows;


    public GameObject[] cube;

    void Start()
    {
        gumParent = GameObject.Find("Gum_parent");

        row0 = new List<GameObject>();
        row1 = new List<GameObject>();
        row2 = new List<GameObject>();
        row3 = new List<GameObject>();
        row4 = new List<GameObject>();
        row5 = new List<GameObject>();
        row6 = new List<GameObject>();
        row7 = new List<GameObject>();
        row8 = new List<GameObject>();
        row9 = new List<GameObject>();


        CreateGrid();               //init grid
       
    }

    public void CreateGrid()
    {
        

        for (int i = 0; i < korkeus; i++)
        {
           

            gum = Random.Range(0, cube.Length);
            
            if(gums.Count > 11)
            {
                while(gum == gums[gums.Count - 6].GetComponent<LocationHolder>().getValue() && gum == gums[gums.Count - 12].GetComponent<LocationHolder>().getValue())
                {
                    gum = Random.Range(0, cube.Length);
                }
            }

            //luodaan ensimmäisen rivin ensimmäinen purkka jne.
            GameObject o = Instantiate(cube[gum], spawnPoint.position, spawnPoint.rotation) as GameObject;
            o.transform.parent = gumParent.transform;

            LocationHolder coord1;

            coord1 = o.GetComponent<LocationHolder>();
            coord1.SetX(0);
            coord1.SetY(i);

            gums.Add(o);



            for (int j = 1; j < leveys; j++, spawnVali += 1)
            {
                
                gum = Random.Range(0, cube.Length);
                
                //tehdään niin, että alussa ei voi olla kuin max. 2 samanväristä peräkkäin
                if(gums.Count > 1)
                {
                    while(gum == gums[gums.Count - 1].GetComponent<LocationHolder>().getValue() && gum == gums[gums.Count - 2].GetComponent<LocationHolder>().getValue())
                    {
                        //Debug.Log("tehdään uusi cube");
                        gum = Random.Range(0, cube.Length);
                    }
                    if(gums.Count > 12)
                    {
                        while(gum == gums[gums.Count - 6].GetComponent<LocationHolder>().getValue() && gum == gums[gums.Count - 12].GetComponent<LocationHolder>().getValue())
                        {
                           // Debug.Log("tehdään uusi cube pystysuoraan");
                            gum = Random.Range(0, cube.Length);
                        }
                    }
                }
                GameObject g = Instantiate(cube[gum], new Vector3(spawnPoint.position.x + spawnVali, spawnPoint.position.y, spawnPoint.position.z), spawnPoint.rotation) as GameObject;
                g.transform.parent = gumParent.transform;

                LocationHolder coord2;

                coord2 = g.GetComponent<LocationHolder>();
                coord2.SetX(j);
                coord2.SetY(i);

                gums.Add(g);


            }
            spawnVali = 1;
            spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + spawnVali, spawnPoint.position.z);

        }

        Debug.Log("grid valmis");
        ListSorter();
    }

    public void ListSorter()
    {
        ListClearer();

       foreach(GameObject gum in gums)
        {
           
            if(gum != null)
            { 
                    int xCor = gum.GetComponent<LocationHolder>().getX();

                switch (xCor)
                {
                    case 0:
                        //eli jos on rivillä 0
                        row0.Add(gum);
                        break;

                    case 1:
                        //eli jos on rivillä 0
                        row1.Add(gum);
                        break;

                    case 2:
                        //eli jos on rivillä 0
                        row2.Add(gum);
                        break;
                    case 3:
                        //eli jos on rivillä 0
                        row3.Add(gum);
                        break;
                    case 4:
                        //eli jos on rivillä 0
                        row4.Add(gum);
                        break;
                    case 5:
                        //eli jos on rivillä 0
                        row5.Add(gum);
                        break;
                    case 6:
                        //eli jos on rivillä 0
                        row6.Add(gum);
                        break;
                    case 7:
                        //eli jos on rivillä 0
                        row7.Add(gum);
                        break;
                    case 8:
                        //eli jos on rivillä 0
                        row8.Add(gum);
                        break;
                    case 9:
                        //eli jos on rivillä 0
                        row9.Add(gum);
                        break;
                }
            }

        }

    }

    private void ListClearer ()
    {
        row0.Clear();
        row1.Clear();
        row2.Clear();
        row3.Clear();
        row4.Clear();
        row5.Clear();
        row6.Clear();
        row7.Clear();
        row8.Clear();
        row9.Clear();
   
    }

    public List<GameObject> ListGetter (int row)
    {
        switch(row)
        {
            case 0:
                return row0;
                break;

            case 1:
                return row1;
                break;

            case 2:
                return row2;
                break;

            case 3:
                return row3;
                break;

            case 4:
                return row4;
                break;

            case 5:
                return row5;
                break;

            case 6:
                return row6;
                break;

            case 7:
                return row7;
                break;

            case 8:
                return row8;
                break;

            case 9:
                return row9;
                break;
        }
        return null;
    }
        
    

  
}
