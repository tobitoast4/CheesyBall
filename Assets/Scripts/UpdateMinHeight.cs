using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMinHeight : MonoBehaviour
{
    public Transform player;
    private float height;

    void Start(){

    }

    void Update(){
        transform.position = new Vector2(player.position.x, transform.position.y);
        if (player.position.y > height && player.position.y > transform.position.y) {
            transform.position = new Vector2(0, Mathf.Lerp(transform.position.y, player.position.y * 2/3, 0.1f));
            if(transform.position.y > 50) {
                transform.localScale = new Vector2(player.position.y * 100, player.position.y * 0.001f);
            }
        }

        height = player.position.y;
    }
}
