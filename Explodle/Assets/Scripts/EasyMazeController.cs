using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMazeController : MonoBehaviour {
	public Rigidbody ball;
	public Transform ballPos;
	private float speed;
	private bool finished;
	public GameObject noLight;
	public GameObject finishedLight;
	private bool alreadyRun;
	public Code codeScript; 
	public AudioSource correct;

	// Use this for initialization
	void Start () {
		finished = false;
		alreadyRun = false;

		AudioSource correct = GetComponent<AudioSource> ();
		Code codeScript = GetComponent<Code> ();

		noLight.SetActive (true);
		finishedLight.SetActive (false);

		ball = GetComponent<Rigidbody> ();
		speed = 1.0f;

	}

	// Update is called once per frame
	void Update () {
		if (!finished) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 move = new Vector3 (moveHorizontal, moveVertical, 0.0f);

			ball.AddForce (move * speed);
		}

		if(transform.position.y > -0.35){
			ballPos.transform.position = new Vector3 (transform.position.x, -0.35f, transform.position.z);
		}

		if ((transform.position.x > -0.28) && (transform.position.y < -1.95)) {
			finished = true;
			speed = 0f;
			ballPos.transform.position = new Vector3 (-0.28f, -1.97f, transform.position.z);

			finishedLight.SetActive (true);
		}

		if (finished && !alreadyRun) {
			correct.Play ();
			alreadyRun = true;
			codeScript.AddToCode ();
		}


	}
}
