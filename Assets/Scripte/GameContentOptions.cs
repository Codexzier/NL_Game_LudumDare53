using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContentOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnExitGame()
    {
        SceneManager.LoadScene("Scenes/MenuScene");
    }
}
