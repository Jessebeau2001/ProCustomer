using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lightswitch : MonoBehaviour
{
    public bool IsOn;
    public bool showText;
    public Light lightSource;
    [SerializeField] Animator anim;
    [SerializeField] GameObject TextMesh;

    void Start() {
        anim = GetComponent<Animator>();
        UpdateState();
        TextMesh = transform.Find("TextMesh").gameObject;
    }

    void Update() {
        TextMesh.SetActive(showText);
        showText = false;
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
