using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static event Action SpinButtonPressed;
    //public static event Action BetButtonPressed; // TO-DO: invoke for bet script subscriber to increase play amount
    
    public static InputManager Instance { get; private set; }

    private void Awake()
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
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    public void OnSpinClick()
    {
        if (SpinSymbol.isSpinning)
        {
            return; // cannot spin while already spinning
        }
        
        // if sufficient balance or not
        if (UIManager.Instance.PlayAmount <= UIManager.Instance.Balance)
        {
            UIManager.Instance.SubtractBalance(UIManager.Instance.PlayAmount);
            
            RandomNumberGenerator.CalculateSelectedSymbols();
            
            SpinButtonPressed?.Invoke(); // invoke event if there are subscribers/listeners (not null)
        }
        else
        {
            Debug.LogWarning("Insufficient balance to place this spin. Please fund your account or lower the play amount.");
        }
    }

    public void OnBetClick()
    {
        if (!SpinSymbol.isSpinning)
        {
            switch (UIManager.Instance.PlayAmount)
            {
                case 1:
                    UIManager.Instance.PlayAmount = 5;
                    break;
                case 5:
                    UIManager.Instance.PlayAmount = 10;
                    break;
                case 10:
                    UIManager.Instance.PlayAmount = 100;
                    break;
                case 100:
                    UIManager.Instance.PlayAmount = 1000;
                    break;
                case 1000:
                    UIManager.Instance.PlayAmount = 1;
                    break;
            }
            
            UIManager.Instance.DisplayPlayAmount(UIManager.Instance.PlayAmount);
        }
    }
}
