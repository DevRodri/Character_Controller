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
    private bool isGrounded;
    private bool isJumping;
    private bool isReJumping;
    private float floorPos;

    void Awake()
    {
        Instance = this;
        moveVector = Vector3.zero;
        isGrounded = isJumping = isReJumping = false;
        floorPos = 0f;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateMovement();
        ApplyGravity();
	}

    public void UpdateMovement() //REVISAR
    {
        if (moveVector.magnitude > 1) moveVector = Vector3.Normalize(moveVector);

        Vector3 newDirection = Quaternion.LookRotation(transform.position - Camera.main.transform.position).eulerAngles;
        newDirection.x = 0;
        newDirection.z = 0;
        Quaternion rot = Camera.main.transform.rotation;
        Camera.main.transform.rotation = Quaternion.Euler(newDirection);
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, Camera.main.transform);
        Camera.main.transform.rotation = rot;

        //Encarar al jugador hacia donde se mueve
        if (moveVector != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation * Quaternion.Euler(newDirection)/*Camera.main.transform.rotation*/, Time.deltaTime * 5f);
        }
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
        Debug.Log("Entro");
        Debug.Log("My Pos: " + this.transform.position.y);
        Debug.Log("Jumping: " + isJumping);
        Debug.Log("Rejumping" + isReJumping);
        if (!isJumping && isGrounded)
        {
            Debug.Log("I'm Jumping !!!)");
            this.rigidbody.AddForce(new Vector3(0f, 1f * jumpSpeed, 0f), ForceMode.Impulse);
            isJumping = true;
        }
        else if (!isReJumping && this.transform.position.y > floorPos)
        {
            Debug.Log("I'm Rejumping !!!");
            this.rigidbody.AddForce(new Vector3(0f, 0.75f * jumpSpeed, 0f), ForceMode.Impulse);
            isReJumping = true;
        }
    }

    void ApplyGravity()
    {
        this.rigidbody.AddForce(new Vector3(0f, -1f * gravity, 0f), ForceMode.Acceleration);
    }

    public void OnCollisionStay(Collision col)
    {
        //Debug.Log("I'm Grounded !!!");
        //Debug.Log("Floor Pos: " + floorPos);
        //Debug.Log("My Pos: " + this.transform.position.y);
        isGrounded = true;
        isJumping = false;
        isReJumping = false;
        floorPos = this.transform.position.y;
    }

    public void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }

}
