using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Menu
{
    public class MainMenuScript : MonoBehaviour
    {
        public void LoadNewScene(String sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}