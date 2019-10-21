using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardataCrusaders.ProjectIcarus {
	public class Earth : MonoBehaviour {

		[SerializeField] private List<Meteor> meteors = new List<Meteor>();
		[SerializeField] private List<SpawnArea> spawnAreas = new List<SpawnArea>();

		public float spawnDelay = 0.1f;
		private float spawnTimer;

		private Rigidbody2D rb;

		[HideInInspector] public List<Meteor> spawnedMeteors = new List<Meteor>();

		private void Awake() {
			rb = GetComponent<Rigidbody2D>();
		}
		private void Start() {
			spawnTimer = spawnDelay;
		}

		private void Update() {

			if(GameManager.singleton.gameState != GameManager.GameState.Active) {
				if(spawnedMeteors.Count > 0) {
					foreach(Meteor _m in spawnedMeteors) {
						Destroy(_m.gameObject);
					}
					spawnedMeteors.Clear();
				}

				return;
			}

			spawnDelay = Mathf.Clamp(spawnDelay, 0.5f, 10f);

			// Normally we would just use InvokeRepeating() for this, but that doesn't work here since the spawn rate will be gradually increasing.
			if(spawnTimer <= 0f) {
				for (int i = 0; i < GameManager.singleton.level; i++) {
					SpawnMeteors();
				}
				spawnTimer = spawnDelay;
			} else {
				spawnTimer -= Time.deltaTime;
			}
		}

		private void SpawnMeteors() {

			if (spawnedMeteors.Count >= 20 * GameManager.singleton.level)
				return;

			SpawnArea _spawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
			Vector3 _position = new Vector3(Random.Range(_spawnArea.minX, _spawnArea.maxX), Random.Range(_spawnArea.minY, _spawnArea.maxY), 0f);

			Meteor _meteor = Instantiate(meteors[Random.Range(0, meteors.Count)].gameObject, _position, Quaternion.identity).GetComponent<Meteor>();
			spawnedMeteors.Add(_meteor);

			_meteor.earth = this;
			_meteor.earthRB = rb;
		}

		[System.Serializable]
		public struct SpawnArea {
			public float minX;
			public float maxX;
			public float minY;
			public float maxY;
		}
	}
}