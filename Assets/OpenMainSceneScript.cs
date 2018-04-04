using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class OpenMainSceneScript : MonoBehaviour 
{
	/*public void GotoNextSceneAction (string GamePlay Scene)
	{
		SceneManager.LoadScene("GamePlay Scene");
	}*/
	public void GotoNextSceneAction ()
	{
		Application.LoadLevel ("GamePlay Scene");
	}
}