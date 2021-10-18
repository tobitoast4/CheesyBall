using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour{

    private float length, startpos;
    public int multiplier = 1;
    public GameObject cam;
    public float parallaxEffect;


    void Start(){
        startpos = transform.position.x;
        length = multiplier * GetComponent<SpriteRenderer>().bounds.size.x;
    }



    void Update(){
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float dist = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if(temp > startpos + length) {
            startpos += length * multiplier;
        } else if(temp < startpos - length) {
            startpos -= length * multiplier;
        }
    }
}
