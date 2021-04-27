using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLigthIntensity : MonoBehaviour
{
    Light lt;
    public float modifier;
    float initialIntensity;
    [SerializeField]
    private GameObject shatter;
    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        initialIntensity = lt.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))
        {
            if (hit.transform.CompareTag("water")) return;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            lt.intensity =  initialIntensity - (modifier / hit.distance);

        }
        else
        {
            lt.intensity = initialIntensity;
        }
    }

    public void ActiveShatter(bool val)
    {
        shatter.SetActive(val);
    }
}
