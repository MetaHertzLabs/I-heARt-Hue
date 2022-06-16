using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomizer : MonoBehaviour
{
    public GameObject[] blocksToMove;
    public List<Vector3> positionsToMoveTo, positionsToMoveToTemp;
    public float randomizeDelay;
    bool scalingBlocks;





    // Start is called before the first frame update
    void Start()
    {
        StartRandomizeBlocksCoroutine();
    }

    //Making this a public function so it can be called in "Reset" button press as well
    public void StartRandomizeBlocksCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine("RandomizeBlocks");
    }

    IEnumerator RandomizeBlocks()
    {
        //This function is called when resetting the level, so first resets move counter to 0
        GameText.instance.ResetTotalMoves();

        //Clones a temporary list based off original list of positions, so positions can be removed while keeping original values stored in original list
        positionsToMoveToTemp = new List<Vector3>(positionsToMoveTo);

        foreach(GameObject block in blocksToMove)
        {
            Animation thisCubeAnimator = block.GetComponent<Animation>();
            thisCubeAnimator.Play("ScaleDown");
            yield return new WaitForSeconds(0.1f);

        }

        //Go through all blocks and set position to a random Vector3 still remaining in the temporary list
        foreach (GameObject block in blocksToMove)
        {
            int randomIndex = Random.Range(0, positionsToMoveToTemp.Count - 1);
            block.transform.localPosition = positionsToMoveToTemp[randomIndex];
            Animation thisCubeAnimator = block.GetComponent<Animation>();
            thisCubeAnimator.Play("ScaleUp");
            yield return new WaitForSeconds(0.1f);
            CubeData thisCubeData = block.GetComponent<CubeData>();
            thisCubeData.savedPosition = block.transform.localPosition;


            if (thisCubeData.savedPosition == thisCubeData.correctPosition)
            {
                SavedCubeData.instance.correctPositionStatuses[thisCubeData.cubeIndex] = true;

            }
            else
            {
                SavedCubeData.instance.correctPositionStatuses[thisCubeData.cubeIndex] = false;
            }
            positionsToMoveToTemp.RemoveAt(randomIndex);
            yield return new WaitForSeconds(randomizeDelay);
        }
    }

}
