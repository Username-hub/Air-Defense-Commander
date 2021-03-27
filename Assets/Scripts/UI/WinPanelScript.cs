using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.UI
{
    public class WinPanelScript : MonoBehaviour
    {
        public void ToLevelChoseButton()
        {
            SceneManager.LoadScene("LevelChoseScene");
        }
        public void NextLevelButton(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}