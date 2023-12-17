using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public string[] acceptedTags;
    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerStay;
    public UnityEvent<Collider> onTriggerExit;
    private HashSet<string> acceptedTagsHashSet = new HashSet<string>();
    
    private void Awake()
    {
        foreach (var acceptedTag in acceptedTags)
        {
            acceptedTagsHashSet.Add(acceptedTag);
        }
    }

    private void OnEnable() {}

    public void OnTriggerEnter(Collider other)
    {
        if(!CompareTag(other.tag))
            return;
        onTriggerEnter?.Invoke(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if(!CompareTag(other.tag))
            return;
        onTriggerStay?.Invoke(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if(!CompareTag(other.tag))
            return;
        onTriggerExit?.Invoke(other);
    }

    bool CompareTag(string tag)
    {
        if(acceptedTagsHashSet.Count > 0 && 
           !acceptedTagsHashSet.Contains(tag))
            return false;
        
        return true;
    }
}
