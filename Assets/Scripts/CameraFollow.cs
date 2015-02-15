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
        //Vector3 destPos = target.transform.position + offset;
        //Debug.Log("destPos: " + (target.transform.position - this.transform.position).magnitude);

        //if ((target.transform.position - this.transform.position).magnitude > distance)
        //{
            this.transform.position = Vector3.Slerp(this.transform.position, destPos, smoothMove * Time.deltaTime);
        //}

        this.transform.LookAt(target.transform.position);
        
	}
}
