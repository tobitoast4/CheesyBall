using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRocks : MonoBehaviour{

    public Transform player;
    private static bool cf;

    void Start(){
        float newY = Random.Range(-9, -16);
        float rot = Random.Range(0, 360);
        float scale = Random.Range(4, 10);
        float newX = Random.Range(-20, 150);
        transform.rotation = Quaternion.Euler(Vector3.forward * rot);
        transform.localScale = new Vector3(scale, scale, 9);
        transform.position = new Vector3(newX, newY, 0);
    }


    void Update(){
        cf = CameraFollow.cameraFollow;
        if (cf) {
            if (player.transform.position.x - this.transform.position.x > 50) {
                float newY = Random.Range(-9, -16);
                float rot = Random.Range(0, 360);
                float scale = Random.Range(4, 10);
                float newX = 1.67f * player.position.y + 100 + player.position.x + Random.Range(0, 100);
                transform.rotation = Quaternion.Euler(Vector3.forward * rot);
                transform.localScale = new Vector3(scale, scale, 9);
                transform.position = new Vector3(newX, newY, 0);
            }
        }
    }
}
