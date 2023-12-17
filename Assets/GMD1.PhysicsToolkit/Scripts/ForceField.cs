using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float force;
    public ForceMode forceMode;
    
    public void Add(Collider other)
    {
        if(other == null)
            return;
        
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(rb == null)
            return;
        
        rb.AddForce(transform.forward * force, forceMode);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.ArrowHandleCap(0, transform.position, transform.rotation, Mathf.Sign(force), EventType.Repaint);
        UnityEditor.Handles.color = Color.white;
    }
#endif
}
