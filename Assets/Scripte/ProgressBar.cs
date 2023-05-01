using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject statusBar;
    
    public void SetValue(float status)
    {
        if(status > 1f) return;
        
        var pos = new Vector3(-0.46f * (1f - status), 0, 0);
        var scale = new Vector3(status, 0.94f, 0.75f);
        
        this.statusBar.transform.localScale = scale;
        this.statusBar.transform.localPosition = pos;
    }
}
