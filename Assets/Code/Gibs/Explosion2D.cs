using UnityEngine;

namespace Assets.Code.Gibs
{
    public class Explosion2D : MonoBehaviour
    {
        [Range(0f, 2f)]
        public float MinPower;

        [Range(0f, 2f)]
        public float MaxPower;
        public float Radius = 1;

        public float MinUp = -0.002f;
        void Start()
        {
            float power = Random.Range(MinPower, MaxPower);
            if (MinPower > 0 || MaxPower > 0 && MinPower < MaxPower)
            {
                AddExplosionForce(power * 100, transform.position, Radius);
            }
        }

        private void AddExplosionForce(float expForce, Vector3 expPosition, float expRadius)
        {
            Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D body in bodies)
            {
                var dir = (body.transform.position - expPosition);
                float calc = 1 - (dir.magnitude / expRadius);
                if (calc <= 0)
                {
                    calc = MinUp;
                }
                //Debug.Log("calc: " + calc);
                Debug.Log("force " + dir.normalized * 1000 * calc);
                body.AddForce(dir.normalized * 1000 * calc);
            }
        }
    }
}
