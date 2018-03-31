using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAb : MonoBehaviour {

	[Header("Q ABILITY")]
	public bool qUse = true;
	public float qCooldown;
	public Transform slimeBall;
	public Transform slimeBallSpawn;

	[Header("E ABILITY")]
	public bool eUse = true;
	public float eCooldown;
	public float eDuration;
	public Transform slimeTrailSpawn;
	public Transform slimeTrail;
	bool canSpawnTrail = false;
	bool canUseE = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.Q) && qUse == true)
		{
			StartCoroutine(Qcooldown());
			qUse = false;
		}

		if (Input.GetKey(KeyCode.E) && eUse == true)
		{
			StartCoroutine(Ecooldown());
			eUse = false;
			canSpawnTrail = true;
			canUseE = false;
		}

		if (canSpawnTrail == true && eUse == false && canUseE == false)
		{
			StartCoroutine(Trail());
			canSpawnTrail = false;
		}
	}

	IEnumerator Qcooldown()
	{
		Instantiate (slimeBall, slimeBallSpawn.position, slimeBallSpawn.rotation);
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

	IEnumerator Ecooldown()
	{
		yield return new WaitForSeconds (eDuration);
		canUseE = true;
		canSpawnTrail = false;
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}

	IEnumerator Trail()
	{
		Instantiate (slimeTrail, slimeTrailSpawn.position, slimeTrailSpawn.rotation);
		yield return new WaitForSeconds (0.1f);
		canSpawnTrail = true;
	}
}
