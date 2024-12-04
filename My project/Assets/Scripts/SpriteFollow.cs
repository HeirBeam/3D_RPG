using UnityEngine;

public class SpriteFollow : MonoBehaviour
{
    private void Update() {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }    
}
