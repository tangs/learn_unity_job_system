using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Step2
{
    [BurstCompile]
    public struct FindNearestJob : IJobFor
    {
        [ReadOnly]
        public NativeArray<float3> SeekersPos;
        public NativeArray<float3> DestPos;
        [ReadOnly]
        public NativeArray<float3> TargetsPos;
            
        public void Execute(int index)
        {
            var pos = SeekersPos[index];
            var minSqrMagnitude = float.MaxValue;
            float3 destPos = Vector3.positiveInfinity;
            foreach (var targetPos in TargetsPos)
            {
                var sqrMagnitude = math.distancesq(pos, targetPos);
                if (!(sqrMagnitude < minSqrMagnitude)) continue;
                
                minSqrMagnitude = sqrMagnitude;
                destPos = targetPos;
            }

            DestPos[index] = destPos;
        }
    }
}