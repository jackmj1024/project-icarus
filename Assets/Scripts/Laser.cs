using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardataCrusaders.ProjectIcarus {
	public class Laser : MonoBehaviour {

		[SerializeField] private float speed = 10f;

		private void Update() {
			transform.position += transform.up * speed * Time.deltaTime;
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			Destroy(gameObject);
		}

		private void OnTriggerEnter(Collider other) {
			if(other.gameObject.tag == "Bounds") {
				Destroy(gameObject);
			}
		}

	}
}