using TMPro;
using UnityEngine.SceneManagement;

public class GameOverBoard : HideableObjectItem
{
    public TextMeshProUGUI TextScore;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        this.TextScore.text = $"Your Score: {score:0000}";
        this.gameObject.SetActive(true);
    }
    
    public void OnExitGame()
    {
        SceneManager.LoadScene("Scenes/MenuScene");
    }

    public void NewGame()
    {
        this.gameObject.SetActive(false);
    }
}