using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterChanges : MonoBehaviour
{
    [SerializeField]
    private Transform lookDistance;
    [SerializeField]
    private Color underWaterColor, defaultColor = Color.black;
    [SerializeField]
    private float underWaterFogDensity, defaultWaterFogDensity = 0.15f;
    [SerializeField]
    private GameObject shatter;
    [SerializeField]
    private GameObject bubbles;

    [SerializeField]
    private GameObject particles;


    private void Update()
    {
        if (lookDistance.position.y < WaveManager.Instance.GetWaveHeight(transform.position.x))
        {
            RenderSettings.fogColor = underWaterColor;
            RenderSettings.fogDensity = underWaterFogDensity;
            shatter.SetActive(true);
            bubbles.SetActive(true);
            if (particles) particles.SetActive(true);
        }
        else
        {
            RenderSettings.fogColor = defaultColor;
            RenderSettings.fogDensity = defaultWaterFogDensity;
            shatter.SetActive(false);
            bubbles.SetActive(false);
            if (particles)particles.SetActive(false);
        }
    }
}
