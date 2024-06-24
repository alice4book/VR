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
        if (CheckCompletion())
        {
            _particleSystem.Play();
            _fireSize.StartAll();
            _spawnSignToNextLevel.SpawnSignToLevel();
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
