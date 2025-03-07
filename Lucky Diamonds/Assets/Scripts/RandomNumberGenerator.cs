using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomNumberGenerator : MonoBehaviour
{
    public static string[] SelectedSymbols = new string[3]; // contains symbols at respective slot

    private const int _TRIPLE_DIAMOND_MULTIPLIER = 1000;
    private const int _TRIPLE_CROWN_MULTIPLIER = 200;
    private const int _TRIPLE_SEVEN_MULTIPLIER = 40;
    private const int _TRIPLE_BAR_MULTIPLIER = 20;
    private const int _TRIPLE_MELON_MULTIPLIER = 5;
    private const int _DOUBLE_MELON_MULTIPLIER = 4;
    private const int _TRIPLE_LEMON_MULTIPLIER = 3;
    private const int _DOUBLE_LEMON_MULTIPLIER = 2;
    private const float _TRIPLE_CHERRY_MULTIPLIER = 1.5f;
    private const int _DOUBLE_CHERRY_MULTIPLIER = 1;

    public static float PrizeValue; 
    
    public static void CalculateSelectedSymbols()
    {
        //Debug.Log($"Prize value before switch: {PrizeValue}");
        
        const int BOUND = 100_000;
        //const int PRECISION = 1000; // percent chance up to thousandths place

        int randomNumber = Random.Range(0, BOUND); // 0-99,999
        
        switch (randomNumber)
        {
            // triple diamond .025% chance
            case < 25:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                Debug.Log($"Triple Diamond symbols {SymbolString.DIAMOND}:{SymbolString.DIAMOND}:{SymbolString.DIAMOND}");
                
                SelectedSymbols[reelSymbol1] = SymbolString.DIAMOND;
                SelectedSymbols[reelSymbol2] = SymbolString.DIAMOND;
                SelectedSymbols[reelSymbol3] = SymbolString.DIAMOND;

                PrizeValue = GameControl.BetAmount * _TRIPLE_DIAMOND_MULTIPLIER;
                break;
            }
            // triple crown .05% chance
            case < 50:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];

                Debug.Log($"TRIPLE CROWN symbols {SymbolString.CROWN}:{SymbolString.CROWN}:{SymbolString.CROWN}");
                
                SelectedSymbols[reelSymbol1] = SymbolString.CROWN;
                SelectedSymbols[reelSymbol2] = SymbolString.CROWN;
                SelectedSymbols[reelSymbol3] = SymbolString.CROWN;

                PrizeValue = GameControl.BetAmount * _TRIPLE_CROWN_MULTIPLIER;
                break;
            }
            // triple seven .25% chance
            case < 250:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                Debug.Log($"TRIPLE SEVEN symbols {SymbolString.SEVEN}:{SymbolString.SEVEN}:{SymbolString.SEVEN}");

                SelectedSymbols[reelSymbol1] = SymbolString.SEVEN;
                SelectedSymbols[reelSymbol2] = SymbolString.SEVEN;
                SelectedSymbols[reelSymbol3] = SymbolString.SEVEN;

                PrizeValue = GameControl.BetAmount * _TRIPLE_SEVEN_MULTIPLIER;
                break;
            }
            // triple bar .28% chance
            case < 280:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                Debug.Log($"TRIPLE BAR symbols {SymbolString.BAR}:{SymbolString.BAR}:{SymbolString.BAR}");
                
                SelectedSymbols[reelSymbol1] = SymbolString.BAR;
                SelectedSymbols[reelSymbol2] = SymbolString.BAR;
                SelectedSymbols[reelSymbol3] = SymbolString.BAR;

                PrizeValue = GameControl.BetAmount * _TRIPLE_BAR_MULTIPLIER;
                break;
            }
            // triple MELON 1% chance
            case < 1000:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];

                Debug.Log($"TRIPLE MELON {SymbolString.MELON}:{SymbolString.MELON}:{SymbolString.MELON}");
                
                SelectedSymbols[reelSymbol1] = SymbolString.MELON;
                SelectedSymbols[reelSymbol2] = SymbolString.MELON;
                SelectedSymbols[reelSymbol3] = SymbolString.MELON;

                PrizeValue = GameControl.BetAmount * _TRIPLE_MELON_MULTIPLIER;
                break;
            }
            // double MELON 2% chance
            case < 2000:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                

                SelectedSymbols[reelSymbol1] = SymbolString.MELON;
                SelectedSymbols[reelSymbol2] = SymbolString.MELON;

                // possible random symbols (MELON already shown so excluded)
                string[] randomSymbol = {"Diamond", "Crown", "Bar", "Seven", "Cherry", "Lemon"};
                int randomSymbolIndex = Random.Range(0, randomSymbol.Length);

                Debug.Log($"DOUBLE MELON symbols {SymbolString.MELON}:{SymbolString.MELON}:{randomSymbol[randomSymbolIndex]}");
                
                SelectedSymbols[reelSymbol3] = randomSymbol[randomSymbolIndex];

                PrizeValue = GameControl.BetAmount * _DOUBLE_MELON_MULTIPLIER;
                break;
            }
            // triple lemon 3% chance
            case < 3000:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                Debug.Log($"TRIPLE LEMON symbols {SymbolString.LEMON}:{SymbolString.LEMON}:{SymbolString.LEMON}");
                Debug.Log($"reelSymbol1: {reelSymbol1}");
                
                SelectedSymbols[reelSymbol1] = SymbolString.LEMON;
                SelectedSymbols[reelSymbol2] = SymbolString.LEMON;
                SelectedSymbols[reelSymbol3] = SymbolString.LEMON;

                PrizeValue = GameControl.BetAmount * _TRIPLE_LEMON_MULTIPLIER;
                break;
            }
            // double lemon 4% chance
            case < 4000:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                SelectedSymbols[reelSymbol1] = SymbolString.LEMON;
                SelectedSymbols[reelSymbol2] = SymbolString.LEMON;
            
                // possible random symbols (lemon already shown so excluded)
                string[] randomSymbol = {"Diamond", "Crown", "MELON", "Bar", "Seven", "Cherry"};
                int randomSymbolIndex = Random.Range(0, randomSymbol.Length);

                Debug.Log($"DOUBLE Lemon symbols: {SymbolString.LEMON}:{SymbolString.LEMON}:{randomSymbol[randomSymbolIndex]}");
                
                SelectedSymbols[reelSymbol3] = randomSymbol[randomSymbolIndex];

                PrizeValue = GameControl.BetAmount * _DOUBLE_LEMON_MULTIPLIER;
                break;
            }
            // triple cherry 5% chance
            case < 5000:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                Debug.Log($"TRIPLE CHERRY symbols {SymbolString.CHERRY}:{SymbolString.CHERRY}:{SymbolString.CHERRY}");

                SelectedSymbols[reelSymbol1] = SymbolString.CHERRY;
                SelectedSymbols[reelSymbol2] = SymbolString.CHERRY;
                SelectedSymbols[reelSymbol3] = SymbolString.CHERRY;
                
                PrizeValue = GameControl.BetAmount * _TRIPLE_CHERRY_MULTIPLIER;
                
                break;
            }
            // double cherry 7% chance
            case < 7000:
            {
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                SelectedSymbols[reelSymbol1] = SymbolString.CHERRY;
                SelectedSymbols[reelSymbol2] = SymbolString.CHERRY;

                // possible random symbols (cherry already shown so excluded)
                string[] randomSymbol = {"Diamond", "Crown", "MELON", "Bar", "Seven", "Lemon"};
                int randomSymbolIndex = Random.Range(0, randomSymbol.Length);

                Debug.Log($"DOUBLE CHERRY symbols {SymbolString.CHERRY}:{SymbolString.CHERRY}:{randomSymbol[randomSymbolIndex]}");
                
                SelectedSymbols[reelSymbol3] = randomSymbol[randomSymbolIndex];

                PrizeValue = GameControl.BetAmount * _DOUBLE_CHERRY_MULTIPLIER;
                
                break;
            }
            default: // no winning symbols, generate random symbols that are not a winning match
            {
                // starts at capacity 7 because the constructor count is 7
                List<string> nonWinSymbolList = new List<string>(SymbolString.SymbolArray);
                
                // determine which slot will display the symbols, 0-2
                int[] reelSymbolArray = AdjustSymbolsIfEqual(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
                int reelSymbol1 = reelSymbolArray[0];
                int reelSymbol2 = reelSymbolArray[1];
                int reelSymbol3 = reelSymbolArray[2];
                
                int randomSymbolIndex1 = Random.Range(0, nonWinSymbolList.Count); // 0-6
                SelectedSymbols[reelSymbol1] = nonWinSymbolList[randomSymbolIndex1];
                
                nonWinSymbolList.RemoveAt(randomSymbolIndex1); // cannot display previous symbol
                nonWinSymbolList.TrimExcess(); // we are not at a capacity of 6

                int randomSymbolIndex2 = Random.Range(0, nonWinSymbolList.Count); // 0-5
                SelectedSymbols[reelSymbol2] = nonWinSymbolList[randomSymbolIndex2];
                
                nonWinSymbolList.RemoveAt(randomSymbolIndex2); // cannot display previous symbol
                nonWinSymbolList.TrimExcess(); // we are not at a capacity of 5
                
                int randomSymbolIndex3 = Random.Range(0, nonWinSymbolList.Count); // 0-4
                SelectedSymbols[reelSymbol3] = nonWinSymbolList[randomSymbolIndex3];
                
                Debug.Log($"No win symbols: {SelectedSymbols[reelSymbol1]}:{SelectedSymbols[reelSymbol2]}:{SelectedSymbols[reelSymbol3]}");

                PrizeValue = 0;
                break;
            }
        }
        
        //Debug.Log($"Prize value after switch: {PrizeValue}");
    }

    private static int[] AdjustSymbolsIfEqual(int reelSymbol1, int reelSymbol2, int reelSymbol3)
    {
        // Check if all reel symbol indices are the same. If so, adjust accordingly. Two could still be the equal.
        if (reelSymbol1 == reelSymbol2 && reelSymbol1 == reelSymbol3 && reelSymbol1 == 0)
        {
            if (Random.value < 0.5f)
            {
                reelSymbol2 += 1; // second symbol at slot 2
                reelSymbol3 = 2; // third random symbol at slot 3
            }
            else
            {
                reelSymbol2 += 2; // second symbol at slot 3
                reelSymbol3 = 1; // third random symbol at slot 2
            }
        }
        else if (reelSymbol1 == reelSymbol2 && reelSymbol1 == reelSymbol3 && reelSymbol1 == 1)
        {
            if (Random.value < 0.5f)
            {
                reelSymbol2 -= 1; // second symbol at slot 1
                reelSymbol3 = 2; // third random symbol at slot 3
            }
            else // 1st and third slot 
            {
                reelSymbol2 += 1; // second symbol at slot 3
                reelSymbol1 = 0; // third random symbol at slot 1
            }
        }
        else if (reelSymbol1 == reelSymbol2 && reelSymbol1 == reelSymbol3 && reelSymbol1 == 2)
        {
            if (Random.value < 0.5f)
            {
                reelSymbol2 -= 2; // second symbol at slot 1
                reelSymbol3 = 1; // third random symbol at slot 2
            }
            else
            {
                reelSymbol2 -= 1; // second symbol at slot 2
                reelSymbol3 = 0; // third random symbol at slot 1
            }
        }

        // Reel symbol indices are not the same now but two could be. Adjust if so
        if (reelSymbol1 == reelSymbol2)
        {
            // adjust reelSymbol1 and reelSymbol3 based on what reelSymbol3 is as its already in a valid index
            if (reelSymbol1 == 0) // Here, reelSymbol3 CANNOT be 0, only 1 or 2
            {
                if (reelSymbol3 == 1) // 0 0 1 situation
                {
                    reelSymbol2 = 2; // 0 2 1 now
                }
                else // 0 0 2 situation
                {
                    reelSymbol2 = 1; // 0 1 2 now
                }
            }
            else if (reelSymbol1 == 1) // Here, reelSymbol3 CANNOT be 1, only 0 or 2
            {
                if (reelSymbol3 == 0) // 1 1 0 situation
                {
                    reelSymbol2 = 2; // 1 2 0 now
                }
                else // 1 1 2 situation
                {
                    reelSymbol2 = 0; // 1 0 2 now
                }
            }
            else if (reelSymbol1 == 2) // Here, reelSymbol3 CANNOT be 2, only 0 or 1
            {
                if (reelSymbol3 == 0) // 2 2 0 situation
                {
                    reelSymbol2 = 1; // 2 1 0 now
                }
                else // 2 2 1 situation
                {
                    reelSymbol2 = 0; // 2 0 1 now
                }
            }
        }
        else if (reelSymbol2 == reelSymbol3)
        {
            // adjust reelSymbol2 and reelSymbol3 based on what reelSymbol1 is as its already in a valid index
            if (reelSymbol2 == 0) // Here, reelSymbol1 CANNOT be 0, only 1 or 2.
            {
                if (reelSymbol1 == 1) // 1 0 0 situation
                {
                    reelSymbol3 = 2; // 1 0 2 now
                }
                else // 2 0 0 situation
                {
                    reelSymbol3 = 1; // 2 0 1 now
                }
            }
            else if (reelSymbol2 == 1) // reelSymbol1 CANNOT be 1, only 0 or 2.
            {
                if (reelSymbol1 == 0) // 0 1 1 situation
                {
                    reelSymbol3 = 2; // 0 1 2 now
                }
                else // 2 1 1 situation
                {
                    reelSymbol3 = 0; // 2 1 0 now
                }
            }
            else if (reelSymbol2 == 2) // reelSymbol1 CANNOT be 2, only 0 or 1
            {
                if (reelSymbol1 == 0) // 0 2 2 situation
                {
                    reelSymbol3 = 1; // 0 2 1 now
                }
                else // 1 2 2 situation
                {
                    reelSymbol3 = 0; // 1 2 0 now
                }
            }
        }
        else if (reelSymbol1 == reelSymbol3)
        {
            // adjust reelSymbol1 and reelSymbol3 based on what reelSymbol2 is as its already in a valid index
            if (reelSymbol1 == 0) // Here, reelSymbol2 CANNOT be 0, only 1 or 2
            {
                if (reelSymbol2 == 1) // 0 1 0 situation
                {
                    reelSymbol3 = 2; // 0 1 2 now
                }
                else // 0 2 0 situation
                {
                    reelSymbol3 = 1; // 0 2 1 now
                }
            }
            else if (reelSymbol1 == 1) // Here, reelSymbol2 CANNOT be 1, only 0 or 2
            {
                if (reelSymbol2 == 0) // 1 0 1 situation
                {
                    reelSymbol3 = 2; // 1 0 2 now
                }
                else // 1 2 1 situation
                {
                    reelSymbol3 = 0; // 1 2 0 now
                }
            }
            else if (reelSymbol1 == 2) // Here, reelSymbol2 CANNOT be 2, only 0 or 1
            {
                if (reelSymbol2 == 0) // 2 0 2 situation
                {
                    reelSymbol3 = 1; // 2 0 1 now
                }
                else // 2 1 2 situation
                {
                    reelSymbol3 = 0; // 2 1 0 now
                }
            }
        }

        return new int[] {reelSymbol1, reelSymbol2, reelSymbol3};
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
