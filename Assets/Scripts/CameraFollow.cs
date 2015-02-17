using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    //variables
    public GameObject target;
    public Transform targettingPoint;
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

        if (TP_Skills.Instance.isTargetting)
        {
            EnableTargetting();
            //transform.LookAt(target.transform.forward);
        }
        else
        {

            Quaternion destRotation = Quaternion.identity;
            destRotation.eulerAngles = target.transform.rotation.eulerAngles;

            offset.y = Mathf.Lerp(offset.y, height, smoothMove * Time.deltaTime);
            offset.z = Mathf.Lerp(offset.z, -distance, smoothMove * Time.deltaTime);

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

    void EnableTargetting()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, targettingPoint.position, smoothMove * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target.transform.rotation, smoothMove * 2f * Time.deltaTime);
        this.camera.fieldOfView = Mathf.Lerp(this.camera.fieldOfView, 45f, smoothMove * Time.deltaTime);
    }
}
