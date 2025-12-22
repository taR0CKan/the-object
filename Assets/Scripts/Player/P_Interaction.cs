using System.Runtime.CompilerServices;
using UnityEngine;

public class P_Interaction : MonoBehaviour
{
    [SerializeField] P_Inventory inventory;
    [SerializeField]
    private Camera cam;
    private const float playerReach = 3f;
    [SerializeField]
    private LayerMask layerMask;
    

   
    public void Interact()
    {
        Ray interactionRay = new Ray(cam.transform.position, cam.transform.forward); //Если нужно пояснение к предмету - кинуть все в апдейт
        if (Physics.Raycast(interactionRay, out RaycastHit hit, playerReach, layerMask))
        {
            if (hit.collider.CompareTag("Interactive"))
            {
                hit.collider.GetComponent<Interactive>().InteractItem();
            }
            else if (hit.collider.CompareTag("Item"))
            {
                inventory.PickItem(hit.collider.gameObject.GetComponent<InteractiveItem>());
                Destroy(hit.collider.gameObject);
            }
        }
    }

    
}
