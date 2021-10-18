using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadHighscores : MonoBehaviour
{
    public TextMeshProUGUI highestJump;
    public TextMeshProUGUI longestJump;
    public TextMeshProUGUI totalDistance;

    void Start(){
        highestJump.text = PlayerPrefs.GetFloat("highestJump_Highscore", 0).ToString("0") + "m";
        longestJump.text = PlayerPrefs.GetFloat("longestJump_Highscore", 0).ToString("0") + "m";
        totalDistance.text = PlayerPrefs.GetFloat("totalDistanceTravelled", 0).ToString("0") + "m";
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void backToMenu() {
        SceneManager.LoadScene(0);
    }

    public void globalHighscores() {
        if(PlayerPrefs.GetString("username", "") == "") {
            SceneManager.LoadScene(5);
        } else {
            SceneManager.LoadScene(4);
        }
    }
}
