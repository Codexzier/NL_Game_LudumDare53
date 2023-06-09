using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Credits;

    void Start()
    {
        this.Credits.SetActive(false);
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
        Time.timeScale = 1f;
    }

    public void OnExitGame()
    {
        Debug.Log("Beenden");
        Application.Quit();
    }

    public void OnShowCredits()
    {
        this.Credits.SetActive(true);
    }

    public void OnHideCredits()
    {
        this.Credits.SetActive(false);
    }
}