using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPlace : MonoBehaviour
{
    public enum Result
    {
        Grab,
        Fail,
        Release
    }

    [SerializeField] bool grabbed;
    [SerializeField] string tagGrabbable = "Grabbable";
    [SerializeField] float raycastDistance = 1f;
    [SerializeField] GameObject grabAnchor;

    private GameObject grabTarget;

    public Result TryGrabOrRelease()
    {
        RaycastHit hit;

        if (grabbed)
        {
            grabbed = false;
            Release();
            return Result.Release;
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {
                if (hit.collider.gameObject.CompareTag(tagGrabbable))
                {
                    grabbed = true;
                    Grab(hit.collider.gameObject);
                    return Result.Grab;
                }
            }

            return Result.Fail;
        }
    }

    private void Grab(GameObject target)
    {
        grabTarget = target;
        target.transform.SetParent(grabAnchor.transform);
        target.transform.SetPositionAndRotation(grabAnchor.transform.position, grabAnchor.transform.rotation);
        if (target.TryGetComponent(out Rigidbody body))
        {
            body.detectCollisions = false;
            body.useGravity = false;
        }
    }
    private void Release()
    {
        grabTarget.transform.SetParent(null);
        if (grabTarget.TryGetComponent(out Rigidbody body))
        {
            body.detectCollisions = true;
            body.useGravity = true;
        }
        grabTarget = null;
    }
}
