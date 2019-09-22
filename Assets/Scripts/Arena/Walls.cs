using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        var wallCollider = GetComponent<Collider>();
        Debug.Log("collided outside");

        if (other.GetComponent<TestController>() != null)
        {
            Debug.Log("collider");

            var isOverlapped = Physics.ComputePenetration(
                other, other.transform.position, other.transform.rotation,
                wallCollider, wallCollider.transform.position, wallCollider.transform.rotation,
                out Vector3 direction, out float distance);

            other.transform.position += direction * distance;
        }
    }
}
