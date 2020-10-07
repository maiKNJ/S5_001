﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class object_spawner : MonoBehaviour
{
    PoolManager poolManager;
    SpectralFluxAnalyzer realTimeSpectralFluxAnalyzer;
    AudioSource audioSource;
    public FFTWindow FFTWindow;
    void Start()
    {
        poolManager = PoolManager.Instance;
        audioSource = GetComponent<AudioSource>();
        realTimeSpectralFluxAnalyzer = new SpectralFluxAnalyzer();
    }

    void FixedUpdate()
    {
        float[] spectrum = new float[1024];

        //GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow);
        realTimeSpectralFluxAnalyzer.analyzeSpectrum(spectrum, audioSource.time);

        if (realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.02 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() < 0.04)
        {
            Debug.Log("check " + realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux());
            poolManager.spawnFromPool("Cube", transform.position, Quaternion.identity);
        }
    }
}