using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table2D : MonoBehaviour {

    public List<GameObject> balls;
    public List<GameObject> holes;
    
    public HVector2D mPos = new HVector2D();
    public HVector2D mPos2 = new HVector2D();

    private Vector2 tempVec;
    private Vector2 tempVec2;

    public GameObject gameOver;
    public AudioClip hitSound;

    // Use this for initialization
    void Start () {
        tempVec = new Vector2();

        gameOver.SetActive(false);

       for (int i = 0; i < balls.Count; i++)
        {
            // Create one new ball
            GameObject b = balls[i];
            Ball2D ball = b.GetComponent<Ball2D>();

            // Make sure this ball spawns at an empty spot
            do
            {
                tempVec = b.transform.position;
     
                //tempVec.x = Random.Range(0, GlobalVariable.SCREEN_WIDTH - 2 * (GlobalVariable.SHOULDER_WIDTH + GlobalVariable.BALL_SIZE)) * (GlobalVariable.SCREEN_WIDTH - 2 * (GlobalVariable.SHOULDER_WIDTH + GlobalVariable.BALL_SIZE)) + GlobalVariable.SHOULDER_WIDTH + GlobalVariable.BALL_SIZE;
                //tempVec.y = Random.Range(0, 500) * (GlobalVariable.SCREEN_HEIGHT - 2 * (GlobalVariable.SHOULDER_WIDTH + GlobalVariable.BALL_SIZE)) + GlobalVariable.SHOULDER_WIDTH + GlobalVariable.BALL_SIZE;
                tempVec.x = Random.Range(ball.mRadius + GlobalVariable.SHOULDER_WIDTH, GlobalVariable.SCREEN_WIDTH - GlobalVariable.SHOULDER_WIDTH);
                tempVec.y = Random.Range(ball.mRadius + GlobalVariable.SHOULDER_WIDTH, GlobalVariable.SCREEN_HEIGHT - GlobalVariable.SHOULDER_WIDTH);

                b.transform.position = tempVec;
                ball.mPos.x = tempVec.x;
                ball.mPos.y = tempVec.y;

            } while (checkBallColli(ball));

        }
    }
	
	// Update is called once per frame
	void Update () {
		
        for(int i = 0; i < balls.Count; i++)
        {
            GameObject b1 = balls[i];
            Ball2D ball = b1.GetComponent<Ball2D>();

            ball.UpdatePhysics();

            for (int j = 0; j < balls.Count; j++)
            {
                GameObject b2 = balls[j];
                Ball2D ball2 = b2.GetComponent<Ball2D>();

                if(ball != ball2)
                {
                    if(ball.isCollidingWith(ball2))
                    {
                        handleBallColli(ball, ball2, Time.deltaTime);
                        AudioSource.PlayClipAtPoint(hitSound, transform.position);
                    }
                }
            }

            for (int k = 0; k < holes.Count; k++)
            {
                Hole2D hole = holes[k].GetComponent<Hole2D>();

                if (ball.isInside(hole))
                {
                    b1.SetActive(false);
                    if(b1.gameObject.tag == "Player")
                    {
                        gameOver.SetActive(true);
                        Time.timeScale = .25f;
                        Invoke("Reset", 1.0f);
                    }
                    break;
                }
            }
        }
	}

    bool checkBallColli(Ball2D toCheck)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            // Create one new ball
            GameObject b = balls[i];
            Ball2D ball = b.GetComponent<Ball2D>();

            if(ball.isCollidingWith(toCheck) && toCheck != ball)
            {
                return true;
            }
        }

        return false;
    }

    void handleBallColli(Ball2D ball1, Ball2D ball2, float elapsed)
    {
        if (ball1.isActiveAndEnabled && ball2.isActiveAndEnabled)
        {
            HVector2D ball1Pos = new HVector2D(ball1.transform.position.x, ball1.transform.position.y);
            HVector2D ball2Pos = new HVector2D(ball2.transform.position.x, ball2.transform.position.y);

            HVector2D distance = new HVector2D(ball1Pos.x - ball2Pos.x, ball1Pos.y - ball2Pos.y);

            HVector2D v1p = ball1.mVel.projection(distance);
            HVector2D v2p = ball2.mVel.projection(distance);

            HVector2D v1pp = (v2p * (ball2.mMass * 2.0f)) / (ball1.mMass + ball2.mMass);
            HVector2D v2pp = (v1p * (ball2.mMass * 2.0f)) / (ball1.mMass + ball2.mMass);

            ball1.mVel = ball1.mVel - v1p + v1pp;
            ball2.mVel = ball2.mVel - v2p + v2pp;

            float overlapDistance = ball1.mRadius * 2.0f - GlobalVariable.findDistance(ball1Pos, ball2Pos);
            if (overlapDistance > 0.0f)
            {
                adjustBallsDist(ball1, ball2, overlapDistance);
            }
        }
    }

    // If 2 balls are overlapping by this time, move them back to when they just collided
    void adjustBallsDist(Ball2D ball1, Ball2D ball2, float overlapDistance)
    {
        HVector2D vectorDifference = new HVector2D(ball1.transform.position.x - ball2.transform.position.x, ball1.transform.position.y - ball2.transform.position.y);

        vectorDifference.normalize();
        HVector2D vectorDifference2 = vectorDifference * -1.0f;

        float moveBackDistance = overlapDistance / 2.0f;
        
        mPos = new HVector2D(ball1.transform.position.x, ball1.transform.position.y);
        mPos2 = new HVector2D(ball2.transform.position.x, ball2.transform.position.y);
        
        HVector2D moveBall1 = new HVector2D(vectorDifference.x * moveBackDistance, vectorDifference.y * moveBackDistance);
        HVector2D moveBall2 = new HVector2D(vectorDifference2.x * moveBackDistance, vectorDifference2.y * moveBackDistance);

        mPos += moveBall1;
        mPos2 += moveBall2;

        tempVec.x = mPos.x;
        tempVec.y = mPos.y;
        tempVec2.x = mPos2.x;
        tempVec2.y = mPos2.y;

        if(ball1.mVel.x > 0.0f || ball1.mVel.y > 0.0f)
        {
            ball1.transform.position = tempVec;
        }
        if (ball2.mVel.x > 0.0f || ball2.mVel.y > 0.0f)
        {
            ball2.transform.position = tempVec2;
        }
    }

    void Reset()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
