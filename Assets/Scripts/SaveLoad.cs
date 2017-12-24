using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad 
{
	
	public static void Save(IceTowerUpgrade upgradeInfo)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		if (File.Exists(Path.Combine(Application.persistentDataPath, upgradeInfo.towerName + ".ug")))
		{
			file = File.Open(Path.Combine(Application.persistentDataPath, upgradeInfo.towerName + ".ug"), FileMode.Truncate);
		}
		else
		{
			file = File.Create(Path.Combine(Application.persistentDataPath, upgradeInfo.towerName + ".ug"));
		}
		bf.Serialize(file, upgradeInfo);
		file.Close();
	}


	public static void Delete(string towerName)
	{
		if (File.Exists(Path.Combine(Application.persistentDataPath, towerName + ".ug")))
		{
			File.Delete(Path.Combine(Application.persistentDataPath, towerName + ".ug"));
		}
	}


	public static IceTowerUpgrade Load(string towerName)
	{
		if (File.Exists(Path.Combine(Application.persistentDataPath, towerName + ".ug")))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Path.Combine(Application.persistentDataPath, towerName + ".ug"), FileMode.Open);
			IceTowerUpgrade upgradeInfo = (IceTowerUpgrade)bf.Deserialize(file);
			file.Close();
			return upgradeInfo;
		}
		return new IceTowerUpgrade(towerName);
	}
}
