using System.Collections.Generic;
using UnityEngine;

public static class SymbolString
{
    public static readonly string[] SymbolArray = { DIAMOND, CROWN, MELON, BAR, SEVEN, CHERRY, LEMON };

    public const string DIAMOND = "Diamond";
    public const string CROWN = "Crown";
    public const string MELON = "Melon";
    public const string BAR = "Bar";
    public const string SEVEN = "Seven";
    public const string CHERRY = "Cherry";
    public const string LEMON = "Lemon";
    
    /*
    public enum SymbolPositions
    {
        // y-positions of each symbol on the reel
        DiamondTop = -7,
        Crown = -5,
        Watermelon = -3,
        Bar = -1,
        Seven = 1,
        Cherry = 3,
        Lemon = 5,
        DiamondBottom = 7
    }
    */
    /*
        public static readonly Dictionary<string, int> SYMBOL_TO_POSITION = new()
        {
            {"Diamond", (int)SymbolPositions.DiamondTop},
            { "Crown", (int)SymbolPositions.Crown },
            { "Watermelon", (int)SymbolPositions.Watermelon },
            { "Bar", (int)SymbolPositions.Bar },
            { "Seven", (int)SymbolPositions.Seven },
            { "Cherry", (int)SymbolPositions.Cherry },
            { "Lemon", (int)SymbolPositions.Lemon },
        };
        */
    
    
    
    
    /*
    public const int UPPER_BOUND = -7; // y position of the top symbol of a reel
    public const int LOWER_BOUND = 7; // y position of the bottom symbol of a reel
    
    public const float TIME_BETWEEN_SPIN = 0.01f;
    public const float SPIN_SPEED = 5f;

    public const float SYMBOL_STEP = 0.25f;
    */
}
