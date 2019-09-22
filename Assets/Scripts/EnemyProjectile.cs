using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifetime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += transform.forward * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("OnTakeDamage", damage, SendMessageOptions.DontRequireReceiver);

        Destroy(this.gameObject);
    }
}
