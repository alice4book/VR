using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectBonfire : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, int> _listForComplition;

    [SerializeField]
    private Dictionary<string, int> _listOfCurrentObj;

    [SerializeField]
    [Tooltip("Tag name of object")]
    private List<string> _listOfObj;

    [SerializeField]
    [Tooltip("How many objects needs to be in bonfire")]
    private List<int> _listOfValues;

    [SerializeField]
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        if (_listOfObj.Count != _listOfValues.Count)
        {
            Debug.LogError("The number of objects and values must be the same.");
            return;
        }

        for (int i = 0; i < _listOfObj.Count; i++)
        {
            _listForComplition[_listOfObj[i]] = _listOfValues[i];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_listOfCurrentObj.ContainsKey(other.gameObject.tag))
        {
            _listOfCurrentObj[other.gameObject.tag]++;
        }
        else
        {
            _listOfCurrentObj[other.gameObject.tag] = 1;
        }
        if (CheckCompletion())
        {
            _particleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_listOfCurrentObj.ContainsKey(other.gameObject.tag))
        {
            _listOfCurrentObj[other.gameObject.tag]--;
        }
    }

    private bool CheckCompletion()
    {
        foreach (var kvp in _listForComplition)
        {
            if (!_listOfCurrentObj.TryGetValue(kvp.Key, out int currentValue))
            {
                // Key not found in _listOfCurrentObj
                Debug.Log("Key not found: " + kvp.Key);
                return false;
            }

            if (currentValue < kvp.Value)
            {
                // Value in _listOfCurrentObj is less than in _listForComplition
                Debug.Log("Value too low for key: " + kvp.Key + ". Required: " + kvp.Value + ", Found: " + currentValue);
                return false;
            }
        }

        // All keys found with equal or higher values
        return true;
    }
}
