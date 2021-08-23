using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/22/21
 * Library for physics-related functions.
 */

public static class PhysLib
{
   //GetAcceleration calculates the acceleration from  a start velocity and an end velocity.
   public static float GetAcceleration(float start, float startDur, float end, float endDur)
    {
        float accel = (end - start) / (endDur - startDur);
        return accel;
    }

    //GetVelocity returns the velocity from a start pos and end pos.
    public static float GetVelocity(Vector3 start, Vector3 end, float timeDur)
    {
        float vel = Vector3.Distance(start, end)/timeDur;
        return vel;
    }

    //GetForce calculates a force given a mass and acceleration.
    public static float GetForce(float mass, float accel)
    {
        return mass * accel;
    }

    public enum TimeType
    {
        Normal,
        Smooth,
        Fixed
    }
}

