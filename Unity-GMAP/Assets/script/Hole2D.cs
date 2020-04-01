using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole2D : MonoBehaviour {

    public HVector2D mPos = new HVector2D(0,0);
    public float mRadius;

    // Use this for initialization
    void Start () {
        mPos.x = transform.position.x;
        mPos.y = transform.position.y;

        mRadius = GlobalVariable.HOLE_SIZE/2;
    }
	
	// Update is called once per frame
	void Update () {
    }
}
