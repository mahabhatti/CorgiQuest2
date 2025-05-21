using UnityEngine;
using System.Collections;

public class LossManager : MonoBehaviour
{
    [Header("Assign your LossPanel here")]
    public GameObject lossPanel;

    private bool isLossShown = false;

    private void Start()
    {
        Debug.Log("[LossManager] Start – panel = " + lossPanel, this);
        if (lossPanel != null)
            lossPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("[LossManager] L pressed", this);
            ShowLoss();
        }
    }

    public void ShowLoss()
    {
        Debug.Log("[LossManager] ShowLoss() called; panel = " + lossPanel, this);

        if (lossPanel == null)
        {
            Debug.LogError("[LossManager] lossPanel is NULL! Assign it in the Inspector.", this);
            return;
        }
        if (isLossShown)
        {
            Debug.Log("[LossManager] loss already shown", this);
            return;
        }

        Time.timeScale    = 0f;
        lossPanel.SetActive(true);
        isLossShown = true;
        Debug.Log("[LossManager] lossPanel.SetActive(true)", this);
    }

    public void HideLoss()
    {
        if (!isLossShown || lossPanel == null) return;
        Time.timeScale    = 1f;
        lossPanel.SetActive(false);
        isLossShown       = false;
    }
}