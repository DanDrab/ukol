using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportField : MonoBehaviour
{
    public Transform target;
    public bool resetVelocity;

    public void Teleport(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(rb == null)
            return;

        Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
        Matrix4x4 localToWorldMatrix = target.localToWorldMatrix;
        
        Vector3 localPosition = worldToLocalMatrix.MultiplyPoint(rb.position);
        Vector3 localVelocity = worldToLocalMatrix.MultiplyVector(rb.velocity);
        Vector3 localAngularVelocity = localToWorldMatrix.MultiplyVector(rb.angularVelocity);
        
        Quaternion localRotation = RotateQuaternionByMatrix(rb.rotation, localToWorldMatrix);

        rb.position = localToWorldMatrix.MultiplyPoint(localPosition);
        rb.velocity = resetVelocity ? Vector3.zero : localToWorldMatrix.MultiplyVector(localVelocity);
        rb.angularVelocity = resetVelocity ? Vector3.zero : localToWorldMatrix.MultiplyVector(localAngularVelocity);
        rb.rotation = RotateQuaternionByMatrix(localRotation, worldToLocalMatrix);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(transform.position, target.position);
        UnityEditor.Handles.ArrowHandleCap(0, target.position, target.rotation, 1f, EventType.Repaint);
        UnityEditor.Handles.color = Color.white;
    }
    #endif

    static Quaternion RotateQuaternionByMatrix(Quaternion q, Matrix4x4 m)
    {
        var forward = m.MultiplyVector(q * Vector3.forward);
        var upwards = m.MultiplyVector(q * Vector3.up);
        return Quaternion.LookRotation(forward, upwards);
    }

    
}
