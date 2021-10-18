using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    
    public void play() {
        SceneManager.LoadScene(1);
    }
    public void highscores() {
        SceneManager.LoadScene(2);
    }

    public void getToMenu() {
        SceneManager.LoadScene(0);
    }

    public void getToHowToPlay() {
        SceneManager.LoadScene(3);
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

}
