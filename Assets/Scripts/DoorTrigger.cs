using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    private bool _endEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        VideoManager.m3DonePlaying += endEnabled;
    }
    private void OnDestroy()
    {
        VideoManager.m3DonePlaying -= endEnabled;
    }

    private void endEnabled()
    {
        _endEnabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _endEnabled)
        {
            Debug.Log("End scene");
            SceneManager.LoadScene(2);//end scene
        }
    }
}
