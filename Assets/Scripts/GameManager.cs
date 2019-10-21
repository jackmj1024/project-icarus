using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StardataCrusaders.ProjectIcarus {
	public class GameManager : MonoBehaviour {

		public static GameManager singleton;

		[HideInInspector] public int level = 1;

		[SerializeField] private float scanTime = 60f;
		private float scanTimer;

		[HideInInspector] public float dataCorruption = 0f;

		[SerializeField] private Texture2D cursor;

		[SerializeField] private TextMeshProUGUI titleText;
		[SerializeField] private TextMeshProUGUI infoText;
		[SerializeField] private Image scanBar;
		[SerializeField] private TextMeshProUGUI scanText;
		[SerializeField] private TextMeshProUGUI corruptionText;

		[SerializeField] private GameObject startScanButton;
		[SerializeField] private GameObject scanInfo;
		[SerializeField] private GameObject restartButton;

		[HideInInspector] public GameState gameState = GameState.Neutral;

		private void Awake() {
			Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
		}

		private void Start() {

			if(singleton == null) {
				singleton = this;
			} else {
				Destroy(this);
			}
		}

		private void Update() {
			if(Input.GetKeyDown(KeyCode.Escape)) {
				Quit();
			}

			if(gameState == GameState.Active) {
				scanTimer += Time.deltaTime;
				scanTimer = Mathf.Clamp(scanTimer, 0f, scanTime * level);
				scanBar.fillAmount = scanTimer / (scanTime * level);
				scanText.text = "Scanning: " + Mathf.RoundToInt(((scanTimer / (scanTime * level)) * 100f)).ToString() + "%";

				dataCorruption = Mathf.Clamp(dataCorruption, 0f, 100f);
				corruptionText.text = "Data Corruption: " + dataCorruption + "%";

				if(scanTimer >= scanTime * level || dataCorruption >= 100) {
					StopScan();
				}
			}
		}

		public void StartLevel() {
			startScanButton.SetActive(true);
			scanInfo.SetActive(false);
			infoText.gameObject.SetActive(true);
			dataCorruption = 0f;
			gameState = GameState.Selecting;
			titleText.text = "Choose scan area";
			infoText.text = "Select an area on the incomplete map at the bottom left to scan. You will then be tasked with defending the satallite to generate a clearer and larger image of the scanned area. Once you have made your selection, press the button below to begin scanning. Be prepared to defend the coming asteroids to protect your precious data!";
		}

		public void StartScan() {
			gameState = GameState.Active;
			titleText.text = "Scanning area...";
			scanTimer = 0f;
		}

		public void StopScan() {
			gameState = GameState.End;

			if (dataCorruption >= 100)
				titleText.text = "Scan Corrupted!";
			else
				titleText.text = "Scan Completed!";
		}

		public void FinishGame() {
			scanInfo.gameObject.SetActive(false);
			infoText.gameObject.SetActive(true);
			restartButton.SetActive(true);
			dataCorruption = 0f;
			titleText.text = "Scans Completed!";
			infoText.text = "You have completed all 3 scans. If you did a good job at defending, you should have a fairly expansive map of the Earth. However, you may wish to increase your view range. Press the button below to restart if you would like to play again.";
		}

		public void Quit() {
			Application.Quit();
		} public void Restart() {
			Application.LoadLevel(Application.loadedLevel); // I know this is obsolete but we're really short on time
		}

		public enum GameState {
			Neutral,
			Selecting,
			Active,
			End
		}
	}
}