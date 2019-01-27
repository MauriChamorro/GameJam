using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerPlatformerController _player;

        public GameObject Map1;
        public GameObject Map2;

        public Text _text;

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
                _text.gameObject.SetActive(true);
                _soundManager.SetMusicClipName("Lv 1 Noche");
                _soundManager.PlayMusicClipName();
            }
        }

        public void Respawn()
        {
            _soundManager.PlaySFXClipName("Error 5");
            _player.transform.position = _level.GetLastCheckPointPosition();
        }

        private void Update()
        {
        }

        public void ChangeMap()
        {
            print("ChangeMap(): " + _level._cantItems);

            if (_level._cantItems == 3)
            {
                _soundManager.SetMusicClipName("Lv complete");
                _soundManager.PlayMusicClipName();

                Map1.SetActive(false);

                Map2.SetActive(true);

                _level.SetStartCheckPoint(_startRespawn);

                _level._cantItems++;

                _text.text = "Thank you for playing";

            }
        }

    }
}
