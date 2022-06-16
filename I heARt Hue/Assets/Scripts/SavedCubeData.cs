using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SavedCubeData : MonoBehaviour
{

    public static SavedCubeData instance = null;
    public Vector3 currentMasterPosition;
    public bool firstCubeSelected = false;
    public bool cubeHovering, cubeNotHovering;
    public bool[] correctPositionStatuses;
    public GameObject firstSelectedCube;


    private void Awake()
    {
        //Standard singleton check to see if another instance already exists besides this one, destroys it if so, sets this as the instance if not.
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void CheckPositionStatuses()
    {
        //Goes through the list of bools and if any of them are false, end the function, if all are true it will trigger the end of game.
        foreach(bool status in correctPositionStatuses)
        {
            if(status == false)
            {
                return;
            }

        }

        //Simply lets you know you won and how many moves it took you
        GameText.instance.WinResults();
    }



}
