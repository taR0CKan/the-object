using UnityEngine;

[System.Serializable]
 public class MainTaskEndpoint
{
    [SerializeField] private Transform beforeDoorPoint;
    [SerializeField] private Transform behindDoorPoint;
    [SerializeField] private GameObject door;
    public Transform BeforeDoorPoint => beforeDoorPoint;
    public Transform BehindDoorPoint => behindDoorPoint;
    public GameObject Door => door;
}
