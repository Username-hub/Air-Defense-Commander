using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.UI
{
    public class GameOverPanelScript : MonoBehaviour
    {
        public void ToMenu()
        {
            SceneManager.LoadScene("LevelChoseScene");
        }
        public void Reload()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}