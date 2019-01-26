using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerItem : MonoBehaviour
    {
        public List<KeyItem.KeyItems> Items;

        // Use this for initialization
        void Start()
        {
            KeyItem.ItemCollected += OnItemCollected;
        }

        private void OnItemCollected(KeyItem.KeyItems key)
        {
            Items.Add(key);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
