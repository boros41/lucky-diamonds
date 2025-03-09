using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance { get; private set; }
    
    private bool _resultsChecked;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        if (SpinSymbol.isSpinning)
        {
            //Debug.Log("Spinning");
            //Debug.Log($"Reels still spinning, setting prize value to 0: 0");
            //RandomNumberGenerator.PrizeValue = 0;
            UIManager.Instance.WinText.enabled = false;
            _resultsChecked = false;
        }
        
        //Debug.Log($"Before check - isSpinning: {SpinSymbol.isSpinning}, _resultsChecked: {_resultsChecked}");
        if (!SpinSymbol.isSpinning && !_resultsChecked)
        {
            //Debug.Log("Entered results check. Setting _resultsChecked = true.");
            _resultsChecked = true;
            
            UIManager.Instance.WinText.enabled = true;

            UIManager.Instance.DisplayWin(RandomNumberGenerator.PrizeValue);
            UIManager.Instance.AddBalance(RandomNumberGenerator.PrizeValue);
            
            
        }
    }
    
}
