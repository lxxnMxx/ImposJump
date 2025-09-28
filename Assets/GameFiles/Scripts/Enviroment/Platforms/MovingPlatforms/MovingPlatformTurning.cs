using System;
using System.Collections;
using UnityEngine;

[Tooltip("For turning platforms")]
public class MovingPlatformTurning : MovingPlatformBase
{
    protected override IEnumerator Move()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, points[I].position) <= 0.02f)
            {
                I += 1;
                if (I == points.Length) I = 0;
                
                // turn the platform to the current point
                Direction = transform.position - points[I].position;
                Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(Angle, Vector3.up);
            }
            
            try
            {
                if (transform.GetChild(0)) 
                    transform.GetChild(0).transform.rotation = Quaternion.EulerAngles(0,0,0);
            }
            catch
            {
                print("No Child found");
            }
            
            transform.position = Vector3.MoveTowards(transform.position, points[I].position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
