using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StardataCrusaders.ProjectIcarus {
	public class Ship : MonoBehaviour {

		[SerializeField] private float speed = 6f;
		[SerializeField] private float fireDelay = 1f;

		[SerializeField] private Transform moveTarget;
		[SerializeField] private Transform fireTransform;
		[SerializeField] private Laser laser;

		private float fireTimer = 0f;

		private void Update() {
			if(Input.GetButton("Horizontal")) {
				Move();
			}

			if(Input.GetButton("Fire") && fireTimer <= 0f) {
				Fire();
				fireTimer = fireDelay;
			} else {
				fireTimer -= Time.deltaTime;
			}
		}

		private void Move() {
			float _angle = -Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
			transform.RotateAround(moveTarget.position, Vector3.forward, _angle);
		}

		private void Fire() {
			GameObject _laser = Instantiate(laser.gameObject, fireTransform);
			_laser.transform.parent = null;
		}
	}
}