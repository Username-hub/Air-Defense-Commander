using UnityEngine;

namespace DefaultNamespace.UI
{
    public class UIHandler : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject pauseMenuPrefab;
        
        public void PauseButtonPressed()
        {
            GameObject pauseMenuObject = Instantiate(pauseMenuPrefab, transform);
            PauseMenuScript pauseMenuScript = pauseMenuObject.GetComponent<PauseMenuScript>();
            pauseMenuScript.gameManager = gameManager;
            pauseMenuScript.PauseGame();
        }
    }
}