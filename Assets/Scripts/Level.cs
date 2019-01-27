using UnityEngine;

namespace Assets.Scripts
{
    public class Level : MonoBehaviour
    {
        public SpriteRenderer _houseRenderer;

        public Sprite[] _houseSprites;
        public Transform lastCheckPoint;

        public int _cantItems;

        private void Start()
        {
            _cantItems = -1;
            CheckPoint(lastCheckPoint);
        }

        public void SetStartCheckPoint(Transform pCheckPointPos)
        {
            lastCheckPoint = pCheckPointPos;
        }

        public void CheckPoint(Transform pCheckPointPos)
        {
            lastCheckPoint = pCheckPointPos;
            _cantItems++;
            _houseRenderer.sprite = _houseSprites[_cantItems];
        }

        public Vector3 GetLastCheckPointPosition()
        {
            return lastCheckPoint.position;
        }

    }
}
