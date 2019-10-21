using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StardataCrusaders.ProjectIcarus {
	public class GameManager : MonoBehaviour {

		public static GameManager singleton;

		[HideInInspector] public int level = 1;

		[SerializeField] private Texture2D cursor;

		[SerializeField] private TextMeshProUGUI titleText;
		[SerializeField] private TextMeshProUGUI infoText;

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
		}

		public void StartLevel() {
			gameState = GameState.Selecting;
			titleText.text = "Choose scan area";
			infoText.text = "Select an area on the incomplete map at the bottom left to scan. You will then be tasked with defending the satallite to generate a clearer and larger image of the scanned area. Once you have made your selection, press the button below to begin scanning. Be prepared to defend the coming asteroids to protect your precious data!";
		}

		public void Quit() {
			Application.Quit();
		}

		public enum GameState {
			Neutral,
			Selecting,
			Active,
			End
		}
	}
}