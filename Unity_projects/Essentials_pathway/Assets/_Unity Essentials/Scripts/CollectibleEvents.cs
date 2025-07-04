using System;
using UnityEngine;

public static class CollectibleEvents
{
    public static Action OnStarCollected;
    public static Action<Transform> OnStarRespawned;   // passes star's Transform
}

