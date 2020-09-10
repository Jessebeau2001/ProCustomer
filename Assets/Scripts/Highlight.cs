using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Shader shader;
    public Shader standard;
    Material mat;

    public float interactionDistance = 300f;
    [SerializeField] private bool glowing;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);

        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance);

        glowing = false;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "PickUp")
            {
                glowing = true;
                mat = hit.collider.GetComponent<Renderer>().material;
            }
        }

        if (mat != null)
        {
            if (glowing)
            {
                mat.shader = shader;
            }
            else
            {
                mat.shader = standard;
                glowing = false;
            }
        }
    }
}
