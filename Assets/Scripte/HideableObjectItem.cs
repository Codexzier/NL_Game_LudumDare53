using UnityEngine;

public abstract class HideableObjectItem : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private void Awake()
    {
        this._renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void Show()
    {
        this._renderer.enabled = true;
    }

    public void Hide()
    {
        this._renderer.enabled = false;
    }
}