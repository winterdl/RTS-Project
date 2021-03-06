using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public class UnitSelectedRenderer : ComponentSystem
{

    protected override void OnUpdate()
    {

        Entities.WithAll<EntitySelectedComponent>().ForEach((ref Translation translation) =>
        {
            float3 position = translation.Value + new float3(0, -.3f, -1f);
            Graphics.DrawMesh(
                GameHandler.instance.unitSelectedCircleMesh,
                position, Quaternion.identity,
                GameHandler.instance.unitSelectedCircleMaterial,
                0
                );
        });
    }

}