using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteProgress : MonoBehaviour 
{

	public string[] towerNames;


	private MainMenu mainMenu;

	
	public void DeleteAllProgress()
	{
		PlayerPrefs.DeleteAll();
		foreach (string tower in towerNames) //delete all files with tower upgrades
		{
			SaveLoad.Delete(tower);
		}
		mainMenu.Start();
	}


	void Awake()
	{
		mainMenu = FindObjectOfType<MainMenu>();
	}
}
