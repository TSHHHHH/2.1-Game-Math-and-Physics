using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour 
{
	public LineFactory lineFactory;

	private Vector2 start;
	private Line drawnLine;
    public GameObject drawnObject;

    private Ball2D ball;

    private Line drawnLine2;
    public float mRadius;
    public AudioClip hitSound;

    private void Start()
    {
        ball = drawnObject.GetComponent<Ball2D>();

        mRadius = GlobalVariable.BALL_SIZE / 2;
    }
    void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			var pos = Camera.main.ScreenToWorldPoint (Input.mousePosition); // Start line drawing
            if(ball != null && ball.isCollidingWith(pos.x,pos.y))
            {
                drawnLine = lineFactory.GetLine (pos, pos, 2.0f, Color.black);
                drawnLine.EnableDrawing(true);

                drawnLine2 = lineFactory.GetLine (pos, pos, 2.0f, Color.red);
                drawnLine2.EnableDrawing(true);

                
            }
			
		} else if (Input.GetMouseButtonUp (0) && drawnLine != null) {
            Vector3 tableCenter = new Vector3(GlobalVariable.SCREEN_WIDTH / 2.0f, GlobalVariable.SCREEN_HEIGHT / 2.0f,0);

            AudioSource.PlayClipAtPoint(hitSound, tableCenter);

            drawnLine.EnableDrawing(false);
            drawnLine2.EnableDrawing(false);
            
            //update the vel of the white ball.
            HVector2D v = new HVector2D(drawnLine.start.x - drawnLine.end.x, drawnLine.start.y - drawnLine.end.y);
          //  v.normalize();

            ball.mVel = v * 3.0f;
            drawnLine = null; // End line drawing

            drawnLine2 = null;
        }

		if (drawnLine != null) {
			drawnLine.end = Camera.main.ScreenToWorldPoint (Input.mousePosition); // Update line end
		}

        if(drawnLine2 != null)
        {
            Vector2 v = new Vector2(drawnLine.start.x - drawnLine.end.x, drawnLine.start.y - drawnLine.end.y);
            Vector2 currentPos = new Vector2(drawnLine2.start.x, drawnLine2.start.y);
            drawnLine2.end = currentPos - v * -1.0f * 3.0f * (1.0f - GlobalVariable.PHYSICS_FRICTION * Time.deltaTime); ;
        }

        
	}

	/// <summary>
	/// Get a list of active lines and deactivates them.
	/// </summary>
	public void Clear()
	{
		var activeLines = lineFactory.GetActive ();

		foreach (var line in activeLines) {
			line.gameObject.SetActive(false);
		}
	}

}
