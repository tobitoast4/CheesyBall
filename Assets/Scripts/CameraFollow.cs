using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Transform target;
    public Transform minHeight;
    public Rigidbody2D player;
    public float smoothspeed = 0.0f;
    public Vector3 offset;

    public float targetZoom;

    public static bool cameraFollow;

    private float lastHeight;

    private void Start() {
        targetZoom = Camera.main.orthographicSize;
        lastHeight = -20;
        cameraFollow = true;
    }

    

    private void FixedUpdate() {
        
        if (player.velocity.x < 0) {
            cameraFollow = false;
        }

        if (player.velocity.y > 0) {
            lastHeight = player.position.y;
        }
        if(player.velocity.y < 0 && player.position.y > -1.4 && lastHeight < minHeight.position.y) {
            cameraFollow = false;
        }
        

        if (cameraFollow) {
            //offset = new Vector3(14, 10f, -1);
            Camera.main.orthographicSize = targetZoom;

            float y;
            y = target.position.y + 10;
            //if(target.position.y < 100) {
            //    y = target.position.y + 10;
            //} else {
            //    y = 110;
            //}


            offset = new Vector3(14 + y * 0.7f, 7f - y * 0.5f, -1);
            Camera.main.orthographicSize = Mathf.Lerp(targetZoom, targetZoom * y * 0.375f, smoothspeed);

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed * 5);
            transform.position = smoothedPosition;
        }

    }



}
