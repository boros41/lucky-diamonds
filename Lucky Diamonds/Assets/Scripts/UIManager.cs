using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // singleton since only one instance of the UI components should exist
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI balanceText;
    public TextMeshProUGUI BalanceText => balanceText;
    
    [SerializeField] private TextMeshProUGUI winText;
    public TextMeshProUGUI WinText => winText;
    
    [SerializeField] private TextMeshProUGUI playAmountText;
    public TextMeshProUGUI PlayAmountText => playAmountText;
    
    public float Balance { get; private set; }

    public float PlayAmount { get; set; } = 1f;

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
        AddBalance(50_000); // 50k default
        DisplayWin(0);
        DisplayPlayAmount(PlayAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SubtractBalance(float amount)
    {
        Balance -= amount;
        balanceText.text = $"{Balance:C}";
    }

    public void AddBalance(float amount)
    {
        Balance += amount;
        balanceText.text = $"{Balance:C}";
    }
    
    

    public void DisplayWin(float amount)
    {
        winText.text = amount.ToString("C"); // won nothing on launch
    }

    public void DisplayPlayAmount(float amount)
    {
        playAmountText.text = $"{amount:C}";
    }

}
