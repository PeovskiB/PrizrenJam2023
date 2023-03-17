using System.Collections;
using UnityEngine;

[System.Serializable]
public struct ShakeData
{
    public float rotationShake;
    public float fovShake;
    public float shakeTime;
}

public class CameraController : MonoBehaviour
{

    private static CameraController instance;

    public Transform target;
    public Vector2 offset;
    public float smoothTime;

    private Vector3 vel;

    private ShakeData data;
    private float baseSize;

    private Camera cam;

    void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();
        baseSize = cam.orthographicSize;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 goal = (Vector2)target.position + offset;

        goal.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, goal, ref vel, smoothTime * Time.smoothDeltaTime);
    }

    public static void Shake(ShakeData shake, float multiplier = 1f)
    {
        instance.StopAllCoroutines();

        instance.Reset();

        instance.data.rotationShake = shake.rotationShake * multiplier;
        instance.data.fovShake = shake.fovShake * multiplier;
        instance.data.shakeTime = shake.shakeTime;// * multiplier;
        instance.baseSize = instance.cam.orthographicSize;

        instance.StartCoroutine(instance.DoShake());
    }

    IEnumerator DoShake()
    {
        while (data.shakeTime > 0)
        {
            while (Time.timeScale == 0f)
            {
                Reset();
                yield return null;
            }

            data.shakeTime -= Time.unscaledDeltaTime;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z = Random.Range(-data.rotationShake, data.rotationShake);

            transform.rotation = Quaternion.Euler(rotation);

            cam.orthographicSize = baseSize + Random.Range(-data.fovShake, data.fovShake);

            yield return null;
        }

        Reset();

        yield break;
    }

    private void Reset()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = 0;

        transform.eulerAngles = rot;

        cam.orthographicSize = baseSize;
    }
}
