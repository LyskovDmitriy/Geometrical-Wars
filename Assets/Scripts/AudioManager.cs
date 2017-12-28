using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour 
{

	public static AudioManager audioManager;

	public AudioClip[] menuThemes;
	public AudioClip[] battleThemes;
	public float timeToVolumeDown;
	

	private AudioSource audioSource;
	private bool subscribedToSceneLoad;
	private bool isPlayingMenuTheme;


	public IEnumerator Play(AudioClip[] clips)
	{
		if (audioSource.isPlaying)
		{
			float initialVolume = audioSource.volume;
			while (audioSource.volume > 0.0f)
			{
				audioSource.volume -= initialVolume * Time.deltaTime;
				yield return null;
			}

			audioSource.Stop();
			audioSource.volume = initialVolume;
		}

		audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
	}


	void Awake()
	{
		if (audioManager == null)
		{
			audioManager = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
			subscribedToSceneLoad = true;
		}
		else if (audioManager != this)
		{
			Destroy(gameObject);
		}
		audioSource = GetComponent<AudioSource>();
	}


	void Start()
	{
		isPlayingMenuTheme = false;
		OnLevelFinishedLoading(SceneManager.GetActiveScene(), LoadSceneMode.Single);
	}


	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (scene.name.Contains("Level ") && scene.name != "Level Select")
		{
			isPlayingMenuTheme = false;
			StartCoroutine(AudioManager.audioManager.Play(battleThemes));
		}
		else
		{
			if (!isPlayingMenuTheme)
			{
				StartCoroutine(AudioManager.audioManager.Play(menuThemes));
				isPlayingMenuTheme = true;
			}
		}
	}


	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		subscribedToSceneLoad = false;
	}
}
