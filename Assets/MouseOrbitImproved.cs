using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public Transform targettingPoint;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;
    bool godmode;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        godmode = true;

        // Make the rigid body not change rotation
        //if (rigidbody)
        //    rigidbody.freezeRotation = true;
    }

    void LateUpdate()
    {
        if (!TP_Skills.Instance.isTargetting)
        {
            x += Input.GetAxis("Horizontal") * xSpeed * distance * Time.deltaTime;
            //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            //y = ClampAngle(y, yMinLimit, yMaxLimit);
            y = 30f;

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            /*RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }*/
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);

        }
        else
        {
            this.transform.position = Vector3.Slerp(this.transform.position, targettingPoint.position, 2f * Time.deltaTime);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targettingPoint.transform.rotation, 2f * 2f * Time.deltaTime);
            this.camera.fieldOfView = Mathf.Lerp(this.camera.fieldOfView, 45f, 2f * Time.deltaTime);
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }


}