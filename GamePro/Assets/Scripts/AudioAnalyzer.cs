using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{

    
    public AudioClip clip;
    AudioSource a;
    public AudioMixerGroup amgMaster;

    public string selectedDevice;

    public static int frameSize = 512;
    public static float[] spectrum;
    public static float[] bands;
    public static float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];

    float[] bandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public float binWidth;
    public float sampleRate;

   

    private void Awake()
    {
        a = GetComponent<AudioSource>();
        spectrum = new float[frameSize];
        bands = new float[(int)Mathf.Log(frameSize, 2)];
        a.clip = clip;
        a.outputAudioMixerGroup = amgMaster;
        
        a.Play();
    }

    // Use this for initialization
    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        binWidth = AudioSettings.outputSampleRate / 2 / frameSize;
    }

   


    void GetFrequencyBands()
    {
        for (int i = 0; i < bands.Length; i++)
        {
            int start = (int)Mathf.Pow(2, i) - 1;
            int width = (int)Mathf.Pow(2, i);
            int end = start + width;
            float average = 0;
            for (int j = start; j < end; j++)
            {
                average += spectrum[j] * (j + 1);
            }
            average /= (float)width;
            bands[i] = average;
            //Debug.Log(i + "\t" + start + "\t" + end + "\t" + start * binWidth + "\t" + (end * binWidth));
        }

    }


    // Update is called once per frame
    void Update()
    {
        a.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        GetFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }

    void CreateAudioBands()
    {
        for(int i = 0; i < 8; i++)
        {
            if (bands[i] > bandHighest[i])
            {
                bandHighest[i] = bands[i];
            }
            audioBand[i] = (bands[i] / bandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / bandHighest[i]);
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if(bands[g] > bandBuffer[g])
            {
                bandBuffer[g] = bands[g];
                bufferDecrease[g] = 0.005f;
            }
            if (bands[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }
}
