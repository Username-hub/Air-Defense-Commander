using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.UI
{
    public class PauseMenuScript : MonoBehaviour
    {
        public GameManager gameManager;

        public void PauseGame()
        {
            gameManager.PauseGame();
        }
        public void ResumeButton()
        {
            gameManager.UnpauseGame();
            Destroy(gameObject);
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}