using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LongestJump : MonoBehaviour{

    public TextMeshProUGUI longestJump_overall_text;
    public TextMeshProUGUI longestJump_current_text;
    public Rigidbody2D player;
    

    Vector3 start;
    bool playerIsTouchingGround;

    private float longestJump;
    private float longestJump_highscore;
    public GameObject longestJumpCrown;

    void Start(){
        player = GetComponent<Rigidbody2D>();
        playerIsTouchingGround = true;
        longestJump_highscore = PlayerPrefs.GetFloat("longestJump_Highscore", 0);
        longestJump = 0;
        longestJump_overall_text.text = longestJump.ToString("0") + "m";
        start = player.transform.position;
        longestJumpCrown.SetActive(false);
    }


    void Update(){
        Vector3 pos = player.transform.position;
        float jump = pos.x - start.x;

        if (playerIsTouchingGround) {
            longestJump_current_text.text = "0m";
        }
        else {
            longestJump_current_text.text = jump.ToString("0") + "m";
        }
        if (jump > longestJump) {
            longestJump = jump;
            longestJump_overall_text.text = jump.ToString("0") + "m";
        }
        if (jump > longestJump_highscore) {
            PlayerPrefs.SetFloat("longestJump_Highscore", jump);
            longestJumpCrown.SetActive(true);
            longestJump_highscore = jump;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        playerIsTouchingGround = true;
        Vector3 end = player.transform.position;
        float jump = end.x - start.x;

        if (jump > longestJump) {
            longestJump = jump;
            longestJump_overall_text.text = jump.ToString("0") + "m";
        }
        if (jump > longestJump_highscore) {
            PlayerPrefs.SetFloat("longestJump_Highscore", jump);
            longestJump_highscore = jump;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        playerIsTouchingGround = false;
        start = player.transform.position;
    }
}
