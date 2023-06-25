using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public float rotateSpeed;
    public Transform focus;
    private Vector2 _orbitAngle = new Vector2(45f, 0);

    // Lock and unlock cursors in game.
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // The movement of the camera around the player to move in any direction the player is facing.
    private bool ManualRotation()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        float deadzone = 0.001f;
        if (input.x < -deadzone || input.x > deadzone || input.y < -deadzone || input.y > deadzone)
        {
            _orbitAngle += input * rotateSpeed * Time.unscaledTime;
            return true;
        }

        return false;
    }

    private void LateUpdate()
    {
        Quaternion lookRotation = transform.rotation;

        if (ManualRotation())
        {
            lookRotation = Quaternion.Euler(_orbitAngle);
        }

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focus.position - lookDirection * 5f;

        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

}
