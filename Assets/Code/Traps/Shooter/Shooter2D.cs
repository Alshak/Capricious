using Assets.Code.Spawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps.Shooter
{
    public class Shooter2D : MonoBehaviour
    {
        public List<GameObject> ListObjectsToShoot;
        private List<Spawnpoint> ListSpawnpoints;

        [Range(0f, 10f)]
        public float DelayPerShot;

        [Range(0.1f, 10f)]
        public float DelayPerVolley;

        private int objectsFired = 0;
        private float cooldown = 0;

        void Start()
        {
            var spawnPoints = GetComponentsInChildren<Spawnpoint>();
            ListSpawnpoints = new List<Spawnpoint>(spawnPoints);
        }

        void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                if (objectsFired < ListObjectsToShoot.Count)
                {
                    if (DelayPerShot == 0)
                    {
                        for (int i = 0; i < ListObjectsToShoot.Count; i++)
                        {
                            SpawnObject();
                        }
                    }
                    else
                    {
                        SpawnObject();
                    }
                }
            }
        }

        private void SpawnObject()
        {
            objectsFired++;
            cooldown = DelayPerShot;

            var spawnObject = GetObjectToShoot();
            spawnObject.transform.position = GetSpawnPos();
            Instantiate(spawnObject);

            if (objectsFired >= ListObjectsToShoot.Count)
            {
                objectsFired = 0;
                cooldown = DelayPerVolley;
            }
        }
        
        private GameObject GetObjectToShoot()
        {
            if (objectsFired < ListObjectsToShoot.Count)
            {
                return ListObjectsToShoot[objectsFired];
            }
            return ListObjectsToShoot[0];
        }

        private Vector3 GetSpawnPos()
        {
            if (objectsFired < ListSpawnpoints.Count)
            {
                return ListSpawnpoints[objectsFired].transform.position;
            }
            return ListSpawnpoints[0].transform.position;
        }
    }
}
