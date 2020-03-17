﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
	{
		SceneManager.LoadScene(1);
	}

	public void OnExit()
	{
		Application.Quit();
	}

	public void OnStart()
	{
		SceneManager.LoadScene(0);
	}
}
