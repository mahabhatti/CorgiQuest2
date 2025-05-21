using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryPanel;
    private bool isVictoryShown = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    
    public void ShowVictory()
    {
        Debug.Log("[VictoryManager] ShowVictory() running; victoryPanel = " + victoryPanel, this);

        if (victoryPanel == null || isVictoryShown) return;
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        isVictoryShown = true;
    }

    
    public void HideVictory()
    {
        if (victoryPanel == null || !isVictoryShown) return;

        Time.timeScale = 1f;
        victoryPanel.SetActive(false);
        isVictoryShown = false;
    }
    
    private void Update()
    {
        Debug.Log($"[VictoryManager] Update running in scene '{UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}'", this);

        
        //Placeholder for WIN
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("[VictoryManager] V pressed", this);
            ShowVictory();
        }
    }

}