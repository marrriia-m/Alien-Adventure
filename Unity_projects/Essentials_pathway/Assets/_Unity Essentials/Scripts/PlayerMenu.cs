using System.Collections;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public Transform idlePoint;            // where the alien chills
    public Transform circleCenter;         // usually the title text pivot
    public float circleRadius = 2.5f;

    public float flySpeed     = 3f;
    public float circleTime   = 4f;        // seconds for one full loop

    void OnEnable()
    {
        CollectibleEvents.OnStarRespawned += HandleStarAppeared;
        CollectibleEvents.OnStarCollected += HandleStarCollected;

        // start parked at idle
        transform.position = idlePoint.position;
        transform.rotation = Quaternion.identity;
    }

    void OnDisable()
    {
        CollectibleEvents.OnStarRespawned -= HandleStarAppeared;
        CollectibleEvents.OnStarCollected -= HandleStarCollected;
    }

    void HandleStarAppeared(Transform star)
    {
        StopAllCoroutines();
        StartCoroutine(FlyTo(star.position));
    }

    void HandleStarCollected()
    {
        StopAllCoroutines();
        StartCoroutine(FlyTo(idlePoint.position));
    }

    IEnumerator FlyTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, flySpeed * Time.deltaTime);
            transform.LookAt(target);
            yield return null;
        }
    }

    /* IEnumerator FlyOrbitThenReturn()
    {
        //Fly to the start-point of the circle (angle = 0)
        Vector3 startPoint = circleCenter.position + circleCenter.right * circleRadius;
        yield return StartCoroutine(FlyTo(startPoint));

        //Circle once around the centre
        float t = 0f;
        while (t < circleTime)
        {
            t += Time.deltaTime;
            float theta = (t / circleTime) * Mathf.PI * 2f;      // 0 → 2π
            Vector3 offset = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) * circleRadius;
            transform.position = circleCenter.position + offset;
            transform.LookAt(circleCenter);
            yield return null;
        }

        //Return to idle
        yield return StartCoroutine(FlyTo(idlePoint.position));
    } */
}

