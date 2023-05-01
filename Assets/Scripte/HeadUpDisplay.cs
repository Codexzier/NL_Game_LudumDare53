using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadUpDisplay : MonoBehaviour
{
    public TextMeshProUGUI pointStatus;

    void Start()
    {
        this.pointStatus.text = "Points: 0";
    }

    public void OnExitGame()
    {
        SceneManager.LoadScene("Scenes/MenuScene");
    }

    public void SetScore(int score)
    {
        this.pointStatus.text = $"Points: {score}";
    }
}
