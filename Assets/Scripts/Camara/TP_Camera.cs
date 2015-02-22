using UnityEngine;
using System.Collections;

public enum Modos { Follow = 1, Libre, Orbit, Dios, Puntos, Cinema }

public class TP_Camera : MonoBehaviour {

    public static TP_Camera Instance;
    public GameObject objetivo;
    public float velX,velY;
    public float velOrbitX;
    public float smoothX,smoothY;
    
    float x,y;
    Vector3 offset;

    public float distancia;
    float distanciaMin,distanciaMax;

    public bool godMode;

    
    public Modos modoCamara = Modos.Follow;

    void Awake()
    {
        Instance = this;
    }


	// Use this for initialization
	void Start () {
        x = transform.eulerAngles.x;
        godMode = false;
	
	}

    void Update()
    {
        if (modoCamara != Modos.Dios) godMode = false;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        switch (modoCamara)
        {
            case Modos.Follow:

                x += Input.GetAxis("Horizontal") * velX * distancia * Time.deltaTime;
                //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                y = 30f; // cambio manual de la inclinación

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distancia);
                Vector3 position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);

                //TESTING
                offset = Camera.main.transform.position - objetivo.transform.position;
                break;

            case Modos.Libre:

                //codigo de movimiento de cámara aqui
                offset.z = -20f;
                transform.position = Vector3.Slerp(transform.position, objetivo.transform.position + offset, smoothX * Time.deltaTime);

                break;
            case Modos.Orbit:

                x += Input.GetAxis("Mouse X") * velOrbitX * distancia * Time.deltaTime;
                //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                y = 30f; // cambio manual de la inclinación

                rotation = Quaternion.Euler(y, x, 0);

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                negDistance = new Vector3(0.0f, 0.0f, -distancia);
                position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);
                break;
            case Modos.Dios: //RETOCAR CON CONTROLES DE SEGUNDO JOYSTICK
                if (!godMode) godMode = true;;
                transform.Translate(Input.GetAxisRaw("Horizontal") * 10f * Time.deltaTime, 0f, Input.GetAxisRaw("Vertical") * 10f * Time.deltaTime, Space.World);
                //transform.Rotate(0f, Input.GetAxisRaw("Horizontal") * 90f * Time.deltaTime, 0f, Space.World);
                break;
            case Modos.Puntos:
                //codigo de movimiento de cámara aqui
                break;
            case Modos.Cinema:
                //codigo de movimiento de cámara aqui
                break;

        }
	}
}
