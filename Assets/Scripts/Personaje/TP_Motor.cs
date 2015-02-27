using UnityEngine;
using System.Collections;

public class TP_Motor : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Motor Instance;
    public Vector3 moveVector;
    Vector3 targetDir;
    public float moveSpeed;
    public float jumpSpeed;
    public float dampSpeed;
    public float rotSpeed;
    public float gravity;



    //PRIVATE
    private float verticalMovement;
    private bool isGrounded;
    private bool isJumping;
    private bool isReJumping;
    private float floorPos;

    void Awake()
    {
        Instance = this;
        moveVector = targetDir = Vector3.zero;
        isGrounded = isJumping = isReJumping = false;
        floorPos = 0f;
    }

	// Use this for initialization
	void Start () {
        verticalMovement = 0f;
	}

    public void UpdateMovement() //REVISAR
    {
        Transform camara = TP_Camera.Instance.transform;

        //coordenadas de cámara a wolrd space
        Vector3 forward = camara.TransformDirection(Vector3.forward);
        forward.y = 0f;
        forward = forward.normalized;

        //calcular forward y right en funcion de coordenadas de camara
        Vector3 right = Vector3.zero;
        right.x = forward.z;
        right.z = -forward.x;

        //obtener movimiento del joystick y calcular dirección de movimiento
        targetDir = moveVector.x * right + moveVector.z * forward;
        //targetDir = targetDir.normalized;

        //aplicar velocidad
        targetDir *= moveSpeed;

        //aplicar salto y/o gravedad
        if (Input.GetKeyUp(KeyCode.Space)) targetDir.y = jumpSpeed;

        targetDir.y -= gravity * Time.deltaTime;

        //actualizar movimiento del controlador
        TP_Controller.Instance.controlador.Move(targetDir * Time.deltaTime);

        FacePlayerToMovementDir();
    }

    private void UpdateMovement_old()
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
        if (targetDir != Vector3.zero)
        {
            Quaternion faceDir = Quaternion.LookRotation(targetDir);
            faceDir.x = 0f;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, faceDir, dampSpeed * Time.deltaTime);
        }
    }


    public void Jump()
    {
        if (TP_Controller.Instance.controlador.isGrounded)
        {
            Debug.Log("I'm grounded, so lets jump!!");
            verticalMovement = jumpSpeed;
        }
    }

    void ApplyGravity()
    {
        targetDir.y -= gravity;
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
