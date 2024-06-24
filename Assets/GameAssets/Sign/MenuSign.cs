using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSign : MonoBehaviour
{
    //[SerializeField] TMP_Text signText;

    [SerializeField] Burnable bonfire;

    public bool level1;
    public bool level2;
    public bool quit;

    private bool _chosen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bonfire._isBurning && !_chosen)
        {
            _chosen = true;
            if(level1)
            {
                Invoke("Level1", 3.0f);
            }
            else if(level2)
            {
                Invoke("Level2", 3.0f);
            }
            else
            {
                Invoke("Quit", 3.0f);
            }
        }
    }

    void Level1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    void Quit()
    {
        Application.Quit();
    }
}
