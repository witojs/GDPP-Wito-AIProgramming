using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ScoreManager _scoreManager;
    
    private List<Pickable> _pickableList = new List<Pickable>();
 
    private void Start()
    {
        InitPickableList();
    }
 
    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsByType<Pickable>(FindObjectsSortMode.None);
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }
        _scoreManager.SetMaxScore(_pickableList.Count);
        Debug.Log("Pickable List: "+_pickableList.Count);
    }
 
    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        Destroy(pickable.gameObject);
        Debug.Log("Pickable List: " + _pickableList.Count);
        if (pickable.PickableType == PickableType.PowerUp)
        {
            Debug.Log("Abrakadabra");
            _player?.PickPowerUp();
        }
        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }

        if (_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }
    }
}
