using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public GameObject loadingScreen;

    public void loadScene(int level) {
        loadingScreen.SetActive(true);

        SceneManager.LoadScene(level);
    }

    public void QuitGame() {
        Application.Quit();
        return;
    }
}
