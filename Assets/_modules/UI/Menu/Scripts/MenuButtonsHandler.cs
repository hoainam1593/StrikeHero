using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsHandler : MonoBehaviour {

	public AudioSource m_mainMenuSound;

	[Header("Main Menu")]
	public GameObject m_mainMenuButtons;
	public GameObject m_loadingIndicator;

	[Header("Gameplay")]
	public GameObject m_pauseScreen;
	public GameObject m_endGameScreen;

	void Awake() {
		GameStats._MenuButtonsHandler = this;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if ((m_pauseScreen != null) && (Input.GetButtonDown ("CancelEditor"))) {
		#else
		if ((m_pauseScreen != null) && (Input.GetButtonDown ("Cancel"))) {
		#endif
			OnPauseGame ();
		}
	}

	public void OnPlayButtonClicked () {
		m_mainMenuSound.Stop ();

		m_mainMenuButtons.SetActive (false);
		m_loadingIndicator.SetActive (true);

		StartCoroutine ("LoadPlaySceneAsync");
	}

	public void OnSettingsButtonClicked () {
	}

	public void OnQuitButtonClicked () {
		Utils.QuitGame ();
	}

	public void OnPauseGame () {
		Time.timeScale = 0.0f;
		LockCursor.Unlock ();
		m_pauseScreen.SetActive (true);
	}

	public void OnResumeButtonClicked () {
		Time.timeScale = 1.0f;
		LockCursor.Lock ();
		m_pauseScreen.SetActive (false);
	}

	public void OnEndGame () {
		if (m_endGameScreen != null) {
			Time.timeScale = 0.0f;
			LockCursor.Unlock ();
			m_endGameScreen.SetActive (true);
		}
	}

	public void OnMainMenuButtonClicked () {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene (0);
	}

	public void OnPlayAgainButtonClicked () {
		Time.timeScale = 1.0f;
		LockCursor.Lock ();
		m_endGameScreen.SetActive (false);

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	IEnumerator LoadPlaySceneAsync () {
		AsyncOperation info = SceneManager.LoadSceneAsync (1);

		while (!info.isDone) {
			yield return null;
		}
	}
}
