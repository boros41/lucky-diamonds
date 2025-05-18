using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static event Action SpinButtonPressed;
    //public static event Action BetButtonPressed; // TO-DO: invoke for bet script subscriber to increase play amount

    private static InputManager instance { get; set; }
    private AudioSource _musicAudioSource;
    private readonly GameObject[] _volumeSliderGroup = new GameObject[3];
    private bool _isMuted;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _musicAudioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();

        _volumeSliderGroup[0] = GameObject.Find("VolumeSliderBackground");
        _volumeSliderGroup[1] = GameObject.Find("VolumeSliderBackground/VolumeSliderIcon");
        _volumeSliderGroup[2] = GameObject.Find("VolumeSliderBackground/VolumeSlider");

        HideVolumeSlider();
    }

    // Update is called once per frame
    void Update()
    {
        // restart game
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }
    
    public void OnSpinClick()
    {
        if (GameObject.Find("SpinButton") != null)
        {
            GameObject.Find("SpinButton").GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.LogWarning("SpinButton GameObject not found");
        }
        
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
        if (GameObject.Find("BetButton") != null)
        {
            GameObject.Find("BetButton").GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.LogWarning("BetButton GameObject not found");
        }

        if (SpinSymbol.isSpinning) return; // cannot spin while spinning
        
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

    public void OnVolumeButtonClicked()
    {
        if (_volumeSliderGroup[0].activeSelf)
        {
            HideVolumeSlider();
        }
        else
        {
            ShowVolumeSlider();
        }
    }
    
    private void ToggleMute()
    {
        if (_isMuted)
        {
            _musicAudioSource.mute = false;
            _isMuted = false;
        }
        else
        {
            _musicAudioSource.mute = true;
            _isMuted = true;
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _volumeSliderGroup[2].GetComponent<Slider>().value; // change volume of entire game
    }
    
    private void ShowVolumeSlider()
    {
        foreach (GameObject currentVolumeElement in _volumeSliderGroup)
        {
            currentVolumeElement.SetActive(true);
        }
    }

    private void HideVolumeSlider()
    {
        foreach (GameObject currentVolumeElement in _volumeSliderGroup)
        {
            currentVolumeElement.SetActive(false);
        }
    }
    
}
