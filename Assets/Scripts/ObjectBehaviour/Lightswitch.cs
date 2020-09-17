using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public bool IsOn;
    public Light lightSource;
    [SerializeField] Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        UpdateState();
    }

    public void SwapState() {
        IsOn = !IsOn;
        UpdateState();
    }

    public void UpdateState() {
        lightSource.enabled = IsOn;
        anim.SetBool("IsOn", IsOn);
    }
}
