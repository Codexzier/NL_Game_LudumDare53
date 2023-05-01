using UnityEngine;

public class AutoHideRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var r = this.GetComponent<Renderer>();
        if (r != null)
        {
            r.enabled = false;
        }
    }
}
