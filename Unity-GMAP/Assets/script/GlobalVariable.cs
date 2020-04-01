using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour {

    //Physics
    public static float VELOCITY_THRESHOLD = 10f;
    public static float PHYSICS_FRICTION_SPEED_REDUCTION = 5f;
    public static float PHYSICS_FRICTION = 0.992f;
    public static float COR_WALL = 0.9f;
    public static float COR_BALL = 0.9f;

    // Screen
    public static int SCREEN_WIDTH = 900;
    public static int SCREEN_HEIGHT = 500;
    public static float BOARD_X_POSITION = 0.0f;
    public static float BOARD_Y_POSITION = 0.0f;

    // Gameplay
    public static float SHOULDER_WIDTH = 30f;
    public static float HOLE_SIZE = 30f;
    public static float BALL_SIZE = 20f;
    public static int NUM_BALLS = 9;

    public static float findDistance(HVector2D p1, HVector2D p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p1.x - p2.x, 2) + Mathf.Pow(p1.y - p2.y, 2));
    }
}
