using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace StardataCrusaders.ProjectIcarus {
	public class MapManager : MonoBehaviour, IPointerClickHandler {

		[SerializeField] private Image pointMarker;

		public void OnPointerClick(PointerEventData data) {
			if (GameManager.singleton.gameState == GameManager.GameState.Selecting) {
				pointMarker.gameObject.SetActive(true);
				pointMarker.rectTransform.position = Input.mousePosition;
			}
		}
	}
}