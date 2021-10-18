using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreator3 : MonoBehaviour{
    public SpriteShapeController shape;
    public Transform player;

    private static bool cf;

    private int amountPoints;

    float distanceBtwnPoints1 = 7;
    float avgAmplitude1 = 1.7f;
    float avgTangentLength1 = 2.4f;

    float distanceBtwnPoints2 = 5;
    float avgAmplitude2 = 1.3f;
    float avgTangentLength2 = 2.0f;
    float verticalShift2 = 1.0f;

    float distanceBtwnPoints3 = 5;
    float avgAmplitude3 = 1.5f;
    float avgTangentLength3 = 2.0f;

    float distanceBtwnPoints4 = 10;
    float avgAmplitude4 = 2.2f;
    float avgTangentLength4 = 3.5f;



    void Start() {
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
        yield return new WaitForSecondsRealtime(60);

        cf = CameraFollow.cameraFollow;
        if (cf) {
            while (player.position.x - shape.spline.GetPosition(2).x > 100) {
                float x = shape.spline.GetPosition(2).x;
                shape.spline.RemovePointAt(2);
                shape.spline.SetPosition(1, new Vector3(x, shape.spline.GetPosition(1).y, shape.spline.GetPosition(1).z));
                shape.spline.SetPosition(0, new Vector3(x, shape.spline.GetPosition(0).y, shape.spline.GetPosition(0).z));
                amountPoints--;
            }
        }

        StartCoroutine(waiter());
    }

    void Update() {
        cf = CameraFollow.cameraFollow;
        if (cf) {
            if (shape.spline.GetPosition(amountPoints - 2).x - player.position.x < 1.7f * player.position.y + 90) {
                for (int i = 0; i < 70; i++) {
                    generateTerrain();
                }
            }
        }
    }


    public void generateTerrain() {
        int random = Random.Range(1, 100);
        int amount = Random.Range(1, 3);
        if (random > 80) {
            generateTerrain1(amount * 2);
        }
        else if (random > 60) {
            generateTerrain4(amount * 2);
        }
        else if (random > 30) {
            generateTerrain2(4);
            generateTerrain1(amount * 2);
        }
        else {
            generateTerrain3(2);
            generateTerrain1(amount * 2);
        }
    }

    //normales Terrain
    public void generateTerrain1(int amountPointsToCreate) {
        shape.spline.SetPosition(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(distanceBtwnPoints1 * amountPointsToCreate, 0, 0));
        shape.spline.SetPosition(amountPoints - 1, shape.spline.GetPosition(amountPoints - 1) + new Vector3(distanceBtwnPoints1 * amountPointsToCreate, 0, 0));

        for (int i = 0; i < amountPointsToCreate; i++) {
            if (i % 2 == 0) {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints1 * (amountPointsToCreate - i), avgAmplitude1, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength1, 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength1, 0, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints1 * (amountPointsToCreate - i), -avgAmplitude1, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength1, 0, 0));
                if (i == amountPointsToCreate - 1) {
                    shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength1 + 0.5f, 0, 0));
                }
                else {
                    shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength1, 0, 0));
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
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints2 * (amountPointsToCreate - i), verticalShift2 + avgAmplitude2, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Broken);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-1.0f, -1.5f, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(1.0f, -1.5f, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints2 * (amountPointsToCreate - i), verticalShift2 - avgAmplitude2, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength2, 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength2, 0, 0));
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
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints3 * (amountPointsToCreate - i) - 1.0f, avgAmplitude3, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Broken);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-1.0f, -1.5f, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(1.0f, -1.5f, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints3 * (amountPointsToCreate - i), -avgAmplitude3, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength3, 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength3 + 0.5f, 0, 0));
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
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints4 * (amountPointsToCreate - i), avgAmplitude4, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength4, 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength4, 0, 0));
                amountPoints++;
            }
            else {
                shape.spline.InsertPointAt(amountPoints - 2, shape.spline.GetPosition(amountPoints - 2) + new Vector3(-distanceBtwnPoints4 * (amountPointsToCreate - i), -avgAmplitude4, 0));
                shape.spline.SetTangentMode(amountPoints - 2, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(amountPoints - 2, new Vector3(-avgTangentLength4, 0, 0));
                shape.spline.SetRightTangent(amountPoints - 2, new Vector3(avgTangentLength4, 0, 0));
                amountPoints++;
            }

        }

    }
}
