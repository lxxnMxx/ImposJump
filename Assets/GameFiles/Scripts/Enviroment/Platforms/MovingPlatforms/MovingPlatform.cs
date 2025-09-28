using System;
using System.Collections;
using UnityEngine;

[Tooltip("For normal platforms")]
public class MovingPlatform : MovingPlatformBase
{
    protected override IEnumerator Move()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, points[I].position) <= 0.02f)
            {
                I += 1;
                if (I == points.Length) I = 0;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, points[I].position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
