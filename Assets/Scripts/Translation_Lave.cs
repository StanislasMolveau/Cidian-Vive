using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation_Lave : MonoBehaviour {

    public Vector2 animationSpeed = new Vector2(1.0f, 1.0f);
    public string nomTexture = "_MainTex";

    private Vector2 offsetUV = Vector2.zero;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        offsetUV += animationSpeed * Time.deltaTime;
        if (this.GetComponent<MeshRenderer>().enabled)
        {
            this.GetComponent<MeshRenderer>().material.SetTextureOffset(nomTexture, offsetUV);
        }
	}
}
