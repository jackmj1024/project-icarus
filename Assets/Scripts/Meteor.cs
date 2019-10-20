using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardataCrusaders.ProjectIcarus {
	public class Meteor : MonoBehaviour {

		[SerializeField] private int health = 6;
	//	[SerializeField] private Vector2 

		[SerializeField] private AudioClip hitClip;
		[SerializeField] private AudioClip deathClip;

		private AudioSource audio;
		private SpriteRenderer renderer;
		private Rigidbody2D rb;

		[HideInInspector] public Earth earth;
		[HideInInspector] public Rigidbody2D earthRB;

		private void Awake() {
			audio = GetComponent<AudioSource>();
			renderer = GetComponent<SpriteRenderer>();
			rb = GetComponent<Rigidbody2D>();
		}

		private void Start() {
			float _xDir = Random.Range(-8f, 8f);
			float _yDir = Random.Range(-4f, 4f);

			rb.AddForce(new Vector3(_xDir, _yDir) * 200f);
		}

		private void FixedUpdate() {
			SimulateGravity();
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			if(collision.gameObject.tag == "Attack") {
				StartCoroutine(TakeDamage());
			}
		}

		private void SimulateGravity() {
			Vector3 _direction = earthRB.position - rb.position;
			float _distance = _direction.magnitude;

			float _forceMagnitute = (rb.mass * earthRB.mass) / Mathf.Pow(_distance, 2);
			Vector3 _force = _direction.normalized * _forceMagnitute;

			rb.AddForce(_force);
		}

		private IEnumerator TakeDamage() {
			health--;
			if (health <= 0) {
				audio.PlayOneShot(deathClip);
				renderer.enabled = false;
				yield return new WaitForSeconds(0.1f);
				earth.spawnDelay -= 0.1f;
				Destroy(gameObject);
			} else {
				audio.PlayOneShot(hitClip);
				renderer.color = Color.red;
				yield return new WaitForSeconds(0.2f);
				audio.Stop();
				renderer.color = Color.white;
			}


		}

	}
}