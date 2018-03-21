using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAb : MonoBehaviour {

	public Transform[] deerSpawns = new Transform[4];
	RacePlayer rotateSpeed;

	[Header("Q ABILITY")]
	public float qCooldown;
	public float qDuration;
	public float newRotationSpeed;
	public bool qUse = true;

	[Header("E ABILITY")]
	public float eCooldown;
	public bool eUse = true;
	public Transform deerClone;

	void Start () 
	{
		rotateSpeed = this.gameObject.GetComponent<RacePlayer>();
	}

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
		}
	}

	IEnumerator Qcooldown()
	{
		rotateSpeed.rotationSpeed = newRotationSpeed;
		yield return new WaitForSeconds (qDuration);
		rotateSpeed.rotationSpeed = 2;
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

	IEnumerator Ecooldown()
	{
		spawnDeers ();
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}

	public void spawnDeers()
	{
		for (int i = 0; i < 4; i++) 
		{
			Instantiate(deerClone, 
			deerSpawns[i].transform.position,
			deerSpawns[i].transform.rotation);
		}
	}
}
