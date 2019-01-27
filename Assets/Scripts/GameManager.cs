using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerPlatformerController _player;

        public GameObject Map1;
        public GameObject Map2;

        public Transform _startRespawn;

        public GameObject _menuPanel;
        public SoundManager _soundManager;

        public Level _level;
        public HUD _hud;

        private bool playing;


        private void Awake()
        {
           
            _player.transform.position = _level.GetLastCheckPointPosition();
            
        }

        private void Start()
        {
            _soundManager = SoundManager.Instance;
            _soundManager.SetMusicClipName("Lv 1 Día");
            _soundManager.PlayMusicClipName();
        }

        public void CheckPointCatched(Transform pCheck)
        {
            _soundManager.PlaySFXClipName("Select o pick up");
            _level.CheckPoint(pCheck);
            pCheck.GetComponent<Renderer>().enabled = false;
            pCheck.GetComponent<CircleCollider2D>().enabled = false;

            if (_level._cantItems == 3)
            {
                //eventos para volver
            }
        }

        public void Respawn()
        {
            _soundManager.PlaySFXClipName("Error 5");
            _player.transform.position = _level.GetLastCheckPointPosition();
        }

        private void Update()
        {
            if (_level._cantItems == 3 )
            {
                _soundManager.SetMusicClipName("Lv 1 Noche");
            }
        }

        private void ChangeMap()
        {
            Map1.SetActive(false);

            Map1.SetActive(true);

            _level.SetStartCheckPoint(_startRespawn);
        }

    }
}
