using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public  GameObject prompt;
    public int scene;

    private void OnTriggerStay(Collider other)
    {
        prompt.SetActive(true); //the prompt appears upon entering the trigger
        if (Input.GetKeyDown(KeyCode.Space)) //press space to go to the next scene
        {
            changeScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        prompt.SetActive(false); //when you exit the trigger you cannot see the prompt
    }

    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }

}
