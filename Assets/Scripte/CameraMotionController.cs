using UnityEngine;

public class CameraMotionController : MonoBehaviour
{
    public ShipSupplier ShipSupplier;
    public Camera camera;

    private void Update()
    {
        var bySize = this.camera.orthographicSize / 5.0f;
        this.ShipSupplier.speedByScreenSize = this.camera.pixelWidth / this.camera.pixelHeight * bySize;

        
        var playerPosition = this.ShipSupplier.transform.position;
        playerPosition.z = this.transform.position.z;
        this.transform.position = playerPosition;
    }
}
