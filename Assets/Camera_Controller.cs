using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

	public Playercontroller player;

	public bool isFollowing;

	public float xOffset;

	public float yOffest;


	// Use this for initialization
	void Start()
	{
		player = FindObjectOfType<Playercontroller>();

		isFollowing = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (isFollowing)
		{
			transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffest, transform.position.z);
		}

	}
}
