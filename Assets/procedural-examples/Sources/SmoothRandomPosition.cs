// This script is placed in public domain. The author takes no responsibility for any possible harm.

using UnityEngine;

public class SmoothRandomPosition : MonoBehaviour
{
    // Moves the object along as far as range units randomly but in a smooth way.
    // This script requires the Noise.cs script.
    float speed = 1.0f;
    Vector3 range = new Vector3 (1.0f, 1.0f, 1.0f);

    private Vector3 position;

    void Start ()
    {
        position = transform.position;
    }

    void Update ()
    {
        transform.position = position + Vector3.Scale (SmoothRandom.GetVector3 (speed), range);
    }
}