using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{    // Update is called once per frame
    void Update()
    {
        Vector3 toCam = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-toCam, Vector3.up);
    }
}
