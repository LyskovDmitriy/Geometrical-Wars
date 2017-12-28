using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour 
{

	private Slider volumeSlider;
	private AudioSource audioSource;
	private AudioManager manager;


	public void OnChangeVolume(float volume)
	{
		audioSource.volume = volume;
		manager.initialVolume = volume;
	}

	
	void Awake()
	{
		volumeSlider = GetComponent<Slider>();
	}


	void Start()
	{
		manager = FindObjectOfType<AudioManager>();
		audioSource = manager.GetComponent<AudioSource>();
		volumeSlider.value = manager.initialVolume;
	}
}
