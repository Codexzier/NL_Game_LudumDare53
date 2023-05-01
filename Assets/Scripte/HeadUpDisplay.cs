using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadUpDisplay : MonoBehaviour
{
    public TextMeshProUGUI pointStatus;
    public TextMeshProUGUI notInTime;
    
    void Start()
    {
        this.pointStatus.text = "Points: 0";
        this.notInTime.text = "Not in time: 0";
    }

    public void OnExitGame()
    {
        SceneManager.LoadScene("Scenes/MenuScene");
    }

    public void SetScore(int score, int notInTime)
    {
        this.pointStatus.text = $"Points: {score}";
        this.notInTime.text = $"Not in time: {notInTime}";
    }
}
