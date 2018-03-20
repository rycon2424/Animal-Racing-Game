using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAb : MonoBehaviour {

	public Transform[] deerSpawns = new Transform[4];
	public RacePlayer rotateSpeed;

	[Header("Q ABILITY")]
	public float qCooldown;
	public float qDuration;
	public float newRotationSpeed;
	public bool qUse = true;

	[Header("E ABILITY")]
	public float eCooldown;
	public float EDuration;
	public bool eUse = true;

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
	}

	IEnumerator Qcooldown()
	{
		rotateSpeed.rotationSpeed = newRotationSpeed;
		yield return new WaitForSeconds (qDuration);
		rotateSpeed.rotationSpeed = 2;
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}
}
