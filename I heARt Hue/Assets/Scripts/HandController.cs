using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{

    ActionBasedController controller;
    Hand hand;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        hand = GetComponentInChildren<Hand>();
    }


    // Update is called once per frame
    void Update()
    {

        //Checks for null as hands instantiate slightly late
        if (hand != null)
        {
            //Sets the values of grip and trigger from the Action Map in ActionBasedController
            hand.SetGrip(controller.selectAction.action.ReadValue<float>());
            hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        }
    }
}
