using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    public bool closed;
    [SerializeField] Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     SwapState();
        // }
    }

    public void SwapState() {
        anim.SetBool("IsClosed", !anim.GetBool("IsClosed"));
    }
}
