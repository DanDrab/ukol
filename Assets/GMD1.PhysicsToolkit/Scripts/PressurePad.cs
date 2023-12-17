using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    public string[] acceptedTags;
    public int minActivationCount = 1;
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    private HashSet<string> acceptedTagsHashSet = new HashSet<string>();
    private HashSet<int> collidersInTrigger = new HashSet<int>();
    private int lastCollidersInTriggerCount = 0;
    
    private void Awake()
    {
        foreach (var acceptedTag in acceptedTags)
        {
            acceptedTagsHashSet.Add(acceptedTag);
        }
    }

    private void FixedUpdate()
    {
        int currentCollidersInTriggerCount = collidersInTrigger.Count;
        if (lastCollidersInTriggerCount != currentCollidersInTriggerCount)
        {
            if (collidersInTrigger.Count >= minActivationCount)
            {
                onActivate?.Invoke();
                Debug.Log("onActivate");
            }
            else
            {
                onDeactivate?.Invoke();
                Debug.Log("onDeactivate");
            }

            lastCollidersInTriggerCount = currentCollidersInTriggerCount;
        }
        
        collidersInTrigger.Clear();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(!CompareTag(other.tag))
            return;
        
        collidersInTrigger.Add(other.gameObject.GetInstanceID());
    }
    
    
    bool CompareTag(string tag)
    {
        if(acceptedTagsHashSet.Count > 0 && 
           !acceptedTagsHashSet.Contains(tag))
            return false;
        
        return true;
    }
}
