using UnityEngine;

namespace Assets.Scripts
{
    public class DeadCam : MonoBehaviour
    {
        public Transform target;
        public float rLimit;
        public float lLimit;

        private void Start()
        {
            transform.position = target.position;
        }

        private void Update()
        {
            if (true)
            {

            }

            Vector3 newPos = new Vector3(0, target.position.y, target.position.z);
        }
    }
}
