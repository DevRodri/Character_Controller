using UnityEngine;
using System.Collections;

public class TP_Skills : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Skills Instance;
    public bool isTargetting;

    //PRIVATE


    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        isTargetting = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
