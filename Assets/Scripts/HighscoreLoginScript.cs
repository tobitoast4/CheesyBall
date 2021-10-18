using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class HighscoreLoginScript : MonoBehaviour
{

    void Start(){
    
    }

    
    void Update(){
        if (Input.GetKey(KeyCode.Escape)) {
            back();
        }
    }

    public void back() {
        SceneManager.LoadScene(2);
    }

}
