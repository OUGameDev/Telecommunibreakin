using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class LeverScript : MonoBehaviour {

    public GuardScript script;
    private VRTK_Control_UnityEvents controlEvents;

    private void Start()
    {
        controlEvents = GetComponent<VRTK_Control_UnityEvents>();
        if (controlEvents == null)
        {
            controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
        }

        controlEvents.OnValueChanged.AddListener(HandleChange);
    }

    private void HandleChange(object sender, Control3DEventArgs e)
    {
        script.SetOnOff(e.normalizedValue);
    }
}
