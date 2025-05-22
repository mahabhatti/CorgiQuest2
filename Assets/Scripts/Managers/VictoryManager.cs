using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryPanel;
    //tracks whether the victory screen is being displayed
    private bool isVictoryShown = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (victoryPanel != null)
        {
            //panel shouldn't be visible when the game starts
            victoryPanel.SetActive(false);
        }
        
    }
    private void Update()
    {

        
        //Placeholder for WIN
        if (Input.GetKeyDown(KeyCode.V))
        {
            ShowVictory();
        }
    }
    
    public void ShowVictory()
    {
        // Pause game time to show the panel
        if (victoryPanel == null || isVictoryShown)
        {
            return;
        }
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        isVictoryShown = true;
    }

    
    public void HideVictory()
    {
        // Only hide if it was shown and a panel is assigned
        if (victoryPanel == null || !isVictoryShown)
        {
            return;
        }

        // Resume game time and hide the panel.

        Time.timeScale = 1f;
        victoryPanel.SetActive(false);
        isVictoryShown = false;
    }

}