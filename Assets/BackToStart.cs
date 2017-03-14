using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToStart : MonoBehaviour {

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
