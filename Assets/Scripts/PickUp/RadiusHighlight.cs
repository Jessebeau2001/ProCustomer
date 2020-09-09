using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusHighlight : MonoBehaviour
{
    public Shader shader;
    public Shader standard;
    Material mat;

    //radius of this object
    private float interactionDistance;
    [SerializeField] private bool glowing;

    // Start is called before the first frame update
    void Start()
    {
        interactionDistance = this.GetComponent<SphereCollider>().radius;//the radius of the sphere collider
        glowing = false;//should not glow at first
    }

    // Update is called once per frame
    void Update()
    {
        glowing = false;//should not glow at first
        if (glowing == true && mat != null)//if glowing and has material
        {
            mat.shader = shader;//special shader?
        }
        else
        {
            mat.shader = standard;//normal material?
            glowing = false;//should not glow at first
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//if player is colliding with the radius
        {
            Debug.Log("Player in radius. Enter");
            //change the material to make the object glow
            glowing = true;
            mat = this.GetComponentInParent<Renderer>().material;//change the parent render material
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")//if player is not colliding with the radius anymore
        {
            Debug.Log("Player out of radius. Exit");

            //change the material to make the object not glow anymore
            glowing = false;
            mat = this.GetComponentInParent<Renderer>().material;//change the parent render material
        }
    }
}
