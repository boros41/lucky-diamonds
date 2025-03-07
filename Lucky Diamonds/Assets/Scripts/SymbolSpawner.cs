using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SymbolSpawner : MonoBehaviour
{
    private const int INITIAL_X = -2;
    private const int OFFSET_X = 2;
    private const int INITIAL_Y = 1;
    private const int OFFSET_Y = 1;
    
    [SerializeField] private List<GameObject> spritePrefabs; // Assign the prefabs in the Inspector
    [HideInInspector] public static List<GameObject> originalSymbolPrefabs; // Assign the prefabs in the Inspector
    [HideInInspector] public static List<GameObject> availableSpritePrefabs;
    
    public static GameObject[,] symbolBatch1 = new GameObject[3, 3];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSymbolPrefabs = spritePrefabs;
        
        SpawnBatch(symbolBatch1);
    }
    
    /*  Instantiate random unique symbols on each row at the start of the game. Nine symbols will be visible at all times. 
     *
     *  Parameters:
     *  batch - 3x3 array containing the nine initial random symbols 
     */
    private void SpawnBatch(GameObject[,] batch)
    {
        for (int i = 0; i < 3; i++)
        {
            availableSpritePrefabs = new List<GameObject>(spritePrefabs); // repopulate List
            for (int j = 0; j < 3; j++)
            {
                int randomSymbolIndex = Random.Range(0, availableSpritePrefabs.Count);
                
                switch (i)
                {
                    case 0: // first row
                        batch[i, j] = Instantiate(availableSpritePrefabs[randomSymbolIndex], new Vector3(INITIAL_X + (OFFSET_X * j), INITIAL_Y, 5), Quaternion.identity);
                        availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        break;
                    case 1: // second row
                        batch[i, j] = Instantiate(availableSpritePrefabs[randomSymbolIndex], new Vector3(INITIAL_X + (OFFSET_X * j), INITIAL_Y - OFFSET_Y, 5), Quaternion.identity);
                        availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        break;
                    case 2: // third row
                        batch[i, j] = Instantiate(availableSpritePrefabs[randomSymbolIndex], new Vector3(INITIAL_X + (OFFSET_X * j), INITIAL_Y - (OFFSET_Y * 2), 5), Quaternion.identity);
                        availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        break;
                }
                
            }
        }
        availableSpritePrefabs = new List<GameObject>(spritePrefabs); // initial spawn finished, repopulate List
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
