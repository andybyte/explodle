using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplicatedMazeController : MonoBehaviour {
	public Rigidbody ball;
	public Transform ballPos;
	private float speed;
	private bool finished;
	//public GameObject noLight;
	//public GameObject finishedLight;
	public Material blueMat;
	public Material redMat;
	public Material greenMat;
	public Material yellowMat;
	private int matNum;
	private bool alreadyRun;
	public Code codeScript; 
	public AudioSource correct;


	// Use this for initialization
	void Start () {
		Code codeScript = GetComponent<Code> ();
		AudioSource correct = GetComponent<AudioSource> ();

		finished = false;
		alreadyRun = false;

		//noLight.SetActive (true);
		//finishedLight.SetActive (false);

		ball = GetComponent<Rigidbody> ();
		speed = 0.5f;

		Renderer rend = GetComponent<Renderer> ();
		matNum = Random.Range (0, 3);

		switch (matNum) {
		case 3:
			rend.material = yellowMat;
			break;
		case 2: 
			rend.material = greenMat;
			break;
		case 1:
			rend.material = redMat;
			break;
		case 0: 
			rend.material = blueMat;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!finished) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 move = new Vector3 (moveHorizontal, moveVertical, 0.0f);

			ball.AddForce (move * speed);
		}

		if ((transform.position.x < -2.20) && (transform.position.y < -2.18)) {//blue endzone
			if (matNum == 0) {
				finished = true;
				speed = 0f;
				ballPos.transform.position = new Vector3 (-2.22f, -2.19f, transform.position.z);

				//finishedLight.SetActive (true);
			} else {
				ballPos.transform.position = new Vector3 (-2.17f, -2.18f, transform.position.z);
			}
		}

		if ((transform.position.x > -0.17) && (transform.position.y > -0.126)) {//red end zone
			if (matNum == 1) {
				finished = true;
				speed = 0f;
				ballPos.transform.position = new Vector3 (-0.15f, -0.1f, transform.position.z);

				//finishedLight.SetActive (true);
			} else {
				ballPos.transform.position = new Vector3 (-0.15f, -0.14f, transform.position.z);
			}
		}

		if ((transform.position.x < -2.19) && (transform.position.y > -0.15)) {//green end zone
			if (matNum == 2) {
				finished = true;
				speed = 0f;
				ballPos.transform.position = new Vector3 (-2.19f, -0.15f, transform.position.z);

				//finishedLight.SetActive (true);
			} else {
				ballPos.transform.position = new Vector3 (-2.17f, -0.15f, transform.position.z);
			}
		}

		if ((transform.position.x > -0.16) && (transform.position.y < -2.189)) {//yellow end zone
			if (matNum == 3) {
				finished = true;
				speed = 0f;
				ballPos.transform.position = new Vector3 (-0.14f, -2.19f, transform.position.z);

				//finishedLight.SetActive (true);
			} else {
				ballPos.transform.position = new Vector3 (-0.14f, -2.17f, transform.position.z);
			}
		}

		if (finished && !alreadyRun) {
			correct.Play ();
			alreadyRun = true;
			codeScript.AddToCode ();
		}

	}
}
