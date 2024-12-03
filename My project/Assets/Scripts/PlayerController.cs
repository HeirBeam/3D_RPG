using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 2f;

    private CharacterController controller;
    private Transform cameraTransform;
    private float pitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * speed * Time.deltaTime);

        // Looking
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        transform.Rotate(Vector3.up * mouseX);
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
