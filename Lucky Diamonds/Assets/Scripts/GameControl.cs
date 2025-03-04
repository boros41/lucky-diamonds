using System;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static event Action SpinButtonPressed;
    
    [SerializeField] private TextMeshProUGUI prizeText;
    //[SerializeField] private Row[] rows;
    [SerializeField] private Reel1 reel1;
    [SerializeField] private Reel2 reel2;
    [SerializeField] private Reel3 reel3;
    
    public static float BetAmount = 1f; // rename this to playAmount
    //public static float PrizeValue;
    private bool _resultsChecked = false;
    
    public void OnMouseClick()
    {
        if (reel1.firstReelStopped && reel2.secondReelStopped && reel3.thirdReelStopped)
        {
            RandomNumberGenerator.CalculateSelectedSymbols();
            SpinButtonPressed?.Invoke(); // invoke event if there are subscribers/listeners (not null)
        }
    }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if rows are still spinning
        if (!reel1.firstReelStopped || !reel2.secondReelStopped || !reel3.thirdReelStopped)
        {
            //Debug.Log($"Reels still spinning, setting prize value to 0: 0");
            //RandomNumberGenerator.PrizeValue = 0;
            prizeText.enabled = false;
            _resultsChecked = false;
        }

        /*
        Debug.Log($"Reel1 stopped?:{reel1.firstReelStopped}");
        Debug.Log($"Reel2 stopped?:{reel2.secondReelStopped}");
        Debug.Log($"Reel3 stopped?:{reel3.thirdReelStopped}");
        Debug.Log($"Results checked?:{_resultsChecked}");
        */
        if (reel1.firstReelStopped && reel2.secondReelStopped && reel3.thirdReelStopped && !_resultsChecked)
        {
            //CheckResults();
            _resultsChecked = true;
            prizeText.enabled = true;
            
            //Debug.Log($"Setting text to prize value: {RandomNumberGenerator.PrizeValue}");
            prizeText.text = $"{RandomNumberGenerator.PrizeValue:C}";
        }
    }

    private void CheckResults()
    {

        /*
        if (rows[0].stoppedSlot.Equals(rows[1].stoppedSlot) && rows[0].stoppedSlot.Equals(rows[2].stoppedSlot))
        {
            switch (rows[0].stoppedSlot)
            {
                case "Diamond":
                    _prizeValue = betAmount * 1000;
                    break;
                case "Crown":
                    _prizeValue = betAmount * 200;
                    break;
                case "Seven":
                    _prizeValue = betAmount * 80;
                    break;
                case "Bar":
                    _prizeValue = betAmount * 40;
                    break;
                case "Watermelon":
                    _prizeValue = betAmount * 25;
                    break;
                case "Lemon":
                    _prizeValue = betAmount * 10;
                    break;
                case "Cherry":
                    _prizeValue = betAmount * 5;
                    break;
            }
        } else if (rows[0].stoppedSlot.Equals(rows[1].stoppedSlot) || // 3 total slots C(3,2) "n choose r"
                   rows[0].stoppedSlot.Equals(rows[2].stoppedSlot) ||
                   rows[1].stoppedSlot.Equals(rows[2].stoppedSlot))
        {
            switch (rows[0].stoppedSlot)
            {
                case "Watermelon":
                    _prizeValue = betAmount * 4;
                    break;
                case "Lemon":
                    _prizeValue = betAmount * 3;
                    break;
                case "Cherry":
                    _prizeValue = betAmount * 2;
                    break;
            }
        }
        
        _resultsChecked = true;
        */
    }
}
