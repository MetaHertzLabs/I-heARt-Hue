using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameText : MonoBehaviour
{
    public static GameText instance = null;
    int totalMoves;

    [SerializeField] TMP_Text winText;

    public void WinResults()
    {
        winText.text = "You win! Total moves: " + totalMoves.ToString();
    }

    public void ResetWinResults()
    {
        totalMoves = 0;
        winText.text = "";
    }

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void AddOneMoveToTotal()
    {
        totalMoves++;
    }

    public void ResetTotalMoves()
    {
        totalMoves = 0;
    }

}
