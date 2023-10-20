using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    private MeshCollider meshCollider;
    private bool quake;
    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        StartCoroutine(EarthQuakeCoroutine(15, .125f));
    }
    IEnumerator EarthQuakeCoroutine(int durationInSecs, float quakeDelay)
    {
        yield return new WaitForSeconds(5);
        float timePassed = 0;
        while (timePassed < durationInSecs)
        {
            timePassed += quakeDelay;
            yield return new WaitForSeconds(quakeDelay);
            meshCollider.convex = quake;
            quake = !quake;
        }
        meshCollider.convex = false;
    }
}
