using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour {

    public GameObject playerView;
    public LayerMask playerMask;

    public GameObject coloryThingy;

    public float visionRange = 10f;
    public float fov = 130f;

    public GameObject bulletPrefab;

    private float lastShot;

    private bool isOn = true;

    private bool disabled = false;

	// Use this for initialization
	void Start () {
        /*
        GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
        test.name = "look dir";
        test.transform.position = this.transform.position + this.transform.forward / 10f;
        test.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        */
        lastShot = Time.time;
	}

    void Shoot(Vector3 dirPos)
    {
        if (Time.time - lastShot > 0.8f)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.transform.position + this.transform.forward * this.transform.lossyScale.x;
            bullet.GetComponent<Rigidbody>().velocity = (playerView.transform.position - bullet.transform.position).normalized * 20f;
            lastShot = Time.time;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOn || disabled)
        {
            return;
        }
        //The vector going from the guard to the player
        Vector3 dispVector = playerView.transform.position - this.transform.position;
        Debug.DrawRay(this.transform.position, this.transform.forward);
        
        //if vector is outside range, ignore.
        if (dispVector.magnitude <= visionRange)
        {
            //do raycast from guard to person
            RaycastHit[] hits = Physics.RaycastAll(this.transform.position, dispVector, dispVector.magnitude);
            int count = 0;
            foreach( RaycastHit hit in hits)
            {
                if (!hit.collider.CompareTag("Transparent"))
                {
                    count++;
                }
            }

            //if hits.length == 1, then only the player was on the vector, aka the guard can see the player
            if (count == 0 && Vector3.Angle(this.transform.forward, dispVector) < fov/2)
            {
                //set red if detected
                coloryThingy.GetComponent<Renderer>().material.color = new Color(0.8f, 0.1f, 0.0f);
                Shoot(playerView.transform.position);
                return;
            }
        }

        //set to green by default
        coloryThingy.GetComponent<Renderer>().material.color = new Color(0.1f, 0.8f, 0.0f);

    }

    public void SetOnOff(float x)
    {
        Debug.Log(x);
        isOn = x < 50;
    }

    public void setDisabled(bool x)
    {
        disabled = x;
    }
}
