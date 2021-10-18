using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBackground : MonoBehaviour{
    
    public Transform player;
    private static bool cf;

    void Start(){
        
    }

    void Update(){
        cf = CameraFollow.cameraFollow;
        if (cf) {
            if (player.transform.position.y > 200) {
                transform.position = new Vector3(transform.position.x, 3.5f - (player.transform.position.y - 200) * 0.7f, -1);
            }
        }
        
        //if (player.transform.position.y > 250) {
        //    transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        //} else {
        //    transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        //    transform.position = new Vector3(transform.position.x, -player.transform.position.y*0.01f, -1);
        //}
    }
}
