using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float[] burstPattern;
    public GameObject projectilePrefab;
    public float burstFireDelay = 1.0f;

    float fireTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTime += Time.deltaTime;
        if(fireTime >= burstFireDelay)
        {
            fireTime = 0.0f;
            FireBurst();
        }
    }

    void FireBurst()
    {
        Vector3 baseFireDirection = transform.forward;
        for (int i = 0; i < burstPattern.Length; ++i)
        {
            Quaternion fireDirection = Quaternion.LookRotation(baseFireDirection, Vector3.up)
                                        * Quaternion.Euler(0.0f, burstPattern[i], 0.0f);

            GameObject projGo = GameObject.Instantiate(projectilePrefab);
            projGo.transform.position = transform.position;
            projGo.transform.rotation = fireDirection;
        }
    }
}
