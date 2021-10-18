using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    private Rigidbody2D player;
    private Vector2 force;
    private static bool cf;

    void Start() {
        player = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        cf = CameraFollow.cameraFollow;
        if (cf) {
            if (Input.GetMouseButton(0)) {
                player.AddForce(force);
            }
            if (Input.GetMouseButton(1)) {
                player.AddForce(new Vector2(0, 100));
            }
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (!(touch.position.x < Screen.width / 4 && touch.position.y > Screen.height - (Screen.height / 4))) {
                    player.AddForce(force);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        force = new Vector2(75, 0);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        force = new Vector2(0, -225);
    }
}
