using UnityEngine;
using System.Collections;

public class InverseInertia : MonoBehaviour {

	private Vector3 _prevPosition;
	private Vector3 _startPoint;
	private float _duration=1.5f;
	private float startTime;
	public Vector3 hitpoint;

	// Use this for initialization
	void Start () {
		rigidbody.useGravity=false;
		_startPoint = transform.position; 
		startTime = Time.time; 
	}
	
	void Update () {
		_prevPosition = transform.position;
		///transform.position = Vector3.MoveTowards (transform.position, new Vector3 (hitpoint.x,hitpoint.y,hitpoint.z),Time.deltaTime);
		transform.position = Vector3.Lerp(_startPoint, hitpoint, (Time.time - startTime) / _duration); 
	}

	void OnDestroy() {
		//rigidbody.velocity =  20*(transform.position -_prevPosition) ;//Añadimos la incercia al finalizar el movimiento.
		rigidbody.useGravity=true;
	}
}
