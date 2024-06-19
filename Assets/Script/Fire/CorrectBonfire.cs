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
    private List<GameObject> _objests;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private FireSize _fireSize;

    private void Awake()
    {
        if (_listOfObj.Count != _listOfValues.Count)
        {
            Debug.LogError("The number of objects and values must be the same.");
            return;
        }

        _listForComplition = new Dictionary<string, int>();
        _listOfCurrentObj = new Dictionary<string, int>();

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
        _objests.Add(other.gameObject);
        if (CheckCompletion())
        {
            _particleSystem.Play();
            _fireSize.StartAll();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_listOfCurrentObj.ContainsKey(other.gameObject.tag))
        {
            _listOfCurrentObj[other.gameObject.tag]--;
            _objests.Remove(other.gameObject);
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

        foreach(var obj in _objests)
        {
            Burnable burnable = obj.GetComponent<Burnable>();
            if (burnable != null)
            {
                if (!burnable._isBurning)
                {
                    Debug.Log("!burnable._isBurning");
                    return false;
                }
            }
        }

        // All keys found with equal or higher values
        return true;
    }
}
