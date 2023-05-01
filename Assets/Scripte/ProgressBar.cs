using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject statusBar;
    
    public void SetValue(float status)
    {
        if(status > 1f) return;
        
        //Debug.Log($"Progressbar: {status}");
        var pos = new Vector3(-0.46f * (1f - status), 0, 0);
        var scale = new Vector3(status, 0.94f, 0.75f);
        
        this.statusBar.transform.localScale = scale;
        this.statusBar.transform.localPosition = pos;
    }
    
    private SpriteRenderer _renderer;
    private SpriteRenderer _rendererStatusBar;
    private void Awake()
    {
        this._renderer = this.gameObject.GetComponent<SpriteRenderer>();
        this._rendererStatusBar = this.statusBar.gameObject.GetComponent<SpriteRenderer>();
    }
    
    // public void Show()
    // {
    //     //Debug.Log("replacement item is enable");
    //     
    //     this._renderer.enabled = true;
    //     this._rendererStatusBar.enabled = true;
    // }
    //
    // public void Hide()
    // {
    //     this._renderer.enabled = false;
    //     this._rendererStatusBar.enabled = false;
    // }
}
