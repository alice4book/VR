using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSignToNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject _sign;
    [SerializeField] private Transform _whereToPut;

    [SerializeField] private string _nextScene;
    public void SpawnSignToLevel()
    {
        if (_sign != null && _whereToPut != null)
        {
            var obj = Instantiate(_sign, _whereToPut.position, Quaternion.identity);
            if(obj != null)
            {
                if(obj.GetComponent<NextLevelScene>() != null)
                    obj.GetComponent<NextLevelScene>().SetName(_nextScene);
            }
            
        }
    }
}
