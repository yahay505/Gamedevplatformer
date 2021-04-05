using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health_Manager : MonoBehaviour
{
	public static int playerHealth;
	public int maxPlayerHealth;

	public bool isDead;

	public Slider healthBar;

	//Text text;

	private Level_Manager levelManager;

	private Life_Manager lifeSystem;

	// Use this for initialization
	void Start()
	{

		//text = GetComponent<Text>();

		healthBar = GetComponent<Slider>();

		playerHealth = maxPlayerHealth;

		levelManager = FindObjectOfType<Level_Manager>();
		isDead = false;

		lifeSystem = FindObjectOfType<Life_Manager>();
	}

	// Update is called once per frame
	void Update()
	{

		if (playerHealth <= 0 && !isDead)
		{
			levelManager.RespawnPlayer();
			isDead = true;
			playerHealth = 0;
			lifeSystem.TakeLife();
		}
		//text.text = "" + playerHealth;
		healthBar.value = playerHealth;

		if (playerHealth > maxPlayerHealth)
		{
			playerHealth = maxPlayerHealth;
		}
	}

	public static void hurtPlayer(int damageToGive)
	{
		playerHealth -= damageToGive;
	}

	public void fullHealth()
	{
		playerHealth = maxPlayerHealth;
	}
}