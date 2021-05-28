using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    
    [Header("Keys")]
    [SerializeField] private string saveVolumeKey;
    
    [Header("Tags")]
    [SerializeField] private string sliderTag;
    [SerializeField] private string textVolumeTag;
    
    [Header("Parameters")]
    [SerializeField] private float volume;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(this.saveVolumeKey))
        {
            this.volume = PlayerPrefs.GetFloat(this.saveVolumeKey);
            this.audio.volume = this.volume;
            
            GameObject sliderObject = GameObject.FindWithTag(this.sliderTag);
            if (sliderObject != null)
            {
                this.slider = sliderObject.GetComponent<Slider>();
                this.slider.value = this.volume;
            }
            else
            {
                this.volume = 0.5f;
                PlayerPrefs.SetFloat(this.saveVolumeKey, this.volume);
                this.audio.volume = this.volume;
            }
        }
    }

    private void LateUpdate()
    {
        GameObject sliderObject = GameObject.FindWithTag(this.sliderTag);
        if (sliderObject != null)
        {
            this.slider = sliderObject.GetComponent<Slider>();
            this.volume = slider.value;

            if (this.audio.volume != this.volume)
            {
                PlayerPrefs.SetFloat(this.saveVolumeKey, this.volume);
            }

            GameObject textObject = GameObject.FindWithTag(this.textVolumeTag);
            if (textObject != null)
            {
                this.text = textObject.GetComponent<Text>();
                this.text.text = Math.Round(this.volume * 100) + "%";
            }
        }

        this.audio.volume = this.volume;
    }
}
