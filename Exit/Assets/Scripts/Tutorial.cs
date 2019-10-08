using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 1.0f)
        {
            count = count + 0.1f;
        }
        else if(count > 1.0f)
        {
            Destroy(this);
        }
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Title");
    }
}
