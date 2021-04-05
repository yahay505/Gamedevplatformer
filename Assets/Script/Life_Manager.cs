﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life_Manager : MonoBehaviour
{

	public int startingLives;

	private int lifeCounter;

	private Text theText;

	public GameObject gameOverScreen;

	public Playercontroller player;

	public string mainMenu;

	public float waitAfter;

	// Use this for initialization
	void Start()
	{
		theText = GetComponent<Text>();

		lifeCounter = startingLives;

		player = FindObjectOfType<Playercontroller>();
	}

	// Update is called once per frame
	void Update()
	{

		theText.text = "x" + lifeCounter;

		if (lifeCounter == 0)
		{
			gameOverScreen.SetActive(true);
			player.gameObject.SetActive(false);
		}

		if (gameOverScreen.activeSelf)
		{
			waitAfter -= Time.deltaTime;
		}

		if (waitAfter < 0)
		{
			SceneManager.LoadScene(mainMenu);
		}
	}

	public void GiveLife()
	{
		lifeCounter++;
	}

	public void TakeLife()
	{
		lifeCounter--;
	}
}