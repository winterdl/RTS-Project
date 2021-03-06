﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class PathFollowSystem : ComponentSystem
{
    protected override void OnUpdate() 
    {
        Entities.ForEach((DynamicBuffer<PathPosition> pathPositionBuffer, ref Translation translation) => {
            if (pathPositionBuffer.Length > 0) {
                int2 nextPosition = pathPositionBuffer[pathPositionBuffer.Length - 1].position;
                float3 targetPosition = new float3(nextPosition.x + 0.5f, nextPosition.y + 0.5f, 0);
                
                // check if we're standing on the target
                if (targetPosition.Equals(translation.Value)) {
                    pathPositionBuffer.RemoveAt(pathPositionBuffer.Length - 1);
                    return;
                }

                float3 moveDir = math.normalize(targetPosition - translation.Value);
                float moveSpeed = 3f;

                translation.Value += moveDir * moveSpeed * Time.deltaTime;

                if (math.distance(translation.Value, targetPosition) < .1f) {
                    //Debug.Log("moving to " + nextPosition);
                    pathPositionBuffer.RemoveAt(pathPositionBuffer.Length - 1);
                }
            }
        });
    }
}