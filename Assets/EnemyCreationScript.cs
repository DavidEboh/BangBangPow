using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreationScript : MonoBehaviour 
{

	public GameObject enemyGo;
	public Sprite[] myImages;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("makeEnemyAction", 0, 1.5f);
	}

	//Creates the enemy in random positons
	void makeEnemyAction ()
	{
		GameObject newEnemyGo = (GameObject)Instantiate (enemyGo) as GameObject;
		float x = Random.Range (-13.0f, 13.0f);
		float y = -5.2f;
		float z = 4;

		newEnemyGo.transform.position = new Vector3 (x, y, z);
		newEnemyGo.GetComponent<SpriteRenderer> ().sprite = myImages [Random.Range (0, myImages.Length)];
		newEnemyGo.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 580);
	}


	// Update is called once per frame
	void Update () {
		
	}
}
