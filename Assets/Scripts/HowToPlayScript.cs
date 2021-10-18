using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayScript : MonoBehaviour{
    
    void Start(){
        
    }

   
    void Update(){
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void backToMenu() {
        SceneManager.LoadScene(0);
    }
}
