using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour {

    public GameObject playerView;
    public LayerMask playerMask;

    public float visionRange = 10f;
    public float fov = 130f;

    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
        /*
        GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test.name = "look dir";
        test.transform.position = this.transform.position + this.transform.forward / 10f;
        test.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        */
	}

    void Shoot(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = dir.normalized * 5f;
    }
	
	// Update is called once per frame
	void Update () {
        //The vector going from the guard to the player
        Vector3 dispVector = playerView.transform.position - this.transform.position;
        
        //if vector is outside range, ignore.
        if (dispVector.magnitude <= visionRange)
        {
            //do raycast from guard to person
            RaycastHit[] hits = Physics.RaycastAll(this.transform.position, dispVector, dispVector.magnitude);
            int count = 0;
            foreach( RaycastHit hit in hits)
            {
                if (!hit.collider.CompareTag("Transparenct"))
                {
                    count++;
                }
            }

            //if hits.length == 1, then only the player was on the vector, aka the guard can see the player
            if (hits.Length == 1 && Vector3.Angle(this.transform.forward, dispVector) < fov/2)
            {
                //set red if detected
                this.GetComponent<Renderer>().material.color = new Color(0.8f, 0.1f, 0.0f);
                Shoot(dispVector);
                return;
            }
        }

        //set to green by default
        this.GetComponent<Renderer>().material.color = new Color(0.1f, 0.8f, 0.0f);
    }
}
