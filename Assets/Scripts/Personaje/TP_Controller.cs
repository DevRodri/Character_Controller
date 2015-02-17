using UnityEngine;
using System.Collections;

public class TP_Controller : MonoBehaviour {


    //ATTRIBUTES

    //PUBLIC
    public static TP_Controller Instance;

    public Vector3 lAnalogDirection;
    public Vector3 rAnalogDirection;
    public float deadZone;

    //PRIVATE


    void Awake()
    {
        Instance = this;
        lAnalogDirection.y = rAnalogDirection.y = 0f;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        DetectInput();
	
	}

    //Get input from controller
    void DetectInput()
    {
        //Joysticks Input
        Debug.DrawRay(transform.position, lAnalogDirection * 10f, Color.green);
        rAnalogDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        Debug.DrawRay(transform.position, rAnalogDirection * 10f, Color.blue);

        if (Input.GetAxisRaw("Horizontal") > deadZone || Input.GetAxisRaw("Horizontal") < -deadZone)
            lAnalogDirection.x = Input.GetAxisRaw("Horizontal");
        else
            lAnalogDirection.x = 0f;

        if (Input.GetAxisRaw("Vertical") > deadZone || Input.GetAxisRaw("Vertical") < -deadZone)
            lAnalogDirection.z = Input.GetAxisRaw("Vertical");
        else
            lAnalogDirection.z = 0f;

        TP_Motor.Instance.moveVector = lAnalogDirection;


        //Jumping Input
        if (Input.GetButtonDown("X360 A Button"))
        {
            Jump();
        }

        //Apuntar
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TP_Skills.Instance.isTargetting = !TP_Skills.Instance.isTargetting;
        }
    }

    void Jump()
    {
        TP_Motor.Instance.Jump();
    }

}
