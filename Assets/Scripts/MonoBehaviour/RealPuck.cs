using System;
using UnityEngine;

public class RealPuck : Puck
{
    public static Action<Checkpoint, RealPuck> CheckpointReached;

    override protected void OnCheckpointHit(Checkpoint checkpoint, Puck puck)
    {
        if (!checkpoint.Reached)
        {
            if (puck is RealPuck realPuck)
            {
                if (realPuck == this)
                {
                    PlaceInMiddleOfCheckpoint(checkpoint);
                    Stop();
                    CheckpointReached?.Invoke(checkpoint, this);
                }
            }
        }
    }

    override protected void OnDestroy()
    {
        CheckpointReached = null;
        base.OnDestroy();
    }

}