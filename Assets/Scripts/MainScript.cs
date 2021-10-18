using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainScript : MonoBehaviour{

    private bool stopped = false;
    public TextMeshProUGUI gamePaused_text;

    public TextMeshProUGUI highestJump_overall_text;
    public TextMeshProUGUI longestJump_overall_text;
    public TextMeshProUGUI highestJump_overall_gameover_text;
    public TextMeshProUGUI longestJump_overall_gameover_text;

    public GameObject gameoverOverlay;
    public Transform player;

    private static bool cf;

    void Update(){
        cf = CameraFollow.cameraFollow;
        if (!cf) {
            highestJump_overall_gameover_text.text = "Longest jump: " + highestJump_overall_text.text;
            longestJump_overall_gameover_text.text = "Highest jump: " + longestJump_overall_text.text;
            gameoverOverlay.SetActive(true);
        } else {
            gameoverOverlay.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Escape)) {
            PlayerPrefs.SetFloat("totalDistanceTravelled", PlayerPrefs.GetFloat("totalDistanceTravelled", 0) + player.position.x + 14.12f);
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
            
    }

    public void pause() {
        if (stopped) {
            ResumeGame();
            stopped = false;
        } else {
            PauseGame();
            stopped = true;
        }
    }

    void PauseGame() {
        Time.timeScale = 0;
        gamePaused_text.SetText("<<< Game paused >>>");
    }

    void ResumeGame() {
        Time.timeScale = 1;
        gamePaused_text.SetText("");
    }

    public void getToMenu() {
        PlayerPrefs.SetFloat("totalDistanceTravelled", PlayerPrefs.GetFloat("totalDistanceTravelled", 0) + player.position.x + 14.12f);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void restart() {
        PlayerPrefs.SetFloat("totalDistanceTravelled", PlayerPrefs.GetFloat("totalDistanceTravelled", 0) + player.position.x + 14.12f);
        CameraFollow.cameraFollow = true;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
