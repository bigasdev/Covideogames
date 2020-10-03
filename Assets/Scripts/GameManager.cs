using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public static GameManager gameManager;

	public GameObject winScreen;
	public GameObject loseScreen;

	AudioSource musicAudioSource;
	public AudioClip victorySFX;
	public AudioClip cheerSFX;

	public bool gameIsOver = false;




	public float currentTime;
	// Start is called before the first frame update
	void Awake()
	{
		// set the current time to the startTime specified
		currentTime = 0;

		// get a reference to the GameManager component for use by other scripts
		if (gameManager == null)
			gameManager = this.gameObject.GetComponent<GameManager>();

		musicAudioSource = this.gameObject.GetComponent<AudioSource>();


	}

	// Update is called once per frame
	void Update()
	{
		if (!gameIsOver)
		{

			if (GoalTracker.goalTracker.goalsFulfilled)
			{  // check to see if beat game

				BeatLevel();
			}

			else
			{ // game playing state, so update the timer
				currentTime += Time.deltaTime;

				if (GameStats.gameStats.peopleDead > 0)
				{
					EndGame();
				}

			}
		}
	}
	public bool ShouldWin()
	{
		if (GameStats.gameStats.score > 200) return true;
		else return false;
	}
	public void EndGame()
	{
		// game is over
		gameIsOver = true;

		if (loseScreen) loseScreen.SetActive(true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource) musicAudioSource.pitch = 0.5f; // slow down the music
		if (ShouldWin())
		{
			PlayVictorySound();
		}
	}
	void BeatLevel()
	{

		// game is over
		gameIsOver = true;


		// activate the gameOverScoreOutline gameObject, if it is set 
		if (winScreen) winScreen.SetActive(true);


		// reduce the pitch of the background music, if it is set 
		PlayVictorySound();

	}

	private void PlayVictorySound()
	{
		if (musicAudioSource)
		{
			musicAudioSource.pitch = 0.5f; // slow down the music
			AudioSource.PlayClipAtPoint(victorySFX, Camera.main.transform.position);
			if (SceneManager.GetActiveScene().name == "Park Hard")
			{
				AudioSource.PlayClipAtPoint(cheerSFX, Camera.main.transform.position);
			}
		}
	}
}