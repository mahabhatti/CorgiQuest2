using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Drag your PopUpCanvas prefab here")]
    public GameObject PopUpCanvasPrefab;    // ← Assign your PopUpCanvas prefab

    private GameObject PopUpCanvas;         // the instantiated canvas
    private GameObject PopUpPanel;          // the dimming panel under the canvas
    private TMP_Text PopUpText;           // the Text component that shows the message

    private void Awake()
    {
        // ─── Singleton setup ─────────────────────────────────────
        Debug.Log("[UIManager] Instance before assignment = " + Instance, this);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Debug.Log("[UIManager] Instance after assignment = " + Instance, this);
        DontDestroyOnLoad(gameObject);

        // ─── Prefab reference check ──────────────────────────────
        if (PopUpCanvasPrefab == null)
        {
            Debug.LogError("[UIManager] PopUpCanvasPrefab is NULL! Assign it in the Inspector.", this);
            return;
        }

        // ─── Instantiate the canvas and make it persist ──────────
        PopUpCanvas = Instantiate(PopUpCanvasPrefab);
        DontDestroyOnLoad(PopUpCanvas);

        // ─── Find the PopUpPanel by name ────────────────────────
        var panelTransform = PopUpCanvas.transform.Find("PopUpPanel");
        if (panelTransform == null)
        {
            Debug.LogError("[UIManager] Could not find a child named 'PopUpPanel' in the prefab.", PopUpCanvas);
            return;
        }
        PopUpPanel = panelTransform.gameObject;

        // ─── Find the PopUpText under the panel ─────────────────
        var textTransform = panelTransform.Find("PopUpText");
        if (textTransform == null)
        {
            Debug.LogError("[UIManager] Could not find a child named 'PopUpText' under PopUpPanel.", PopUpPanel);
            return;
        }
        PopUpText = textTransform.GetComponent<TMP_Text>();
        if (PopUpText == null)
        {
            Debug.LogError("[UIManager] 'PopUpText' has no Text component!", textTransform);
            return;
        }

        // ─── Hide the panel at start ────────────────────────────
        PopUpPanel.SetActive(false);
    }

    /// <summary>
    /// Shows the popup message for 'duration' seconds.
    /// </summary>
    public void ShowPopup(string message, float duration = 2f)
    {
        if (PopUpPanel == null || PopUpText == null) return;

        PopUpText.text = message;
        PopUpPanel.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(HideAfter(duration));
    }

    private IEnumerator HideAfter(float wait)
    {
        yield return new WaitForSeconds(wait);
        PopUpPanel.SetActive(false);
    }
}
