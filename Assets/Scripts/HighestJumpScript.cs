using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighestJumpScript : MonoBehaviour{

    public Transform player;
    public TextMeshProUGUI highestJump_overall_text;
    public TextMeshProUGUI highestJump_current_text;
    private float highestJump = 0.0f;
    private float highestJump_highscore;
    public GameObject highestJumpCrown;

    void Start(){
        highestJump_highscore = PlayerPrefs.GetFloat("highestJump_Highscore", 0);
        highestJump_overall_text.text = highestJump.ToString("0") + "m";
        highestJump_current_text.text = highestJump.ToString("0") + "m";
        highestJumpCrown.SetActive(false);
    }

    void Update(){
        float jump = player.position.y + 3.0f;
        if (jump > 0) {
            highestJump_current_text.text = jump.ToString("0") + "m";
        } else {
            highestJump_current_text.text = "0m";
        }
        if (jump > highestJump) {
            highestJump = jump;
            highestJump_overall_text.text = highestJump.ToString("0") + "m";
        }
        if (jump > highestJump_highscore) {
            PlayerPrefs.SetFloat("highestJump_Highscore", jump);
            highestJumpCrown.SetActive(true);
            highestJump_highscore = jump;
        }
    }
}
