using UnityEngine;

namespace Assets.Scripts
{
    public class Level : MonoBehaviour
    {
        public SpriteRenderer _houseRenderer;

        public Sprite[] _houseSprites;
        public Transform lastCheckPoint;

        private int _cantItems;

        private void Awake()
        {
        }

        private void Start()
        {
            _cantItems = -1;
            CheckPoint(lastCheckPoint);

            print(GetLastCheckPointPosition());
        }

        public void CheckPoint(Transform pCheckpointPos)
        {
            lastCheckPoint = pCheckpointPos;
            _cantItems++;
            _houseRenderer.sprite = _houseSprites[_cantItems];
        }

        public Vector3 GetLastCheckPointPosition()
        {
            return lastCheckPoint.position;
        }
    }
}
