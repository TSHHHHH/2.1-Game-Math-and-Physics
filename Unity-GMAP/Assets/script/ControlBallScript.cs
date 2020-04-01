using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBallScript : MonoBehaviour {

    LineRenderer lineRenderer;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();

       // lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
      //  lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }
	
	// Update is called once per frame
	void Update () {
        
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(0, Vector3.zero);
    }
}
