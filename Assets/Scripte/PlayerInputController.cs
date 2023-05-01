using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerInputController : MonoBehaviour
{
    public ShipSupplier ShipSupplier;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 1f)
        {
            return;
        }
        
        if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))this.ShipSupplier.change.x = 1;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )   this.ShipSupplier.change.x = -1;
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) this.ShipSupplier.change.y = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) this.ShipSupplier.change.y = -1;
        else if (Input.GetKeyUp(KeyCode.E)) 
        {
            // Interact
        }
        else if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        }
    }
}
