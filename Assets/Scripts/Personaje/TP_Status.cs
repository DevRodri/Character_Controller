﻿using UnityEngine;
using System.Collections;

public class TP_Status : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Status Instance;


    //PRIVATE
    private int vida;
    private bool isDead;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        vida = 100;
        isDead = false;
	}

    public int GetVida(){ return vida; }

    public void SetVida(int num)
    {
        if (num == 0) isDead = true;
        else if (num <= 100) vida = num;
    }

    public void AddVida(int num)
    {
        if (vida + num > 100) vida = 100;
        else vida += num;
    }

    public void SubsVida(int num)
    {
        if (vida - num > 0)
        {
            vida = 0;
            isDead = false;
        }
        else vida += num;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
