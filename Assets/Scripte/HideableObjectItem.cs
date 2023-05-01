using UnityEngine;
using UnityEngine.UI;

public abstract class HideableObjectItem : MonoBehaviour
{
    private SpriteRenderer _renderer;
    protected Image Icon;
    private void Start()
    {
        this._renderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.Icon = this.gameObject.GetComponent<Image>();
        
        this.StartExtended();
    }

    protected virtual void StartExtended()
    {
    }
    
    public void Show()
    {
        if(this._renderer != null) this._renderer.enabled = true;
        if (this.Icon != null) this.Icon.enabled = true;
        
        Debug.Log("Show Object");
    }

    public void Hide()
    {
        if(this._renderer != null) this._renderer.enabled = false;
        if (this.Icon != null) this.Icon.enabled = false;
        
        Debug.Log("Hide Object");
    }
}