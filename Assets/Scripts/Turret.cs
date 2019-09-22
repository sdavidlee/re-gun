using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float[] burstPattern1;
    public float[] burstPattern2;
    public float[] burstPattern3;
    public float[] burstPattern4;
    public float[] burstPattern5;
    public float[] burstPattern6;
    public float[] burstPattern7;
    public float[] burstPattern8;
    public float[] burstPattern9;
    public int currentPattern = 1;
    public int numPatterns = 3;
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
        float[] patternToUse;
        switch (currentPattern)
        {
            case 1:
                patternToUse = burstPattern1;
                break;
            case 2:
                patternToUse = burstPattern2;
                break;
            case 3:
                patternToUse = burstPattern3;
                break;
            case 4:
                patternToUse = burstPattern4;
                break;
            case 5:
                patternToUse = burstPattern5;
                break;
            case 6:
                patternToUse = burstPattern6;
                break;
            case 7:
                patternToUse = burstPattern7;
                break;
            case 8:
                patternToUse = burstPattern8;
                break;
            case 9:
                patternToUse = burstPattern9;
                break;
            default:
                patternToUse = new float[] { 0.0f };
                break;
        }
        Vector3 baseFireDirection = transform.forward;
        for (int i = 0; i < patternToUse.Length; ++i)
        {
            Quaternion fireDirection = Quaternion.LookRotation(baseFireDirection, Vector3.up)
                                        * Quaternion.Euler(0.0f, patternToUse[i], 0.0f);

            GameObject projGo = GameObject.Instantiate(projectilePrefab);
            projGo.transform.position = transform.position;
            projGo.transform.rotation = fireDirection;
        }
        currentPattern++;
        if (currentPattern > numPatterns)
        {
            currentPattern = 1;
        }
    }
}
