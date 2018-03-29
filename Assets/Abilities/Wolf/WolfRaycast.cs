using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRaycast : MonoBehaviour {

	RacePlayer wolfScript;
	
	Ray myRay1;
	RaycastHit hit;
	public float speed;

	bool startTimer = true;
	bool lookat = false;
	bool doOnce = true;
	public GameObject Enemy;
	public GameObject Wolf;
	public Vector3 opgeslagenV3;

	[Header("E ABILITY")]
	public bool eUse = true;
	bool rayScanTest;
	public float eCooldown;
	public float eDuration;

	void Start () 
	{
		wolfScript = Wolf.gameObject.GetComponent<RacePlayer>();
	}

	void Update ()
	{
		
		transform.Rotate (new Vector3 (0, speed, 0));

		if (Input.GetKey(KeyCode.E) && eUse == true)
		{
			rayScanTest = true;
			StartCoroutine(Ecooldown());
			eUse = false;
		}

		if (startTimer == true && lookat == false)
			{
				StartCoroutine(Timer());
				startTimer = false;
			}

		if (lookat == true) 
		{
			transform.LookAt(Enemy.transform.position);
			transform.Rotate(new Vector3(-3.2f, 0, 0), Space.Self);
		}

		if (rayScanTest == true)
		{
			myRay1 = new Ray (transform.position, transform.forward);
			Debug.DrawRay (transform.position, transform.forward * 30f);
			if (Physics.Raycast(myRay1, out hit, 30f))
			{
				if (hit.collider.tag == "Player")
				{
					Enemy = hit.collider.gameObject;
					opgeslagenV3 = hit.collider.gameObject.transform.position;
					lookat = true;
					wolfScript.gainSpeedPerSecond = 0.05f;
					wolfScript.maxSpeed = 0.26f;
				}
			}
		}	
		if (Vector3.Distance(opgeslagenV3, transform.position) < 8) 
		{
			wolfScript.gainSpeedPerSecond = 0.018f;
			wolfScript.maxSpeed = 0.22f;
		}

	}

	IEnumerator Timer()
	{
		
	yield return new WaitForSecondsRealtime(2);
	speed = speed * -1;
	startTimer = true;

	}

	IEnumerator Ecooldown()
	{
		yield return new WaitForSeconds (eDuration);
		rayScanTest = false;
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}

}