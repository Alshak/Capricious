using UnityEngine;

namespace Assets.Code.Gibs
{
    public class BloodSpawner : MonoBehaviour
    {
        public int AmountOfBloodDrops = 10;

        public GameObject BloodDrop;

        void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < AmountOfBloodDrops; i++)
            {
                BloodDrop.transform.position = transform.position;
                BloodDrop.transform.rotation = transform.rotation;
                BloodDrop.transform.eulerAngles = new Vector3(0, 0, i + 10 * AmountOfBloodDrops);
                Instantiate(BloodDrop);
            }
        }
    }
}
