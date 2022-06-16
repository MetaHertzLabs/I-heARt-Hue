using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class CubeData : MonoBehaviour
{

    public Vector3 savedPosition, correctPosition;
    public int cubeIndex;

    [SerializeField] Outline outline;



    public void SelectCube()
    {
        StartCoroutine("SelectCubeCoroutine");
    }

    IEnumerator SelectCubeCoroutine()
    {

        //Checks to see if first cube was already selected, if so then it swaps the saved position values of first cube with this second cube
        if (SavedCubeData.instance.firstCubeSelected == true)
        {
            CubeData firstCubeData = SavedCubeData.instance.firstSelectedCube.GetComponent<CubeData>();
            SavedCubeData.instance.firstCubeSelected = false;
            firstCubeData.outline.OutlineWidth = 0;

            if (this.cubeIndex == firstCubeData.cubeIndex)
            {
                yield break;
            }
            //Grab necessary components
            Animation firstCubeAnim = SavedCubeData.instance.firstSelectedCube.GetComponent<Animation>();
            Animation secondCubeAnim = GetComponent<Animation>();

            //Animate shrink then wait slightly to allow animation to complete
            firstCubeAnim.Play("ScaleDown");
            secondCubeAnim.Play("ScaleDown");
            yield return new WaitForSeconds(0.67f);

            //Swap positions and save them
            SavedCubeData.instance.firstSelectedCube.transform.localPosition = this.savedPosition;
            firstCubeData.savedPosition = this.savedPosition;

            this.transform.localPosition = SavedCubeData.instance.currentMasterPosition;
            savedPosition = SavedCubeData.instance.currentMasterPosition;

            //Animate them back up to scale
            firstCubeAnim.PlayQueued("ScaleUp");
            secondCubeAnim.PlayQueued("ScaleUp");


            //If this is the position the box should be in, mark position as correct
            if (firstCubeData.savedPosition == firstCubeData.correctPosition)
            {
                SavedCubeData.instance.correctPositionStatuses[firstCubeData.cubeIndex] = true;
            }
            else
            {
                SavedCubeData.instance.correctPositionStatuses[firstCubeData.cubeIndex] = false;
            }


            if (this.savedPosition == this.correctPosition)
            {
                SavedCubeData.instance.correctPositionStatuses[cubeIndex] = true;
            }
            else
            {
                SavedCubeData.instance.correctPositionStatuses[cubeIndex] = false;
            }

            //Set bool to false so next function starts at bottom, then check positions
            SavedCubeData.instance.firstCubeSelected = false;
            SavedCubeData.instance.CheckPositionStatuses();

        }


        //Else if no cube is currently selected, then save position values of first selected cube and set the firstCubeSelected check to true
        else if (SavedCubeData.instance.firstCubeSelected == false)
        {
            SavedCubeData.instance.firstCubeSelected = true;

            //Sets up game object and position to swap the next cube to
            SavedCubeData.instance.firstSelectedCube = this.gameObject;
            outline.OutlineWidth = 10;
            SavedCubeData.instance.currentMasterPosition = savedPosition;
        }


        //Adds +1 to total move counter for stat counting
        GameText.instance.AddOneMoveToTotal();


    }
}
