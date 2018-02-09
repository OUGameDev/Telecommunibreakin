using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundScript : MonoBehaviour {

    GameObject bounder;

    private void OnCollisionExit(Collision collision)
    {
        Vector3 vect = collision.collider.transform.position.normalized * 0.7f;
        bounder.transform.position = collision.collider.transform.position + vect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounder.transform.position = new Vector3(1000, 1000, 1000);
    }

}
