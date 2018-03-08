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

    public GameObject[] cube;

    void Start()
    {
        gumParent = GameObject.Find("Gum_parent");

       
        CreateGrid();               //init grid
       
    }

    public void CreateGrid()
    {
        

        for (int i = 1; i < korkeus + 1; i++)
        {
            int counter;
            int previousGum;

            gum = Random.Range(0, cube.Length);
            

            //luodaan ensimmäisen rivin ensimmäinen purkka jne.
            GameObject o = Instantiate(cube[gum], spawnPoint.position, spawnPoint.rotation) as GameObject;
            o.transform.parent = gumParent.transform;

            LocationHolder coord1;

            coord1 = o.GetComponent<LocationHolder>();
            coord1.SetX(0);
            coord1.SetY(i-1);

            gums.Add(o);
            


            for (int j = 1; j < leveys; j++, spawnVali += 1)
            {
                //      spawnPoint.position = new Vector3(spawnPoint.position.x + 2, spawnPoint.position.y, spawnPoint.position.z);
                gum = Random.Range(0, cube.Length);
                GameObject g = Instantiate(cube[gum], new Vector3(spawnPoint.position.x + spawnVali, spawnPoint.position.y, spawnPoint.position.z), spawnPoint.rotation) as GameObject;
                g.transform.parent = gumParent.transform;

                LocationHolder coord2;

                coord2 = g.GetComponent<LocationHolder>();
                coord2.SetX(j);
                coord2.SetY(i-1);

                gums.Add(g);

            }
            spawnVali = 1;
            spawnPoint.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + spawnVali, spawnPoint.position.z);

        }

        Debug.Log("grid valmis");

    }

  
}
