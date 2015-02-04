using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    //variables
    public GameObject target;
    public float distance;
    public float height;
    public float smoothMove;

    private Vector3 offset;

	// Use this for initialization
	void Start () 
    {
        this.transform.position = target.transform.position + (-target.transform.forward * distance) + (target.transform.up * height);
        this.transform.rotation = target.transform.rotation;

        offset = this.transform.position - target.transform.position;	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        Quaternion destRotation = Quaternion.identity;
        destRotation.eulerAngles = target.transform.rotation.eulerAngles;

        offset.y = height;
        offset.z = -distance;

        Vector3 destPos = target.transform.position + (destRotation * offset);

        this.transform.position = Vector3.Lerp(this.transform.position, destPos, smoothMove * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, destRotation, smoothMove * Time.deltaTime);

        this.transform.LookAt(target.transform.position);
        
	}
}
