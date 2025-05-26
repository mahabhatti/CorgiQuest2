using UnityEngine;
using System.Collections;

public class LossManager : MonoBehaviour
{
    public static LossManager Instance;
    
    [Header("Assign your LossPanel here")]
    public GameObject lossPanel;

    //tracks whether the loss screen is being displayed
    private bool isLossShown = false;

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
        //must change condition
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowLoss();
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

        // Pause game time to show the panel
        Time.timeScale    = 0f;
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
        // Resume game time and hide the panel
        Time.timeScale    = 1f;
        lossPanel.SetActive(false);
        isLossShown       = false;
    }
    
    public void Respawn()
    {
        HideLoss();
        GameController.Instance.ResetWorld();
    }
}