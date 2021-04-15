using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField]
    private float amplitude = 1f, length = 2f, speed = 1f, offset = 0f;

    private void Update()
    {
        offset += speed * Time.deltaTime;
    }

    public float GetWaveHeight (float _x)
    {
        return amplitude * Mathf.Sin(_x / length +offset);
    }
}
