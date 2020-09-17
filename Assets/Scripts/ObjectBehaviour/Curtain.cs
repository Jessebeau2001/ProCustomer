using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Curtain : MonoBehaviour
{
    public bool isClosed;
    [SerializeField] string text;
    [SerializeField] Animator anim;
    [SerializeField] GameObject textMesh;
    void Start()
    {
        anim = GetComponent<Animator>();
        textMesh = transform.Find("TextMesh").gameObject;
        UpdateString();
    }

    void Update()
    {
        DisableText();
    }

    private void UpdateString() {
        if (anim.GetBool("IsClosed"))
            textMesh.GetComponent<TextMeshPro>().text = "Press 'F' to open";
        else
            textMesh.GetComponent<TextMeshPro>().text = "Press 'F' to close";
    }
    public void SwapState() {
        isClosed = !isClosed;
        UpdateState();
        UpdateString();
    }

    private void UpdateState() {
        anim.SetBool("IsClosed", isClosed);
    }

    public void EnableText() {
        textMesh.SetActive(true);
    }

    public void DisableText() {
        textMesh.SetActive(false);
    }

}
