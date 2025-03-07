using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSymbol : MonoBehaviour
{
    public LeanTweenType easeType;
    private int spinDuration = 3;
    [HideInInspector] public static bool isSpinning;
    
    [SerializeField] private SymbolSpawner symbolSpawner;
    
    private float speed = 2f; // Scroll speed
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSpinning = false; // reel 1 not spinning yet
        GameControl.SpinButtonPressed += StartSpinning;
    }

    private void StartSpinning()
    {
        StartCoroutine("SpinReel1");
    }

    private IEnumerator SpinReel1()
    {
        isSpinning = true;
        float spinTime = 0f;
        float maxSpinTime = spinDuration;
        float[] stopTimes = ReelStopTimes(spinDuration);
        
        //string selectedSymbol1 = RandomNumberGenerator.SelectedSymbols[0];
        
        Debug.Log("Spinning");

        while (spinTime < maxSpinTime)
        {
            for (int i = 0; i < 3; i++)
            {
                symbolSpawner.availableSpritePrefabs = new List<GameObject>(symbolSpawner.spritePrefabs); // repopulate List
                for (int j = 0; j < 3; j++)
                {
                    if (spinTime >= stopTimes[j])
                    {
                        continue;
                    }
                    
                    
                    SymbolSpawner.symbolBatch1[i, j].transform.position += Vector3.down * (speed * Time.deltaTime);
                    
                    // If a symbol reaches the bottom threshold, replace it
                    if (SymbolSpawner.symbolBatch1[i, j].transform.position.y <= -1.5f)
                    {
                        LeanTween.cancel(SymbolSpawner.symbolBatch1[i, j]); // Ensure no duplicate tweens
                        
                        // Destroy the old symbol
                        Destroy(SymbolSpawner.symbolBatch1[i, j]);

                        int randomSymbolIndex = Random.Range(0, symbolSpawner.availableSpritePrefabs.Count);

                        // Spawn a new symbol at the top (y = 1.5)
                        Vector3 spawnPosition = new Vector3(SymbolSpawner.symbolBatch1[i, j].transform.position.x, 1.5f, SymbolSpawner.symbolBatch1[i, j].transform.position.z);
                        SymbolSpawner.symbolBatch1[i, j] = Instantiate(symbolSpawner.availableSpritePrefabs[randomSymbolIndex], spawnPosition, Quaternion.identity);
                        
                        symbolSpawner.availableSpritePrefabs.RemoveAt(randomSymbolIndex);

                    }
                }
            }

            spinTime += Time.deltaTime; // Increment the spin timer
            Debug.Log("Time: " + spinTime);

            yield return null; // Wait until the next frame
        }
        
        
        isSpinning = false;
        Debug.Log("Spinning Stopped");
    }

    private float[] ReelStopTimes(int duration)
    {
        return new float[] {duration / 3f, duration * 2 / 3f, duration};
    }

    private void SmoothStop(int i, int j)
    {

    }
    
    private void OnDestroy()
    {
        GameControl.SpinButtonPressed -= StartSpinning;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}