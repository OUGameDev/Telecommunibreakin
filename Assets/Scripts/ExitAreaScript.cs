using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAreaScript : MonoBehaviour {


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "pictureFrameTemplate")
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
