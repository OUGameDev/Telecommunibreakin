using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class ButtonScript : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectTouched += EMPScript_InteractableObjectEventHandler;
    }

    private void EMPScript_InteractableObjectEventHandler(object sender, InteractableObjectEventArgs e)
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}