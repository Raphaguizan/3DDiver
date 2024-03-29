using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater1 : MonoBehaviour
{
    Rigidbody rb;


    [SerializeField]
    private float depthBeforeSubmerged = 1f, displacementAmount = 1f;
    [SerializeField]
    private float waterDrag = 1f, waterAngularDrag = 1f;

    [SerializeField]
    private bool useGravity;

    private List<Transform> floaters;
    private bool CanMakeWave = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        floaters = new List<Transform>();
        foreach (Transform item in transform)
        {
            if (item.CompareTag("floater"))
            {
                floaters.Add(item);
            }
        }
    }

    private void FixedUpdate()
    {
        if(floaters.Count == 0)
        {
            Debug.Log("n�o tem ponto de gravidade");
            return;
        }
        for (int i = 0; i < floaters.Count; i++)
        {
            if(useGravity)rb.AddForceAtPosition(Physics.gravity / floaters.Count, floaters[i].position, ForceMode.Acceleration);
            float waveHeight = rippleSharp.Instance.GetWaveHeigth(floaters[i].position); //WaveManager.Instance.GetWaveHeight(floaters[i].position.x);
            if (floaters[i].position.y < waveHeight)
            {
                float displacementMultiplier = Mathf.Clamp01(waveHeight - floaters[i].position.y / depthBeforeSubmerged) * displacementAmount;
                rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), floaters[i].position, ForceMode.Acceleration);
                rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
                rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            RaycastHit hit;
            if (Physics.Raycast(floaters[i].position, Vector3.down, out hit))
            {
                if (hit.transform.CompareTag("water"))
                {
                    if (hit.distance < depthBeforeSubmerged)
                    {
                        if (CanMakeWave && rb.velocity.y <= 0)
                        {
                            rippleSharp.Instance.MakeWave(hit, rb.velocity.y);
                            CanMakeWave = false;
                        }
                    }
                    else
                    {
                        CanMakeWave = true;
                    }
                }
            }
        }

    }
}
