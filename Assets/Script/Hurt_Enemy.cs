using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt_Enemy : MonoBehaviour
{
	public int damageToGive;
	public float bounceOnEnemy;
	private Rigidbody2D myRigibody;

	// Use this for initialization
	void Start()
	{
		myRigibody = transform.parent.parent.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "enemy")
		{
			other.GetComponent<Enemy_Health_Manager>().giveDamage(damageToGive);
			myRigibody.velocity = new Vector2(myRigibody.velocity.x, bounceOnEnemy);
			Debug.Log("hurt");

		}
	}
}
