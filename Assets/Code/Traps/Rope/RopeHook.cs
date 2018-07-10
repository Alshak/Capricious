using UnityEngine;

namespace Assets.Code.Traps.Rope
{ 
    public class RopeHook : MonoBehaviour
    {
        //hook prefab
        public GameObject hook;
        private GameObject curHook;

        void Start()
        {
            //Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DebugSprite start = GetComponentInParent<DebugSprite>();
            //curHook = Instantiate(hook, transform.position, Quaternion.identity);
            //curHook.GetComponent<Rope2D>().destiny = start.transform.position;
            
        }
    }
}