using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAb : MonoBehaviour {

	RacePlayer rabbitScript;

	[Header("Q ABILITY")]
	public bool qUse = true;
	public float qCooldown;
	public float qDuration;
	public GameObject hat;

	[Header("E ABILITY")]
	public bool eUse = true;
	public float eCooldown;
	public float eDuration;
	public float forceConst;
	private Rigidbody selfRigidbody;


	void Start () 
	{
		hat.SetActive (false);
		rabbitScript = this.gameObject.GetComponent<RacePlayer>();
		selfRigidbody = GetComponent<Rigidbody> ();
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
			selfRigidbody.AddForce (0, forceConst, 0, ForceMode.Impulse);
			StartCoroutine(Ecooldown());
			eUse = false;
		}
	}

	IEnumerator Qcooldown()
	{
		hat.SetActive(true);
		transform.localScale = new Vector3 (0.5f , 0.5f , 0.5f);
		rabbitScript.maxSpeed = 0.2f;
		yield return new WaitForSeconds (qDuration);
		rabbitScript.maxSpeed = 0.15f;
		transform.localScale = new Vector3 (1 , 1 , 1);
		hat.SetActive(false);
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

	IEnumerator Ecooldown()
	{
		
		yield return new WaitForSeconds (eDuration);

		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}

}
