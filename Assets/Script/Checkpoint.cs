﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

	public Level_Manager level_manager;

	// Use this for initialization
	void Start()
	{
		level_manager = FindObjectOfType<Level_Manager>();
	}


	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			level_manager.currentCheckpoint = gameObject;
			Debug.Log(("Activated Checkpoint") + transform.position);
		}
	}
}
