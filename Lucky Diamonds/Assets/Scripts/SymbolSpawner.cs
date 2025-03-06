using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SymbolSpawner : MonoBehaviour
{
    public List<GameObject> availableSpritePrefabs; // Assign the prefab in the Inspector
    private IReadOnlyList<GameObject> spritePrefabs; // Assign the prefab in the Inspector

    private Vector3 spawnPosition = new Vector3(0, 0, 5); // Set spawn position
    
    private GameObject[,] symbolsBatch = new GameObject[3, 3];
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spritePrefabs = availableSpritePrefabs;
        Debug.LogFormat("Read only, {0}: {1}", spritePrefabs.Count, string.Join(", ", spritePrefabs));
        
        const int INITIAL_X = -2;
        const int OFFSET_X = 2;
        const int INITIAL_Y = 1;
        const int OFFSET_Y = 1;
        
        for (int i = 0; i < 3; i++)
        {
            availableSpritePrefabs = new List<GameObject>(spritePrefabs); // repopulate List
            for (int j = 0; j < 3; j++)
            {
                int randomSymbolIndex = Random.Range(0, availableSpritePrefabs.Count);
                
                switch (i)
                {
                    case 0: // first row
                        symbolsBatch[i, j] = Instantiate(availableSpritePrefabs[randomSymbolIndex], new Vector3(INITIAL_X + (OFFSET_X * j), INITIAL_Y, 5), Quaternion.identity);
                        availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        
                        Debug.LogFormat("[{0}][{1}], {2}: {3}", i, j, availableSpritePrefabs.Count, string.Join(", ", availableSpritePrefabs));
                        
                        break;
                    case 1: // second row
                        symbolsBatch[i, j] = Instantiate(availableSpritePrefabs[randomSymbolIndex], new Vector3(INITIAL_X + (OFFSET_X * j), INITIAL_Y - OFFSET_Y, 5), Quaternion.identity);
                        availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        
                        Debug.LogFormat("[{0}][{1}], {2}: {3}", i, j, availableSpritePrefabs.Count, string.Join(", ", availableSpritePrefabs));

                        
                        break;
                    case 2: // third row
                        symbolsBatch[i, j] = Instantiate(availableSpritePrefabs[randomSymbolIndex], new Vector3(INITIAL_X + (OFFSET_X * j), INITIAL_Y - (OFFSET_Y * 2), 5), Quaternion.identity);
                        availableSpritePrefabs.RemoveAt(randomSymbolIndex);
                        
                        Debug.LogFormat("[{0}][{1}], {2}: {3}", i, j, availableSpritePrefabs.Count, string.Join(", ", availableSpritePrefabs));
                        
                        break;
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
