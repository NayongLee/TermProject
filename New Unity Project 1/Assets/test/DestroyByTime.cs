﻿using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float destroyTime;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
