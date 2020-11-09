﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private int _doorCount;
    private List<int> _randomDoors;

    public int fakeDoorCount;

    // Start is called before the first frame update
    void Start()
    {
        _randomDoors = new List<int>();
        _doorCount = transform.childCount;
        for(int i = 0; i < fakeDoorCount; i++)
        {
            _randomDoors.Add(Random.Range(0, _doorCount));
        }
        foreach(var item in _randomDoors) {
            Debug.Log(item.ToString());
        }
        int randomDoor = Random.Range(0, _doorCount);
        for(int i = 0; i < _doorCount; i++) {
            GameObject doorGameObject = transform.GetChild(i).gameObject;
            doorGameObject.GetComponent<Door>().isFakeDoor = _randomDoors.Contains(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
