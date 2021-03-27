using System;
using System.Collections.Generic;
using DefaultNamespace.SaveLoadSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.Menu
{
    public class LevelChoseScript : MonoBehaviour
    {
        [SerializeField]
        public List<Button> levelChoseButtons;

        private void Start()
        {
            LevelSaveData levelSaveData = SaveSystem.LoadProgress();
            if (levelSaveData.firstGameStart)
            {
                levelSaveData.levelStates = new LevelState[levelChoseButtons.Count];
                levelSaveData.levelStates[0] = LevelState.Open;
                for (int i = 1; i < levelSaveData.levelStates.Length; i++)
                {
                    levelSaveData.levelStates[i] = LevelState.Blocked;
                }

                levelSaveData.firstGameStart = false;
                SaveSystem.SaveProgress(levelSaveData);
            }
            
            for (int i = 0; i < levelSaveData.levelStates.Length; i++)
            {
                if (levelSaveData.levelStates[i] == LevelState.Open)
                    levelChoseButtons[i].interactable = true;
                else if (levelSaveData.levelStates[i] == LevelState.Blocked)
                { 
                    levelChoseButtons[i].interactable = false;
                }
                else if (levelSaveData.levelStates[i] == LevelState.Finished)
                {
                    levelChoseButtons[i].interactable = true;
                }
            }
        }

        public void LoadNewScene(String sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}