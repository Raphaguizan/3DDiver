using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField]
    private float distance;
    public void OnEnable()
    {
        StartCoroutine(BuoyancyLoop());
    }

    public void OnDisable()
    {
        StopCoroutine(BuoyancyLoop());
    }

    IEnumerator BuoyancyLoop()
    {
        while (this.isActiveAndEnabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance))
            {
                Debug.Log("normal : "+hit.normal);
                Debug.Log("texturecoord : "+hit.textureCoord);
                Debug.Log("distance : "+hit.distance);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * distance, Color.yellow);
                
            }
            yield return new WaitForFixedUpdate();
        }
    }

}
