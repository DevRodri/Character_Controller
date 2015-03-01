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
    public float reJumpSpeed;
    public float dampSpeed;
    public float rotSpeed;
    public float gravity;
    public float reJumpDelay;



    //PRIVATE
    private float verticalMovement;
    //private bool isJumping;
    //private bool isReJumping;
    private float floorPos;

    void Awake()
    {
        Instance = this;
        moveVector = targetDir = Vector3.zero;
        //isJumping = isReJumping = false;
        floorPos = 0f;
    }

	// Use this for initialization
	void Start () {
        verticalMovement = 0f;
	}

    private void UpdateMovement() //REVISAR
    {
        if (TP_Controller.Instance.controlador.isGrounded)
        {
            TP_Status.Instance.SetJumping(false);
            TP_Status.Instance.SetReJumping(false);
        }

        Transform camara = TP_Camera.Instance.transform;

        //coordenadas de cámara a wolrd space
        Vector3 forward = camara.TransformDirection(Vector3.forward);
        forward.y = 0f;
        forward = forward.normalized;

        //calcular forward y right en funcion de coordenadas de camara
        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        //obtener movimiento del joystick y calcular dirección de movimiento
        targetDir = moveVector.x * right + moveVector.z * forward;
        //targetDir = targetDir.normalized;

        //aplicar velocidad de movimiento
        targetDir *= moveSpeed;

        //aplicar gravedad si no está en el suelo
        //if (!TP_Controller.Instance.controlador.isGrounded) verticalMovement -= gravity * Time.deltaTime;
        ApplyGravity();

        //añadir velocidad vertical
        targetDir.y = verticalMovement;

        //actualizar movimiento del controlador
        TP_Controller.Instance.controlador.Move(targetDir * Time.deltaTime);

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
        if (moveVector != Vector3.zero)
        {
            targetDir.y = 0f;
            Quaternion faceDir = Quaternion.LookRotation(targetDir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, faceDir, dampSpeed * Time.deltaTime);
        }
    }

    public void MovePlayer()
    {
        UpdateMovement();
        FacePlayerToMovementDir();
    }

    public void Jump()
    {
        if (TP_Controller.Instance.controlador.isGrounded && !TP_Status.Instance.IsJumping())
        {
            verticalMovement = jumpSpeed;
            TP_Status.Instance.SetJumping(true);
        }
        else if (!TP_Status.Instance.IsReJumping() && verticalMovement < reJumpDelay)
        {
            verticalMovement = reJumpSpeed;
            TP_Status.Instance.SetReJumping(true);
        }
    }

    void ApplyGravity()
    {
        if (!TP_Controller.Instance.controlador.isGrounded)
            verticalMovement -= gravity * Time.deltaTime;
    }

    /*
    public void OnCollisionStay(Collision col)
    {
        //Debug.Log("I'm Grounded !!!");
        //Debug.Log("Floor Pos: " + floorPos);
        //Debug.Log("My Pos: " + this.transform.position.y);
        isJumping = false;
        isReJumping = false;
        floorPos = this.transform.position.y;
    }

    public void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }
    */

}
