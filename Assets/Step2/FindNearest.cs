using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Step2
{
    public class FindNearest : MonoBehaviour
    {   
        private NativeArray<float3> _seekersPos;
        private NativeArray<float3> _destPos;
        private NativeArray<float3> _targetsPos;
        
        public void Start()
        {
            var spawner = FindObjectOfType<Spawner>();
            _seekersPos = new NativeArray<float3>(spawner.numSeekers, Allocator.Persistent);
            _destPos = new NativeArray<float3>(spawner.numSeekers, Allocator.Persistent);
            _targetsPos = new NativeArray<float3>(spawner.numTargets, Allocator.Persistent);
        }

        public void OnDestroy()
        {
            _seekersPos.Dispose();
            _destPos.Dispose();
            _targetsPos.Dispose();
        }

        public void Update()
        {
            var seekersTransforms = Spawner.SeekersTransforms;
            for (var i = 0; i < seekersTransforms.Length; ++i)
            {
                _seekersPos[i] = seekersTransforms[i].localPosition;
            }

            var targetsTransforms = Spawner.TargetsTransforms;
            for (var i = 0; i < targetsTransforms.Length; ++i)
            {
                _targetsPos[i] = targetsTransforms[i].localPosition;
            }
            
            var scheduleJobDependency = new JobHandle();
            var myJob = new FindNearestJob()
            {
                SeekersPos = _seekersPos,
                DestPos = _destPos,
                TargetsPos = _targetsPos
            };
            var handle = myJob.Schedule(seekersTransforms.Length, scheduleJobDependency);
            // var handle = myJob.ScheduleParallel(seekersTransforms.Length,
            //     128, scheduleJobDependency);
            handle.Complete();
            
            for (var i = 0; i < seekersTransforms.Length; ++i)
            {
                Debug.DrawLine(_seekersPos[i], _destPos[i]);
            }
            
        }
    }
}
