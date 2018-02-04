using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0, -Time.smoothDeltaTime, 0);
    }

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
