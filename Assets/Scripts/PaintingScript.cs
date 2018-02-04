using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class PaintingScript : MonoBehaviour
{

    private Rigidbody body;

    private void Awake()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += EMPScript_InteractableObjectUngrabbed;
        body = GetComponent<Rigidbody>();
    }

    private void EMPScript_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        body.constraints = RigidbodyConstraints.None;
    }

}
