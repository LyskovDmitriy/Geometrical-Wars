using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour 
{

	private Slider volumeSlider;
	private AudioSource audioSource;


	public void OnChangeVolume(float volume)
	{
		audioSource.volume = volume;
	}

	
	void Awake()
	{
		volumeSlider = GetComponent<Slider>();
	}


	void Start()
	{
		AudioManager manager = FindObjectOfType<AudioManager>();
		audioSource = manager.GetComponent<AudioSource>();
		volumeSlider.value = audioSource.volume;
	}
}
