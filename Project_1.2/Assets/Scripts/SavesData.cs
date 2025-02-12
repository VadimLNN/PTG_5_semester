using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavesData : MonoBehaviour
{
    private static int? level;

    // ���������� ������
    public static void Save(int lvlToSave)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.level = lvlToSave;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    // ���������� ��������� �������� �������
    public static int LastOpenedLevel()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath
                + "/MySaveData.dat", FileMode.Open);
            if (file.Length != 0)
            {
                SaveData data = (SaveData)bf.Deserialize(file);
                level = data.level;
                Debug.Log("Game data loaded!");
            }
            else
            {
                level = 0;
                Debug.Log("Game data created!");
            }
            
            file.Close();
            

            return Convert.ToInt32(level);
        }
        else
            return -1;
    }

    // ������� ���� � ������������ 
    public static void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");

            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }

    // ���������� �������� ������� 
    public static int CurrentLevel() => SceneManager.GetActiveScene().buildIndex;

    // ���������� ����� ���������� ������� 
    public static int CountLevels() => SceneManager.sceneCountInBuildSettings - 1;
}

// ����� ��� �������� ������ � ���� � ������������ 
[Serializable]
class SaveData
{
    public int level;
}
