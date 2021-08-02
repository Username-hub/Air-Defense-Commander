using DefaultNamespace;
using DefaultNamespace.AirBaseScripts;
using DefaultNamespace.PlayerAircraftSripts;
using DefaultNamespace.SaveLoadSystem;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.acive;
        Time.timeScale = 1.0f;
    }

    public bool isPathMaking = false;
    // Update is called once per frame
    public GameObject mainUiCanvas;
    void Update()
    {
        if (Input.touchCount > 0 && gameState == GameState.acive)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchColider = TouchControlHandler.getOnTouchCollider(touch);
                if (touchColider != null)
                {
                    if (touchColider.tag == "aircraft")
                    {
                        touchColider.gameObject.GetComponent<PlayerAircraftScript>().pathMakingHandler.StartPathMaking();
                    }else if (touchColider.tag == "Base")
                    {
                        touchColider.gameObject.GetComponent<AirBaseScript>().SpawnBaseUI(mainUiCanvas);
                    }
                }
                
            }
            if (touch.phase == TouchPhase.Moved)
            {
            }
        }
    }
    
    public GameObject gameOverPanelPrefab;

    public void GameOver()
    {
        Time.timeScale = 0;
        Instantiate(gameOverPanelPrefab, mainUiCanvas.transform);
        gameState = GameState.paused;
    }

    public int levelId;
    public GameObject WinScreen;
    public void LevelWin()
    {
        Time.timeScale = 0;
        SaveSystem.SaveProgress(levelId);
        Instantiate(WinScreen, mainUiCanvas.transform);
        gameState = GameState.paused;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState = GameState.paused;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gameState = GameState.acive;
    }

    public enum GameState
    {
        paused,
        acive
    }
}
