using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAreaScript : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pictureFrameTemplate")
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

}
