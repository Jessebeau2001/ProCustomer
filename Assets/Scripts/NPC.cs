using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Camera cam;
    public Transform textPrefab;
    public bool EnablePopup;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                agent.SetDestination(hit.point);
                if (EnablePopup)
                    TextPopup("Testlalalalablublubruhriebruhma");
            }
        }
    }

    private void TextPopup(string text) {
        var NewText = Instantiate(textPrefab, transform.position, Quaternion.identity);
        NewText.SetParent(transform);
        NewText.Translate(new Vector3(0, 1, 0));
    }

    public void SetDest(Vector3 dest) {
        agent.SetDestination(dest);
    }
}
