using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayScript : MonoBehaviour 
{
	public GameObject gameOverPanel;

	public ParticleSystem muzzleFlashParticle;

	//--------------------------------Sounds
	AudioSource gunShotAudioSource;
	public AudioClip gunShotClip;
	//--------------------------------Text
	public GameObject timerTextView;
	public GameObject scoreTextView;
	int roundsShot = 0;
	int enemyHit = 0;
	//--------------------------------Weapons
	public GameObject bazookaGo;
	public GameObject crossHairGo;
	//--------------------------------Time
	bool gameIsPaused = false;
	int gameTime = 30;
	int timeMinutes;
	int timeSeconds;

	// Use this for initialization
	void Start () 
	{
		scoreTextView.GetComponent<Text> ().text = enemyHit.ToString () + " / " + roundsShot.ToString ();

		gunShotAudioSource = gameObject.GetComponent<AudioSource> ();
		//Once the game starts, after 1 second, go into timeCounterAction and then cycle.
		InvokeRepeating ("timeCounterAction", 0, 1);
	}
		

	void timeCounterAction ()
	{
		if (gameTime >= 0) 
		{
			//int is in brackets to cast the value into gameTime
			timeMinutes = (int)(gameTime / 60);
			timeSeconds = gameTime % 60;

			//D1 = only 1 digit. D2 = at least 2 digits
			string timerTextString = timeMinutes.ToString ("D1") + "\'" + timeSeconds.ToString ("D2") + "\"";
			timerTextView.GetComponent<Text> ().text = timerTextString;
			gameTime = gameTime - 1;
		} 
		else 
		{
			//Game Over
			Time.timeScale = 0;
			gameIsPaused = true;
			gameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
			gameOverPanel.GetComponent<CanvasGroup> ().interactable = true;
			gameOverPanel.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}
	}

	void shootAction()
	{
		gunShotAudioSource.PlayOneShot (gunShotClip);
		muzzleFlashParticle.Emit (1);

		roundsShot++;


		//sends ray from camera point in the chosen direction and determine if there is a git
		Vector2 dir = new Vector2 (crossHairGo.transform.position.x, crossHairGo.transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.transform.position, dir);

		// enemy is hit
		//if hit collider is not null = enemy is hit
		if (hit.collider != null && hit.collider.gameObject != crossHairGo) 
		{
			enemyHit++;
			Destroy (hit.collider.gameObject);
		}

		scoreTextView.GetComponent<Text> ().text = enemyHit.ToString () + " / " + roundsShot.ToString ();

	}

	// Update is called once per frame
	void Update () 
	{
		if (!gameIsPaused) 
		{

			if (Input.GetKeyUp (KeyCode.Space)) {
				shootAction ();
			}

			bazookaGo.transform.LookAt (crossHairGo.transform);

			float horz = Input.GetAxis ("Mouse X");
			float vert = Input.GetAxis ("Mouse Y");

			//In every frame, make a new vector 3 and move in axis x and y but nothing in z. Apply it to crosshair movement
			Vector3 crossHairMove = new Vector3 (horz, vert, 0);
			crossHairGo.transform.Translate (crossHairMove);

			//Checks screen boundaries
			float x = crossHairGo.transform.position.x;
			float y = crossHairGo.transform.position.y;

			if (x > 9)
				x = 9;
			if (x < -9)
				x = -9;

			if (y > 5)
				y = 5;
			if (y < -5)
				y = -5;

			crossHairGo.transform.position = new Vector3 (x, y, 0);
		}
	}

	public void RestartAction()
	{
		//This restarts the game
		Time.timeScale = 1;
		gameIsPaused = false;
		gameOverPanel.GetComponent<CanvasGroup>().alpha = 0;
		gameOverPanel.GetComponent<CanvasGroup> ().interactable = false;
		gameOverPanel.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		gameTime = 5;
		enemyHit = 0;
		roundsShot = 0;
		scoreTextView.GetComponent<Text> ().text = enemyHit.ToString () + " / " + roundsShot.ToString ();
		timeCounterAction ();
	}
}
