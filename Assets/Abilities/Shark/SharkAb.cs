using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAb : MonoBehaviour {

	[Header("Q ABILITY")]
	public bool qUse = true;
	public float qCooldown;
	public float qDuration;

	[Header("E ABILITY")]
	public bool eUse = true;
	public float eCooldown;
	public float eDuration;
	public Transform sharkBody;
	public GameObject undergroundCol;

	BoxCollider sharkE;

	void Start () 
	{
		sharkE = GetComponent<BoxCollider> ();
		undergroundCol.SetActive (false);
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
		transform.localScale = new Vector3 (2 , 2 , 2);
		yield return new WaitForSeconds (qDuration);
		transform.localScale = new Vector3 (1 , 1 , 1);
		yield return new WaitForSeconds (qCooldown);
		qUse = true;
	}

	IEnumerator Ecooldown()
	{
		sharkE.enabled = false;;
		sharkBody.transform.position = new Vector3 (sharkBody.transform.position.x,
		sharkBody.transform.position.y - 1.4f, sharkBody.transform.position.z);
		undergroundCol.SetActive (true);
		yield return new WaitForSeconds (eDuration);
		sharkBody.transform.position = new Vector3 (sharkBody.transform.position.x,
		sharkBody.transform.position.y + 1.4f, sharkBody.transform.position.z);
		sharkE.enabled = true;
		undergroundCol.SetActive (false);
		yield return new WaitForSeconds (eCooldown);
		eUse = true;
	}

}
