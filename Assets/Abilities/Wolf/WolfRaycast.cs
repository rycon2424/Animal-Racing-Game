using System.Collections										;
using System.Collections.Generic								;
using UnityEngine												;
public
class
WolfRaycast														:
MonoBehaviour 													{
Ray myRay														;
RaycastHit hit													;
public float speed												;
bool startTimer = true											;
void Update () 													{
transform.Rotate (new Vector3 (0, speed, 0))					;
if (startTimer == true											){
StartCoroutine(Timer())											;
startTimer = false												;}
myRay = new Ray (transform.position, transform.forward			);
Debug.DrawRay (transform.position, transform.forward * 30f		);
if (Physics.Raycast(myRay, out hit, 30f							)){
if (hit.collider.tag == "Player"								){
Debug.Log ("hithit"												);}}}
IEnumerator Timer(												){
yield return new WaitForSecondsRealtime(2						);
speed = speed * -1												;
startTimer = true												;}}