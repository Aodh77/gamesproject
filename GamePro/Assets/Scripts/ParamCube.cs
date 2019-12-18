using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour

{

    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;
    Material material;
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        int randB = Random.Range(0, 7);
        band = randB;
    }

    // Update is called once per frame
    void Update()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioAnalyzer.bandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z);
            Color color = new Color(AudioAnalyzer.audioBandBuffer[band], AudioAnalyzer.audioBandBuffer[band], AudioAnalyzer.audioBandBuffer[band]);
            material.SetColor("_EmissionColor", color);
        }
        if (!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioAnalyzer.bands[band] * scaleMultiplier) + startScale, transform.localScale.z);
            Color color = new Color(AudioAnalyzer.audioBand[band], AudioAnalyzer.audioBand[band], AudioAnalyzer.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }


    }
}
