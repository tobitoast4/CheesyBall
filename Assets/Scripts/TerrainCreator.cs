using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreator : MonoBehaviour{

    public SpriteShapeController shape;
    public Transform player;
 
    private int amountPoints;


    float distanceBtwnPoints1 = 7;
    float distanceDeviation1 = 0.4f;
    float avgAmplitude1 = 1.7f;
    float avgAmplitudeDeviation1 = 1.0f;
    float avgTangentLength1 = 2.4f;
    float avgTangentDeviation1 = 0.5f;

    float distanceBtwnPoints2 = 5;
    float distanceDeviation2 = 0.4f;
    float avgAmplitude2 = 1.3f;
    float avgAmplitudeDeviation2 = 0.4f;
    float avgTangentLength2 = 2.0f;
    float avgTangentDeviation2 = 0.5f;
    float verticalShift2 = 1.0f;

    float distanceBtwnPoints3 = 5;
    float distanceDeviation3 = 0.4f;
    float avgAmplitude3 = 1.5f;
    float avgAmplitudeDeviation3 = 0.4f;
    float avgTangentLength3 = 2.0f;
    float avgTangentDeviation3 = 0.5f;

    float distanceBtwnPoints4 = 10;
    float distanceDeviation4 = 0.4f;
    float avgAmplitude4 = 2.2f;
    float avgAmplitudeDeviation4 = 1.2f;
    float avgTangentLength4 = 3.5f;
    float avgTangentDeviation4 = 0.5f;



    void Start(){
        shape = GetComponent<SpriteShapeController>();
        amountPoints = 4;
        float distanceBtwnPoints = 6;

        shape.spline.SetPosition(1, new Vector3(shape.spline.GetPosition(1).x * 20, shape.spline.GetPosition(2).y, 0));
        shape.spline.SetPosition(0, new Vector3(shape.spline.GetPosition(1).x, shape.spline.GetPosition(0).y, 0));

        shape.spline.SetPosition(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(distanceBtwnPoints * 2, 0, 0));
        shape.spline.SetPosition(amountPoints - 1, shape.spline.GetPosition(amountPoints - 1) + new Vector3(distanceBtwnPoints * 2, 0, 0));

        shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) - new Vector3(distanceBtwnPoints * 2, 0, 0));
        shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
        shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-3.5f, 0, 0));
        shape.spline.SetRightTangent(amountPoints - 2, new Vector3(3.5f, 0, 0));
        amountPoints++;
        shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) - new Vector3(distanceBtwnPoints * 1, 2.0f, 0));
        shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
        shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-2.2f, 0, 0));
        shape.spline.SetRightTangent(amountPoints - 2, new Vector3(3.5f, 0, 0));
        amountPoints++;

        StartCoroutine(waiter());
    }

    IEnumerator waiter() {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(10);

        while(player.position.x - shape.spline.GetPosition(2).x > 50) {
            float x = shape.spline.GetPosition(2).x;
            shape.spline.RemovePointAt(2);
            shape.spline.SetPosition(1, new Vector3(x, shape.spline.GetPosition(1).y, shape.spline.GetPosition(1).z));
            shape.spline.SetPosition(0, new Vector3(x, shape.spline.GetPosition(0).y, shape.spline.GetPosition(0).z));
            amountPoints--;
        }

        StartCoroutine(waiter());
    }

    void Update() {
        if (shape.spline.GetPosition(amountPoints - 2).x - player.position.x < 1.67f * player.position.y + 90) {
            generateTerrain();
        }
    }

    public void generateTerrain() {
        int random = Random.Range(1, 100);
        int amount = Random.Range(1, 3);
        if(random > 80) {
            generateTerrain1(amount*2);
        } else if (random > 60) {
            generateTerrain4(amount*2);
        } else if (random > 30) {
            generateTerrain2(4);
            generateTerrain1(amount * 2);
        } else {
            generateTerrain3(2);
            generateTerrain1(amount * 2);
        }
    }

    //normales Terrain
    public void generateTerrain1(int amountPointsToCreate) {
        shape.spline.SetPosition(amountPoints-2, shape.spline.GetPosition(amountPoints-2) + new Vector3(distanceBtwnPoints1* amountPointsToCreate, 0, 0));
        shape.spline.SetPosition(amountPoints-1, shape.spline.GetPosition(amountPoints-1) + new Vector3(distanceBtwnPoints1* amountPointsToCreate, 0, 0));

        for(int i = 0; i < amountPointsToCreate; i++) {
            if (i % 2 == 0) {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints1 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation1, distanceDeviation1), (avgAmplitude1 + Random.Range(-avgAmplitudeDeviation1, avgAmplitudeDeviation1)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength1 + Random.Range(-avgTangentDeviation1, avgTangentDeviation1), 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength1 + Random.Range(-avgTangentDeviation1, avgTangentDeviation1), 0, 0));
                amountPoints++;
            } else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints1 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation1, distanceDeviation1), -(avgAmplitude1 + Random.Range(-avgAmplitudeDeviation1, avgAmplitudeDeviation1)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength1 + Random.Range(-avgTangentDeviation1, avgTangentDeviation1), 0, 0));
                if( i == amountPointsToCreate - 1) {
                    shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength1 + 0.5f + Random.Range(-avgTangentDeviation1, avgTangentDeviation1), 0, 0));
                } else {
                    shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength1 + Random.Range(-avgTangentDeviation1, avgTangentDeviation1), 0, 0));
                }
                amountPoints++;
            }
            
        }
        
    }

    //mehrere Spitzen
    public void generateTerrain2(int amountPointsToCreate) {
        shape.spline.SetPosition(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(distanceBtwnPoints2 * amountPointsToCreate, 0, 0));
        shape.spline.SetPosition(amountPoints - 1, shape.spline.GetPosition(amountPoints - 1) + new Vector3(distanceBtwnPoints2 * amountPointsToCreate, 0, 0));

        for (int i = 0; i < amountPointsToCreate; i++) {
            if (i % 2 == 0) {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints2 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation2, distanceDeviation2), verticalShift2 + (avgAmplitude2 + Random.Range(-avgAmplitudeDeviation2, avgAmplitudeDeviation2)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Broken);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-1.0f, -1.5f, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(1.0f, -1.5f, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints2 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation2, distanceDeviation2), verticalShift2 - (avgAmplitude2 + Random.Range(-avgAmplitudeDeviation2, avgAmplitudeDeviation2)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength2 + Random.Range(-avgTangentDeviation2, avgTangentDeviation2), 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength2 + Random.Range(-avgTangentDeviation2, avgTangentDeviation2), 0, 0));
                amountPoints++;
            }

        }

    }

    //eine Spitze
    public void generateTerrain3(int amountPointsToCreate) {
        shape.spline.SetPosition(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(distanceBtwnPoints3 * amountPointsToCreate, 0, 0));
        shape.spline.SetPosition(amountPoints - 1, shape.spline.GetPosition(amountPoints - 1) + new Vector3(distanceBtwnPoints3 * amountPointsToCreate, 0, 0));

        for (int i = 0; i < amountPointsToCreate; i++) {
            if (i % 2 == 0) {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints3 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation3, distanceDeviation3) - 1.0f, (avgAmplitude3 + Random.Range(-avgAmplitudeDeviation3, avgAmplitudeDeviation3)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Broken);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-1.0f, -1.5f, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(1.0f, -1.5f, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints3 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation3, distanceDeviation3), -(avgAmplitude3 + Random.Range(-avgAmplitudeDeviation3, avgAmplitudeDeviation3)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength3 + Random.Range(-avgTangentDeviation3, avgTangentDeviation3), 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength3 + 0.5f + Random.Range(-avgTangentDeviation3, avgTangentDeviation3), 0, 0));
                amountPoints++;
            }

        }

    }

    //normales langes Terrain
    public void generateTerrain4(int amountPointsToCreate) {
        shape.spline.SetPosition(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(distanceBtwnPoints4 * amountPointsToCreate, 0, 0));
        shape.spline.SetPosition(amountPoints - 1, shape.spline.GetPosition(amountPoints - 1) + new Vector3(distanceBtwnPoints4 * amountPointsToCreate, 0, 0));

        for (int i = 0; i < amountPointsToCreate; i++) {
            if (i % 2 == 0) {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints4 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation4, distanceDeviation4), (avgAmplitude4 + Random.Range(-avgAmplitudeDeviation4, avgAmplitudeDeviation4)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength4 + Random.Range(-avgTangentDeviation4, avgTangentDeviation4), 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength4 + Random.Range(-avgTangentDeviation4, avgTangentDeviation4), 0, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints4 * (amountPointsToCreate - i) + Random.Range(-distanceDeviation4, distanceDeviation4), -(avgAmplitude4 + Random.Range(-avgAmplitudeDeviation4, avgAmplitudeDeviation4)), 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength4 + Random.Range(-avgTangentDeviation4, avgTangentDeviation4), 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength4 + Random.Range(-avgTangentDeviation4, avgTangentDeviation4), 0, 0));
                amountPoints++;
            }

        }

    }
}
