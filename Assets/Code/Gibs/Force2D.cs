using UnityEngine;

namespace Assets.Code.Gibs
{
    public class Force2D : MonoBehaviour
    {
        [Range(0, 1000)]
        public float MinForce;

        [Range(0, 1000)]
        public float MaxForce;

        void Start()
        {
            Rigidbody2D body2D = GetComponent<Rigidbody2D>();
            
            if (body2D != null)
            {
                body2D.AddForce(GetRandomForce()); //GetRandomForce()
            }
        }

        private Vector2 GetRandomForce()
        {
            return new Vector2(Random.Range(MinForce, MaxForce), Random.Range(MinForce, MaxForce));
        }
    }
}
