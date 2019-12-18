using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject topTile;
    public GameObject currentTile;

    
 

    public GameObject[] cubePrefabs;


    public static PathManager instance = null;


    private PathManager Awake()
    {
       
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<PathManager>();
        }
        return instance;
    }

    



    void Start()
    {
        
        for (int i = 0; i < 3; i++)
        {
            SpawnTile();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile()
    {
        

       currentTile = Instantiate(topTile, currentTile.transform.GetChild(0).transform.GetChild(0).position, Quaternion.identity);


        for (int i = 1; i < 5; i++)
        {
            int randC = Random.Range(0, 3);
            Instantiate(cubePrefabs[randC], currentTile.transform.GetChild(0).transform.GetChild(i).position, Quaternion.identity);
        }
    }
}
