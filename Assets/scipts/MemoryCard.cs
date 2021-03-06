﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField] private GameObject cardBack;
    [SerializeField] private Sprite image;
    [SerializeField] private SceneController controller;

    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void setCard(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canUncover) {

            cardBack.SetActive(false);
            controller.unCovered(this);
        }
    }

    public void Cover() {
        cardBack.SetActive(true);
    }
    void Start () {
       
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
