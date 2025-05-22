using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        GameController.Instance.SetGameState(GameState.Overworld);
        GameController.Instance.SetBiomeState(BiomeState.Forest);
        SceneManager.LoadScene("Overworld");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
