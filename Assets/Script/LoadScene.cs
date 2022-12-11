using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] protected string loadSceneName;

    void OnMouseDown()
    {
        SceneManager.LoadScene(loadSceneName);
        if (this.tag == "Levels")
        {
            Destroy(GameObject.FindGameObjectWithTag("Song"));
        }

    }
}
