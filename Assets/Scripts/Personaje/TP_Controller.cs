using UnityEngine;
using System.Collections;

public class TP_Controller : MonoBehaviour {


    //ATTRIBUTES

    //PUBLIC
    public static TP_Controller Instance;

    public Vector3 lAnalogDirection;
    public Vector3 rAnalogDirection;
    public float deadZone;

    public CharacterController controlador;

    void Awake()
    {
        Instance = this;
        lAnalogDirection.y = rAnalogDirection.y = 0f;
        controlador = GetComponent<CharacterController>();
    }

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        InputMovimiento(); //input y actualización del movimiento
        InputHabilidades();
        InputCamara();

        //actualizamos el movimiento del player
        TP_Motor.Instance.UpdateMovement();
	}

    //Get input from controller
    void InputMovimiento()
    {
        //Joysticks Input
        Debug.DrawRay(transform.position, lAnalogDirection * 10f, Color.green);

        if (Input.GetAxisRaw("Horizontal") > deadZone || Input.GetAxisRaw("Horizontal") < -deadZone)
            lAnalogDirection.x = Input.GetAxisRaw("Horizontal");
        else
            lAnalogDirection.x = 0f;

        if (Input.GetAxisRaw("Vertical") > deadZone || Input.GetAxisRaw("Vertical") < -deadZone)
            lAnalogDirection.z = Input.GetAxisRaw("Vertical");
        else
            lAnalogDirection.z = 0f;

        TP_Motor.Instance.moveVector = lAnalogDirection;

    }

    void InputHabilidades()
    {
        //Jumping Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Apuntar
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TP_Skills.Instance.isTargetting = !TP_Skills.Instance.isTargetting;
        }
    }

    void InputCamara()
    {
        //Designar boton para alternar entre modos de cámara { LIBRE, SEGUIMIENTO, DIOS, PUNTOS POR PANTALLA}
        rAnalogDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        Debug.DrawRay(transform.position, rAnalogDirection * 10f, Color.blue);

        if (Input.GetKeyUp(KeyCode.C))
        {
            if (TP_Camera.Instance.modoCamara == Modos.Cinema)
                TP_Camera.Instance.modoCamara = Modos.Follow;
            else
                TP_Camera.Instance.modoCamara += 1;

            Debug.Log("El modo de Camara es: " + TP_Camera.Instance.modoCamara);
        }
        
    }

    void Jump()
    {
        TP_Motor.Instance.Jump();
    }

}
