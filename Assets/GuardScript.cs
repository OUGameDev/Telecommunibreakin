using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour {

    public GameObject playerView;
    public LayerMask playerMask;

	// Use this for initialization
	void Start () {
        /*
        GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test.name = "look dir";
        test.transform.position = this.transform.position + this.transform.forward / 10f;
        test.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        */
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dispVector = playerView.transform.position - this.transform.position;
        if (dispVector.magnitude <= 10)
        {
            RaycastHit[] hits = Physics.RaycastAll(this.transform.position, dispVector, dispVector.magnitude);
            if (hits.Length == 1 && Vector3.Angle(this.transform.forward, dispVector) < 45)
            {
                this.GetComponent<Renderer>().material.color = new Color(0.8f, 0.1f, 0.0f);
                return;
            }
        }

        this.GetComponent<Renderer>().material.color = new Color(0.1f, 0.8f, 0.0f);
    }
}
