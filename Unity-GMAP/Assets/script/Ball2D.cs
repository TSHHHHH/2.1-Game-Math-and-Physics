using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2D : MonoBehaviour {

    public float mMass = 1.0f;
    public float mNumBalls = 0;
    public float mRadius;
    public HVector2D mVel;
    public  HVector2D mPos = new HVector2D(0,0);

    // temp usage
    private HMatrix2D matrix = new HMatrix2D();
    private Vector2 tempPos = new Vector2();

    public AudioClip hitSound;

    // Use this for initialization
    void Start () {
        mVel = new HVector2D(0,0);

        mRadius = GlobalVariable.BALL_SIZE / 2;
    }

    public bool isCollidingWith(float x, float y)
    {
        HVector2D thisBall = new HVector2D(transform.position.x, transform.position.y);
        HVector2D point = new HVector2D(x, y);
        return GlobalVariable.findDistance(thisBall, point) <= mRadius;
    }

    public bool isCollidingWith(Ball2D other)
    {
        HVector2D thisBall = new HVector2D(transform.position.x, transform.position.y);
        HVector2D thatBall = new HVector2D(other.transform.position.x, other.transform.position.y);
        return GlobalVariable.findDistance(thisBall,thatBall) <= mRadius * 2;
    }

    // Falls inside a hole
    public bool isInside(Hole2D hole)
    { 
        HVector2D thisBall = new HVector2D(transform.position.x, transform.position.y);
        HVector2D holePos = new HVector2D(hole.transform.position.x, hole.transform.position.y);
        return GlobalVariable.findDistance(thisBall, holePos) <= hole.mRadius;
    }

    public void UpdatePhysics()
    {
        updateBoundaryCollision(Time.deltaTime);
        updatePhysics(Time.deltaTime);
    }

    public bool updatePhysics(float elapsed)
    {
        // get the object position
        mPos.x = transform.position.x;
        mPos.y = transform.position.y;

        //-------------------------------------------

        mVel = mVel * (1.0f - GlobalVariable.PHYSICS_FRICTION * elapsed);

        float displacementX = mVel.x * elapsed;
        float displacementy = mVel.y * elapsed;
        
        matrix.setTranslationMat(displacementX, displacementy);
        mPos = matrix * mPos;

        /*Back up solution
          Vector2 displacement = new Vector2(displacementX, displacementy);
          mPos.x += displacement.x;
          mPos.y += displacement.y;*/

        //-----------------------------------------------
        tempPos.x = mPos.x;
        tempPos.y = mPos.y;

        transform.position = tempPos;
        // transform.position = new Vector2(transform.position.x + mVel.x, transform.position.y + mVel.y);
        return true;
    }

    void updateBoundaryCollision(float elapsed)
    {
        mPos.x = transform.position.x;
        mPos.y = transform.position.y;
        //------------------------------------------------------

        HVector2D tableCenter = new HVector2D(GlobalVariable.SCREEN_WIDTH / 2.0f, GlobalVariable.SCREEN_HEIGHT / 2.0f);
        Vector3 tableCenterV3 = new Vector3(tableCenter.x, tableCenter.y, 0);

        float moveAreaX = GlobalVariable.SCREEN_WIDTH / 2.0f - GlobalVariable.SHOULDER_WIDTH;
        float moveAreaY = GlobalVariable.SCREEN_HEIGHT / 2.0f - GlobalVariable.SHOULDER_WIDTH;

        if (transform.position.x >= tableCenter.x + moveAreaX - mRadius || transform.position.x <= tableCenter.x - moveAreaX + mRadius)
        {
            mVel.x = -mVel.x * (1.0f - 0.1f * elapsed);
            AudioSource.PlayClipAtPoint(hitSound, tableCenterV3);
        }

        if (transform.position.y >= tableCenter.y + moveAreaY - mRadius || transform.position.y <= tableCenter.y - moveAreaY + mRadius)
        {
            mVel.y = -mVel.y * (1.0f - 0.1f * elapsed);
            AudioSource.PlayClipAtPoint(hitSound, tableCenterV3);
        }

        //--------------------------------------------------------
        tempPos.x = mPos.x;
        tempPos.y = mPos.y;

        transform.position = tempPos;
    }
}
