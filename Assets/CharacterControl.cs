using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [Header("")]
    [SerializeField] string horizontalName = "Horizontal";
    [SerializeField] string verticalName = "Vertical";
    [SerializeField] string mouseXName = "Mouse X";
    [SerializeField] string mouseYName = "Mouse Y";
    [SerializeField] string grabName = "Grab";
    [Header("")]
    [SerializeField] CharacterMove move;
    [SerializeField] GrabPlace grab;
    [SerializeField] MouseLook mouseX;
    [SerializeField] MouseLook mouseY;

    private void Update()
    {
        move.Move(new Vector3(
            Input.GetAxis(horizontalName) * Time.deltaTime * moveSpeed, 
            Physics.gravity.y * Time.deltaTime, 
            Input.GetAxis(verticalName) * Time.deltaTime * moveSpeed));

        mouseX.mouseInput.x = Input.GetAxis(mouseXName);
        mouseY.mouseInput.y = Input.GetAxis(mouseYName);

        if (Input.GetButtonDown(grabName))
        {
            grab.TryGrabOrRelease();
        }
    }
}
