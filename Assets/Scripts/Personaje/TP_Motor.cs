using UnityEngine;
using System.Collections;

public class TP_Motor : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Motor Instance;
    public Vector3 moveVector;
    public float moveSpeed;
    public float jumpSpeed;
    public float dampSpeed;
    public float rotSpeed;
    public float gravity;



    //PRIVATE
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isReJumping = false;

    void Awake()
    {
        Instance = this;
        moveVector = Vector3.zero;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateMovement();
	}

    void UpdateMovement()
    {

        //this.transform.Rotate(0f, moveVector.x * rotSpeed * Time.deltaTime, 0f);

        //transform to world space
        moveVector = transform.TransformDirection(moveVector);

        FacePlayerToMovementDir();
        
        //normalize the movement vector
        if (moveVector.magnitude > 1) moveVector = Vector3.Normalize(moveVector);
        //Debug.DrawRay(transform.position, moveVector * 5f);

        //add speed
        moveVector *= moveSpeed;

        //add vertical movement
        //moveVector = new Vector3(moveVector.x, yVelocity, moveVector.z);

        //apply gravity
        ApplyGravity();

        Debug.DrawRay(this.transform.position, moveVector, Color.red);

        //update the movement
        this.transform.Translate(moveVector * Time.deltaTime, Space.World);
        
    }

    void FacePlayerToMovementDir()
    {
        if (moveVector != Vector3.zero)
        {
            Quaternion faceDir = Quaternion.LookRotation(moveVector);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, faceDir, dampSpeed * Time.deltaTime);
        }
    }


    public void Jump()
    {
        Debug.Log("Button 0 pressed!!");
        if (!isJumping)
        {
            this.rigidbody.AddForce(new Vector3(0f, 1f * jumpSpeed, 0f), ForceMode.Impulse);
            isJumping = true;
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            this.rigidbody.AddForce(new Vector3(0f, -1f * gravity, 0f), ForceMode.Acceleration);
        }
    }

    public void OnCollisionStay(Collision col)
    {
        isGrounded = true;
        isJumping = false;
        isReJumping = false;
    }

    public void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }

}
