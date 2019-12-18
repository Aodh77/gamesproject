using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rise : MonoBehaviour
{

    public GameObject Tile;
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance = 10.4f;

    private float lerpTime = 5;
    private float currentLerpTime = 0;


    void Start()
    {
        startPos = Tile.transform.position;
        endPos = Tile.transform.position + Vector3.up * distance;
    }

    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if(currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float Perc = currentLerpTime / lerpTime;
        Tile.transform.position = Vector3.Lerp(startPos, endPos, Perc);
    }
}
