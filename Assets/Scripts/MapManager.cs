using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace StardataCrusaders.ProjectIcarus {
	public class MapManager : MonoBehaviour, IPointerClickHandler {

		[SerializeField] private Image pointMarker;
		[SerializeField] private GameObject mapPrefab;
		[SerializeField] private Transform mapParent;

		[SerializeField] private TextMeshProUGUI levelText;

		[SerializeField] private List<Image> scans = new List<Image>();

		[SerializeField] private float revealSpeed = 10f;

		private bool done = false;

		private void Update() {
			if(GameManager.singleton.gameState == GameManager.GameState.End) {
				if(!done)
					StartCoroutine(ShowResults());
			} else {
				if(done) {
					done = false;
				}
			}
		}

		public void OnPointerClick(PointerEventData data) {
			if (GameManager.singleton.gameState == GameManager.GameState.Selecting) {
				pointMarker.rectTransform.position = Input.mousePosition;
			}
		}

		private IEnumerator ShowResults() {
			done = true;

			levelText.text = "Points Scanned: " + GameManager.singleton.level + "/3";

			float _goalSize = 400f * (1 - (GameManager.singleton.dataCorruption / 100));
			Image _scan = scans[GameManager.singleton.level - 1];

			_scan.transform.position = pointMarker.transform.position;

			GameObject _map = Instantiate(mapPrefab);
			_map.transform.SetParent(mapParent, false);
			_map.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			_map.transform.SetParent(_scan.transform, true);

			while(_scan.rectTransform.sizeDelta.x < _goalSize || _scan.rectTransform.sizeDelta.y < _goalSize) { 
				if(_scan.rectTransform.sizeDelta.x < _goalSize)
					_scan.rectTransform.sizeDelta += new Vector2(revealSpeed, 0f);
				if (_scan.rectTransform.sizeDelta.y < _goalSize)
					_scan.rectTransform.sizeDelta += new Vector2(0f, revealSpeed);
				yield return new WaitForEndOfFrame();
			}

			yield return new WaitForSeconds(3f);

			GameManager.singleton.level++;

			if (GameManager.singleton.level < 4)
				GameManager.singleton.StartLevel();
			else
				GameManager.singleton.FinishGame();
		}
	}
}