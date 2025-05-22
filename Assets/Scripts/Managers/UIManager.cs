using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject PopUpCanvasPrefab;   //prefab for the canvas pop up
    private GameObject PopUpCanvas;         // canvas for the pop up
    private GameObject PopUpPanel;          // the dimming panel under the canvas
    private TMP_Text PopUpText;           // the message to show up

    private void Awake()
    {
    // Singleton initialization       
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // checking the prefab
        if (PopUpCanvasPrefab == null)
        {
            return;
        }

        // Instantiate the canvas
        PopUpCanvas = Instantiate(PopUpCanvasPrefab);
        DontDestroyOnLoad(PopUpCanvas);

        // find the panel
        if (canvasSetUp(out var panelTransform))
        {
            return;
        }

        // find the text
        if (textSetUp(panelTransform))
        {
            return;
        }

        //Hide the panel at start of the game
        PopUpPanel.SetActive(false);
    }

    private bool textSetUp(Transform panelTransform)
    {
        var textTransform = panelTransform.Find("PopUpText");
        if (textTransform == null)
        {
            return true;
        }
        //text mesh pro implementation
        PopUpText = textTransform.GetComponent<TMP_Text>();
        if (PopUpText == null)
        {
            return true;
        }

        return false;
    }

    private bool canvasSetUp(out Transform panelTransform)
    {
        panelTransform = PopUpCanvas.transform.Find("PopUpPanel");
        if (panelTransform == null)
        {
            return true;
        }
        PopUpPanel = panelTransform.gameObject;
        return false;
    }


    public void ShowPopup(string message, float duration = 2f)
    {
        // Displays a popup message
        if (PopUpPanel == null || PopUpText == null) return;

        PopUpText.text = message;
        PopUpPanel.SetActive(true);

        // Restart hide coroutine
        StopAllCoroutines();
        StartCoroutine(HideAfter(duration));
    }

    private IEnumerator HideAfter(float wait)
    {
       // Hides the popup after the delay.
        yield return new WaitForSeconds(wait);
        PopUpPanel.SetActive(false);
    }
}
