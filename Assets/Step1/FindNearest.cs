using UnityEngine;

namespace Step1
{
    public class FindNearest : MonoBehaviour
    {
        public void Update()
        {
            var pos = transform.localPosition;
            var minSqrMagnitude = float.MaxValue;
            Transform destTargetTransform = null;
            foreach (var targetTransform in Spawner.TargetsTransforms)
            {
                var sqrMagnitude = (pos - targetTransform.localPosition).sqrMagnitude;
                if (!(sqrMagnitude < minSqrMagnitude)) continue;
                
                minSqrMagnitude = sqrMagnitude;
                destTargetTransform = targetTransform.transform;
            }

            if (destTargetTransform is null) return;
            Debug.DrawLine(pos, destTargetTransform.localPosition, Color.red);
        }
    }
}
