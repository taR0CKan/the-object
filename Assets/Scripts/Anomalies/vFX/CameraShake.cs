using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 startLocalPosition;

    private void Awake()
    {
        startLocalPosition = cameraTransform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition =
                startLocalPosition + new Vector3(x, y, 0f);

            yield return null;
        }

        cameraTransform.localPosition = startLocalPosition;
    }
}
