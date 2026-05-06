using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class VisualEffectsManager : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    private DepthOfField dof;

    private void Awake()
    {
        globalVolume.profile.TryGet(out dof);
        dof.active = false;
    }

    public void EnableInspectBlur(float distance)
    {
        dof.focusDistance.value = distance * 2f;
        dof.focalLength.value = distance*400;
        dof.active = true;
    }

    public void DisableInspectBlur()
    {
        dof.active = false;
    }
}
