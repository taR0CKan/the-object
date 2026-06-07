using UnityEngine;

public class TrayController : MonoBehaviour
{
    [SerializeField]
    public ItemPlaceholder placeholder;

    public ItemScriptable CurrentItem => placeholder.CurrentItemData;
}
