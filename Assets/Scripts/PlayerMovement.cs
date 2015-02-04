using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float turnSpeed;
    public float jumpForce;

    public float gravity;

    private bool isGrounded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (isGrounded)
        {
            /*REHACER EL MOVIMIENTO CON FUERZAS */
            //Player Movement
            this.transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime);
            this.transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime, 0);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                //calcular la dirección de salto
                Vector3 jumpDir = (this.transform.forward * Input.GetAxis("Vertical")) + this.transform.up;


                this.rigidbody.AddForce(jumpDir * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        //apply gravity
        if(!isGrounded)
        {
            this.rigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }	
	}

    void OnCollisionStay(Collision collisionInfo)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        isGrounded = false;
    }
}
