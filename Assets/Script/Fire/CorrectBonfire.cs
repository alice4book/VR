using System.Collections.Generic;
using UnityEngine;

public class CorrectBonfire : MonoBehaviour
{
    [SerializeField]
    [Tooltip("CorrectBonfirePart scripts that need to be true")]
    private List<CorrectBonfirePart> _listOfScripts;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private FireSize _fireSize;

    [SerializeField]
    private bool _bSign = false;
    private SpawnSignToNextLevel _spawnSignToNextLevel;


    void Start()
    {
        _spawnSignToNextLevel = GetComponent<SpawnSignToNextLevel>();

        foreach (CorrectBonfirePart script in _listOfScripts)
        {
            script.OnChange += IsItReady;
        }
    }

    private void IsItReady()
    {
        if (CheckCompletion() && !_bSign)
        {
            _particleSystem.Play();
            _fireSize.StartAll();
            _spawnSignToNextLevel.SpawnSignToLevel();
            _bSign = true;
        }
    }

    private bool CheckCompletion()
    {
        foreach(CorrectBonfirePart script in _listOfScripts)
        {
            if (!script.CheckCompletion())
            {
                return false;
            }
        }
        // All keys found with equal or higher values
        return true;
    }

}
