using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class EMPScript : MonoBehaviour
{

    private Rigidbody body;
    public GameObject free;
    public GameObject hips;

    private ParticleSystem sys;

    private void Awake()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += EMPScript_InteractableObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += EMPScript_InteractableObjectUngrabbed;
        body = GetComponent<Rigidbody>();

        sys = GetComponent<ParticleSystem>();
    }

    private void EMPScript_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (body.velocity.magnitude < 2f)
        {
            body.constraints = RigidbodyConstraints.FreezeAll;
            transform.localPosition = new Vector3(0.25f, 0, 0);
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            transform.SetParent(free.transform);
            StartCoroutine(Detonate());
        }
    }

    private void EMPScript_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        body.constraints = RigidbodyConstraints.None;
    }

    IEnumerator Detonate()
    {
        yield return new WaitForSeconds(3f);

        Collider[] hits = Physics.OverlapSphere(transform.position, 5f);

        Debug.Log(hits.Length);

        foreach (Collider hit in hits)
        {
            if (hit.name == "Robot")
            {
                Debug.Log("hit a robo");
                hit.transform.Find("Head").GetComponent<GuardScript>().setDisabled(true);
                var emiss = hit.GetComponent<ParticleSystem>().emission;
                emiss.enabled = true;
            }
        }

        var main = sys.main;
        main.maxParticles = 110;
        main.startSize = 10f;
        main.simulationSpeed = 3f;
        sys.Emit(100);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}