using System.Runtime.CompilerServices;
using UnityEngine;

public class P_Interaction : MonoBehaviour
{
    [SerializeField] ActionMapManager actionMapManager;
    [SerializeField] P_Inventory inventory;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    private const float playerReach = 3f;
    //private bool isAtScreen = false;



    //public void Interact() //Старое
    //{
    //    Ray interactionRay = new Ray(cam.transform.position, cam.transform.forward); //Если нужно пояснение к предмету - кинуть все в апдейт
    //    if (Physics.Raycast(interactionRay, out RaycastHit hit, playerReach, layerMask))
    //    {
    //        if (hit.collider.CompareTag("Interactive"))
    //        {
    //            hit.collider.GetComponent<Interactive>().InteractItem();
    //        }
    //        else if (hit.collider.CompareTag("Item"))
    //        {
    //            inventory.PickItem(hit.collider.gameObject.GetComponent<InteractiveItem>());
    //        }
    //        else if (hit.collider.CompareTag("Screen"))
    //        {
    //            isAtScreen = actionMapManager.SwitchToInteraction(isAtScreen);
    //            hit.collider.GetComponent<Interactive>().InteractItem();
    //        }
    //        else if (hit.collider.CompareTag("ScreenButton"))
    //        {
    //            //isAtScreen = actionMapManager.SwitchToInteraction(isAtScreen);
    //            hit.collider.GetComponent<Interactive>().InteractItem();
    //        }
    //        else if (hit.collider.CompareTag("InspectableItem"))
    //        {
    //            //isAtScreen = actionMapManager.SwitchToInteraction(isAtScreen);
    //            hit.collider.GetComponent<Interactive>().InteractItem();
    //        }
    //        else if (hit.collider.CompareTag("Button"))
    //        {
    //            if (hit.collider.GetComponent<InteractiveButton>().isCorrectItem(inventory.GetActiveItem()))
    //            {
    //                hit.collider.GetComponent<InteractiveButton>().InteractItem();
    //                inventory.DestroyCurrentItem();
    //            }
    //        }
    //    }
    //}

    public void Interact()
    {
        Ray interactionRay = new Ray(cam.transform.position, cam.transform.forward); //Если нужно пояснение к предмету - кинуть все в апдейт
        if (Physics.Raycast(interactionRay, out RaycastHit hit, playerReach, layerMask))
        {
            if (hit.collider.TryGetComponent<Interactive>(out Interactive interactive))
            {
                interactive.InteractItem();
                return;
            }
            //else if (hit.collider.CompareTag("Item"))
            //{
            //    inventory.PickItem(hit.collider.gameObject.GetComponent<InteractiveItem>());
            //}
            //else if (hit.collider.CompareTag("Screen"))
            //{
            //    isAtScreen = actionMapManager.SwitchToInteraction(isAtScreen);
            //    hit.collider.GetComponent<Interactive>().InteractItem();
            //}
            //else if (hit.collider.CompareTag("ScreenButton"))
            //{
            //    //isAtScreen = actionMapManager.SwitchToInteraction(isAtScreen);
            //    hit.collider.GetComponent<Interactive>().InteractItem();
            //}
            //else if (hit.collider.CompareTag("InspectableItem"))
            //{
            //    //isAtScreen = actionMapManager.SwitchToInteraction(isAtScreen);
            //    hit.collider.GetComponent<Interactive>().InteractItem();
            //}

            //else if (hit.collider.CompareTag("Button"))
            //{
            //    if (hit.collider.GetComponent<InteractiveButton>().isCorrectItem(inventory.GetActiveItem()))
            //    {
            //        hit.collider.GetComponent<InteractiveButton>().InteractItem();
            //        inventory.DestroyCurrentItem();
            //    }
            //}
        }
    }

}
