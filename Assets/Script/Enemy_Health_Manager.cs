using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health_Manager : MonoBehaviour
{
	public int enemyHealth;

	public GameObject deathEffect;

	public int pointsOnDeath;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (enemyHealth < 0)
		{

			//Instantiate(deathEffect, transform.position, transform.rotation);
			//Score_Manager.AddPoints(pointsOnDeath);
			Destroy(gameObject);
		}


	}
	public void giveDamage(int damageToGive)
	{
		enemyHealth -= damageToGive;
	}

}
