using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [System.Flags] private enum Axis
    {
         X = 0b10,
         Y = 0b01,
        XY = 0b11
    }

    [SerializeField] Axis axis;
    [SerializeField] float mouseSens;
    [SerializeField] float vertMin;
    [SerializeField] float vertMax;

    [HideInInspector] public Vector2 mouseInput;

    private float angleX;
    private float angleY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (axis.HasFlag(Axis.Y))
        {
            angleY -= mouseInput.y * mouseSens;
            angleY = Mathf.Clamp(angleY, vertMin, vertMax);
        }
        else angleY = transform.eulerAngles.x;

        if (axis.HasFlag(Axis.X)) angleX = transform.eulerAngles.y + (mouseInput.x * mouseSens);
        else angleX = transform.eulerAngles.y;

        transform.eulerAngles = new Vector3(angleY, angleX, transform.eulerAngles.z);
    }
}
