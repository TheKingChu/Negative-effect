using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using System;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// Gonna make the outline here first and then implement it
/// to the GameManager since its easier to see things in an own script
/// -Charlie
/// </summary>
/// 
namespace SaveLoadSystem
{
    /*want to do something like this so that it can be used
     * for checking the bool if there is a savefile or not*/
    //public static bool getSaveFile => SaveData;

   public static class SaveGameManager
   {
        public static SaveData CurrentSaveData = new SaveData();
        public const string SaveDirectory = "/SaveData/";
        public const string FileName = "SaveGame.txt";

        public static UnityAction OnLoadGameStart;
        public static UnityAction OnLoadGameFinish;

        /// <summary>
        /// This makes a save locally and on to our assets in Unity with all data from PlayerSaveData.cs
        /// Local save can be found in: C:\Users\[YOUR USER NAME]\AppData\LocalLow\DefaultCompany\NegativeEffectBackup
        /// Unity save can be found in: Assets/SaveData
        /// </summary>
        /// <returns></returns>
        public static bool SaveGame()
        {
            var dir = Application.dataPath + Path.AltDirectorySeparatorChar + SaveDirectory;
            string fullPath = Application.persistentDataPath + SaveDirectory;
            
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(dir + FileName, json);

            json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(fullPath + FileName, json);

            GUIUtility.systemCopyBuffer = dir;

            return true;
        }

        /// <summary>
        /// Should find the file automatically and load the latest save file
        /// </summary>
        public static void LoadGame()
        {
            OnLoadGameStart?.Invoke();

            var dir = Application.dataPath + Path.AltDirectorySeparatorChar + SaveDirectory;

            string fullPath = Application.persistentDataPath + SaveDirectory;
            SaveData tempData = new SaveData();
            //BinaryFormatter bf = new BinaryFormatter();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (File.Exists(fullPath + FileName))
            {
                string json = File.ReadAllText(fullPath + FileName);
                tempData = JsonUtility.FromJson<SaveData>(json);


                json = JsonUtility.ToJson(tempData, true);
                File.WriteAllText(dir + FileName, json);

                GUIUtility.systemCopyBuffer = dir;

                Debug.Log("Save file does exist!");
            }
            else
            {
                Debug.LogError("Save file does not exist!");
            }

            CurrentSaveData = tempData;
            OnLoadGameFinish?.Invoke();
        }
    }
}

