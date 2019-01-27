using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerPlatformerController _player;

        public GameObject _menuPanel;
        public SoundManager _soundManager;

        public Level _level;
        public HUD _hud;

        #region Lerp
        //public float currentClockLerp;
        //public float percClock;
        //public Transform minHealth;
        //public float speedTimeHealth;
        //public float perc; 
        #endregion

        private bool playing;


        private void Awake()
        {
            _soundManager = SoundManager.Instance;

            _player.transform.position = _level.GetLastCheckPointPosition();
            
            //SetGame();

            //SetLevel();
        }

        //private void Update()
        //{
        //    if (Input.GetButtonDown("Cancel"))
        //    {
        //        MenuPanel.SetActive(MenuPanel.gameObject.activeSelf ? false : true);
        //        uiAnimator.SetBool("ShowMenu", MenuPanel.gameObject.activeSelf ? true : false);

        //        //GeneralGameValues.StopGame();

        //        soundManager.PlaySFXClipName("menu-game");
        //    }

        //    if (!playing)
        //    {
        //        #region countToPlayText
        //        if (countToPlayText.enabled)
        //        {
        //            if (countToPlay.OnTime())
        //            {
        //                StartGame();

        //                trajectoryManager.StartGame(currentLevel.CantElementSpanw);

        //                return;
        //            }

        //            countToPlay.Update(Time.deltaTime);
        //            countToPlayText.text = countToPlay.TimeOnSeconds(CustomTimer.TimeType.CurrentDecTime);

        //            if (countToPlayText.text == "3")
        //            {
        //                //uiAnimator.SetBool("Count", true);
        //                //imagen 1
        //            }

        //            if (countToPlayText.text == "2")
        //            {
        //                //uiAnimator.SetBool("Count", true);

        //                //imagen 2
        //            }

        //            if (countToPlayText.text == "1")
        //            {
        //                //uiAnimator.SetBool("Count", true);
        //                //imagen 3
        //            }

        //            if (countToPlayText.text == "0")
        //            {
        //                //uiAnimator.SetBool("Count", true);
        //                countToPlayText.text = "GO";
        //                //imagen go
        //            }
        //        }
        //        #endregion
        //    }

        //    if (playing)
        //    {
        //        if (Clock.value <= 0f && health.transform.position.y > minHealth.transform.position.y/*health.transform.position.y >= currentLevel.MaxHealth*/)
        //        {
        //            if (currentLevel.GameWon())
        //            {
        //                GameOver(true);

        //                return;
        //            }

        //            LevelUp();

        //            return;
        //        }
        //        else
        //        if (health.transform.position.y <= -9.5f)
        //        {
        //            GameOver(false);

        //            return;
        //        }

        //        if (Input.GetButton("Fire1"))
        //        {
        //            RayOnElement();
        //        }

        //        #region Lerp for Clock
        //        currentClockLerp += Time.deltaTime;
        //        if (currentClockLerp > currentLevel.ClockTime)
        //        {
        //            currentClockLerp = currentLevel.ClockTime;
        //        }

        //        percClock = currentClockLerp / currentLevel.ClockTime;

        //        Clock.value = Mathf.Lerp(currentLevel.ClockTime, 0f, percClock);
        //        #endregion

        //        #region Lerp for Health
        //        currentHealthTime += Time.deltaTime * speedTimeHealth;

        //        if (currentHealthTime > journeyLength)
        //        {
        //            currentHealthTime = journeyLength;
        //        }

        //        perc = currentHealthTime / journeyLength;

        //        health.transform.position = Vector3.Lerp(
        //            health.transform.position,
        //            minHealth.position,
        //            perc);
        //        #endregion
        //    }
        //}

        //private void SetGame()
        //{
        //    playing = false;
        //    GeneralGameValues.ResumeGame();

        //    if (countToPlay == null)
        //        countToPlay = new CustomTimer(GeneralGameValues.TiempoConteo);
        //    else
        //        countToPlay.Restart(GeneralGameValues.TiempoConteo);

        //    countToPlayText.enabled = true;

        //    if (currentLevel == null)
        //        currentLevel = new Level();
        //}

        //private void SetLevel()
        //{
        //    gameOverText.enabled = false;

        //    health.transform.position = new Vector3(0f, currentLevel.MaxHealth, 93f);
        //    currentHealthTime = 0f;
        //    speedTimeHealth = currentLevel.SpeedHealth;
        //    journeyLength = Vector3.Distance(health.transform.position, minHealth.position);

        //    currentClockLerp = 0f;
        //    Clock.maxValue = currentLevel.ClockTime;
        //    Clock.value = currentLevel.ClockTime;

        //    switch (currentLevel.NumLevel)
        //    {
        //        case 1:
        //            levelText.text = "Nivel 1";
        //            break;
        //        case 2:
        //            {
        //                levelText.text = "Nivel 2";
        //                uiAnimator.SetTrigger("LevelUp");
        //            }
        //            break;
        //        case 3:
        //            {
        //        uiAnimator.SetTrigger("LevelUp");
        //                levelText.text = "Nivel 3";
        //                uiAnimator.SetTrigger("LevelUp");
        //            }
        //            break;
        //        default:
        //            levelText.text = "ERROR";
        //            break;
        //    }
        //}

        //public void StartGame()
        //{
        //    countToPlayText.enabled = false;
        //    playing = true;
        //}

        //public void RestartGame()
        //{
        //    //soundManager.PlaySFXClipName("back-menu");

        //    MenuPanel.SetActive(false);

        //    currentLevel = null;

        //    SetGame();

        //    SetLevel();

        //    trajectoryManager.RestartGame(currentLevel.CantElementSpanw);

        //}

        //private void StopGame(bool pValue)
        //{
        //    playing = pValue;

        //    if (pValue)
        //        GeneralGameValues.ResumeGame();
        //    else
        //        GeneralGameValues.StopGame();
        //}

        //private void LevelUp()
        //{
        //    soundManager.PlaySFXClipName("levelUp");

        //    SetGame();

        //    currentLevel.LevelUp();

        //    trajectoryManager.RestartGame(currentLevel.CantElementSpanw);

        //    SetLevel();
        //}

        //private void GameOver(bool pWon)
        //{
        //    if (pWon)
        //    {
        //        gameOverText.text = "¡Ganaste! :]";
        //        soundManager.PlaySFXClipName("win1");
        //    }
        //    else
        //    {
        //        gameOverText.text = "Perdiste :'[";
        //        soundManager.PlaySFXClipName("lose1");
        //    }

        //    gameOverText.enabled = true;
        //    StopGame(false);

        //    MenuPanel.SetActive(MenuPanel.gameObject.activeSelf ? false : true);
        //    uiAnimator.SetBool("ShowMenu", MenuPanel.gameObject.activeSelf ? true : false);

        //    //GeneralGameValues.StopGame();
        //    //soundManager.PlaySFXClipName("menu-game");
        //}

        //private void RayOnElement()
        //{
        //    Ray hit = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit rhit;

        //    if (Physics.Raycast(hit, out rhit) && rhit.transform.gameObject.tag == "Element")
        //    {
        //        DetermineEfect(rhit.transform.name);

        //        rhit.transform.gameObject.SetActive(false);
        //    }
        //}

        //public void BackToMenu()
        //{
        //    soundManager.PlaySFXClipName("back-menu");
        //    SceneManager.LoadScene("Menu");
        //}


        //#region  UI Events
        //public void PointerEnterText(string pNameText)
        //{
        //    Text auxText = textButtons.FirstOrDefault(t => t.name == pNameText);
        //    if (auxText)
        //    {
        //        auxText.fontSize = 60;
        //        soundManager.PlaySFXClipName("over-button");
        //    }
        //}

        //public void PointerExitText(string pNameText)
        //{
        //    Text auxText = textButtons.FirstOrDefault(t => t.name == pNameText);
        //    if (auxText)
        //    {
        //        auxText.fontSize = 50;
        //    }
        //}
        //#endregion
    }
}
