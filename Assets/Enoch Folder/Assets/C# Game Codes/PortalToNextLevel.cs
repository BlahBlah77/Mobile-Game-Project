﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToNextLevel : MonoBehaviour {

    public GameObject Player;
    public Transform SpawnLevelPoint;

	// Use this for initialization
	void Start () {

        transform.position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            other.transform.position = SpawnLevelPoint.transform.position;
        }
    }

}