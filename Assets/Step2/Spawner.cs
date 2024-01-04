using UnityEngine;
using Random = UnityEngine.Random;

using Seeker = Step1.Seeker;
using Target = Step1.Target;

namespace Step2
{
    public class Spawner : MonoBehaviour
    {
        public static Transform[] SeekersTransforms;
        public static Transform[] TargetsTransforms;

        public GameObject seekerPrefab;
        public GameObject targetPrefab;
        
        public int numSeekers;
        public int numTargets;
        public Vector2 bounds;

        public void Start()
        {
            Random.InitState(12345);
        
            SeekersTransforms = new Transform[numSeekers];
            for (var i = 0; i < numSeekers; ++i)
            {
                var go = Instantiate(seekerPrefab);
                var seeker = go.GetComponent<Seeker>();
                seeker.direction = Random.insideUnitCircle;
                go.transform.localPosition = new Vector3(
                    Random.Range(0, bounds.x), 0, Random.Range(0, bounds.y));
                SeekersTransforms[i] = go.transform;
            }
            
            TargetsTransforms = new Transform[numTargets];
            for (var i = 0; i < numTargets; ++i)
            {
                var go = Instantiate(targetPrefab);
                var target = go.GetComponent<Target>();
                target.direction = Random.insideUnitCircle;
                go.transform.localPosition = new Vector3(
                    Random.Range(0, bounds.x), 0, Random.Range(0, bounds.y));
                TargetsTransforms[i] = go.transform;
            }
        
        }
    }
}
