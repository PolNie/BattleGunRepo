using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform cameraTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraTarget.position;
        transform.rotation = cameraTarget.rotation;
    }
}
