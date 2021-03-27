using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DefaultNamespace.SaveLoadSystem
{
    public static class SaveSystem
    {
        public static void SaveProgress(LevelSaveData levelSaveData)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            String path = Application.persistentDataPath + "save.bin";
            FileStream fileStream = new FileStream(path, FileMode.Create);
            binaryFormatter.Serialize(fileStream, levelSaveData);
            fileStream.Close();
        }
        public static void SaveProgress(int levelNumber)
        {
            LevelSaveData levelSaveData = LoadProgress();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            String path = Application.persistentDataPath + "save.bin";
            FileStream fileStream = new FileStream(path, FileMode.Create);
            levelSaveData.LevelFinished(levelNumber);
            binaryFormatter.Serialize(fileStream, levelSaveData);
            fileStream.Close();
        }

        public static LevelSaveData LoadProgress()
        {
            String path = Application.persistentDataPath + "save.bin";
            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(path, FileMode.Open);
                if (fileStream.Length == 0)
                {
                    fileStream.Close();
                    return new LevelSaveData(true);
                }

                LevelSaveData levelSaveData = binaryFormatter.Deserialize(fileStream) as LevelSaveData;
                fileStream.Close();
                return levelSaveData;
            }
            else
            {
                return new LevelSaveData(true);
            }
        }
    }
}