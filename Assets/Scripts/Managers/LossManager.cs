using UnityEngine;
using System.Collections;

public class LossManager : MonoBehaviour
{
    public static LossManager Instance;
    
    [Header("Assign your LossPanel here")]
    public GameObject lossPanel;

    //tracks whether the loss screen is being displayed
    private bool isLossShown = false;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        if (lossPanel != null)
        {
            //panel shouldn't be visible when the game starts
            lossPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameController.Instance.CurrentGameState == GameState.Loss)
        {
            ShowLoss();
            GameController.Instance.SetGameState(GameState.Overworld);
        }
    }

    public void ShowLoss()
    {

        if (lossPanel == null)
        {
            return;
        }
        if (isLossShown)
        {
            return;
        }
        
        lossPanel.SetActive(true);
        isLossShown = true;
    }

    public void HideLoss()
    {
        // Only hide if it was shown and a panel is assigned.
        if (!isLossShown || lossPanel == null)
        {
            return;
        }
        
        lossPanel.SetActive(false);
        isLossShown = false;
    }
    
    public void Respawn()
    {
        HideLoss();
        GameController.Instance.ResetWorld();
    }
}