using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Action<Pickable> OnPicked;
    
    [SerializeField] public PickableType _pickableType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(OnPicked != null)
            {
                OnPicked(this);
            }
        }
    }
}
