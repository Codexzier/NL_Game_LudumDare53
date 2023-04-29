using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContentOptions : MonoBehaviour
{
    public void OnExitGame()
    {
        SceneManager.LoadScene("Scenes/MenuScene");
    }
}
