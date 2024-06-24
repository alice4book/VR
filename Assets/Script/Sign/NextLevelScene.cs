using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScene : MonoBehaviour
{
    [SerializeField] private Burnable _burnable;
    [SerializeField] private string _name;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _deleyTime = 2.0f;

    public void SetName(string name)
    {
        _name = name;
        if(_textMesh)
            _textMesh.text = name;
    }

    private void Start()
    {
        _burnable.OnStartBurning += ChanegeLevelAfterTime;
    }

    private void ChanegeLevelAfterTime()
    {
        Invoke("ChangeLevel", _deleyTime);
    }

    private void ChangeLevel()
    {
        if (!string.IsNullOrEmpty(_name))
        {
            SceneManager.LoadScene(_name);
        }
        else
        {
            Debug.LogWarning("Scene name is null or empty.");
        }
    }
}
