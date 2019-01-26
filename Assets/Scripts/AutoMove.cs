using UnityEngine;

namespace Assets.Scripts
{
    public class AutoMove : PhysicsObject
    {
        private void Update()
        {
            targetVelocity = Vector2.left;
        }
    }
}
