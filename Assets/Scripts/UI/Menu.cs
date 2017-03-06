using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public bool ShowCursor = true;

	void Start () {
		Cursor.visible = ShowCursor;
	}
	
	public void LoadScene(String scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
