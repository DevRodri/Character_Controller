using UnityEngine;
using System.Collections;

public class Intertia : MonoBehaviour {

	public float speed =10.0f;
	public float offset_lateral=0f;
	public float offset_horizontal=0f;
	public float energy;
	public GameObject player;
	private Vector3 _prevPosition;
	// Use this for initialization
	void Start () {
		rigidbody.useGravity=false;
		energy = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			_prevPosition = transform.position;
			//GameObject Beta = GameObject.Find ("Beta");	
			//Vector3 localForward = Beta.transform.worldToLocalMatrix.MultiplyVector(Beta.transform.forward);	
			float step = speed * Time.deltaTime;
			
			//transform.position = Vector3.MoveTowards (transform.position, new Vector3 (
			//(Beta.transform.right.x*offset_lateral)+(Beta.transform.position.x+Beta.transform.forward.x*2), 
		    //(2.0f+Beta.transform.position.y+offset_horizontal),
		    //(Beta.transform.right.z*offset_lateral)+(Beta.transform.position.z+Beta.transform.forward.z*2)), step);
		transform.position = Vector3.Lerp(transform.position, new Vector3 (
			(player.transform.right.x*offset_lateral)+(player.transform.position.x+player.transform.forward.x*2), 
			(2.0f+player.transform.position.y+offset_horizontal),
			(player.transform.right.z*offset_lateral)+(player.transform.position.z+player.transform.forward.z*2)), step); 
		} 
	void OnDestroy() {
		rigidbody.useGravity=true;
		rigidbody.velocity =  20*(transform.position -_prevPosition) ;//Añadimos la incercia al finalizar el movimiento.
	}

}
