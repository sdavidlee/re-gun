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
    public float sFireDelay = 0.1f;

    float fireTime = 0.0f;
    bool sFireActive = false;
    bool sFireDirectionForward = true;
    int sFireCurrentIndex = 0;
    float[] patternToUse;
    float sFireTime = 0.0f;
    float sFireAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        patternToUse = burstPattern1;
    }

    // Update is called once per frame
    void Update()
    {

        if (sFireActive)
        {
            sFire();
            return;
        }

        fireTime += Time.deltaTime;
        if (fireTime >= burstFireDelay)
        {
            fireTime = 0.0f;
            FireBurst();
        }
    }

    void sFire()
    {
        if (patternToUse.Length == 0)
        {
            currentPattern++;
            if (currentPattern > numPatterns)
            {
                currentPattern = 1;
            }
            sFireActive = false;
            return;
        }

        sFireTime += Time.deltaTime;
        if (sFireTime >= sFireDelay)
        {
            if (sFireDirectionForward)
            {
                sFireAngle = patternToUse[sFireCurrentIndex];
                sFireCurrentIndex++;
                if (sFireCurrentIndex >= patternToUse.Length)
                {
                    sFireCurrentIndex = patternToUse.Length - 1;
                    sFireDirectionForward = false;
                }
            }
            else
            {
                sFireAngle = patternToUse[sFireCurrentIndex];
                sFireCurrentIndex--;
                if (sFireCurrentIndex < 0)
                {
                    sFireCurrentIndex = 0;
                    sFireDirectionForward = true;
                    currentPattern++;
                    if (currentPattern > numPatterns)
                    {
                        currentPattern = 1;
                    }
                    sFireActive = false;
                }
            }

            Vector3 baseFireDirection = transform.forward;
            sFireTime = 0.0f;
            Quaternion fireDirection = Quaternion.LookRotation(baseFireDirection, Vector3.up)
                                        * Quaternion.Euler(0.0f, sFireAngle, 0.0f);

            GameObject projGo = GameObject.Instantiate(projectilePrefab);
            projGo.transform.position = transform.position;
            projGo.transform.rotation = fireDirection;
        }
    }

    void FireBurst()
    {
        if (Random.value > 0.5f)
        {
            sFireActive = true;
        }

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

        if (patternToUse.Length == 0)
        {
            currentPattern++;
            if (currentPattern > numPatterns)
            {
                currentPattern = 1;
            }
            sFireActive = false;
            return;
        }

        if(!sFireActive)
        {
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
}
