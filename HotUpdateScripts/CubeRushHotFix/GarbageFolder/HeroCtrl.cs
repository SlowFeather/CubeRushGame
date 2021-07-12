using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class HeroCtrl:MonoBehaviour
    {
		public Transform hero;
		public bool isLeft;
		public bool isRight;
		public bool forward = true;
		public float speed = 5f;


		public float process;
		public bool sMove = false;
		public bool canmove = true;

		RaycastHit hit;

		public bool isSword;
		public bool isShield;
		public bool isAxe;
		public bool isSpeed;
		public bool isFist;
		public bool dropWater = false;

		public bool hitLwall = false;
		public bool hitRwall = false;
		public bool isAliveOnce = false;

		public Vector3 diePos;
		public Vector3 endpos;

		public int heroId;
		Rigidbody heroRigid;

		// Use this for initialization
		void Start()
		{
			isAliveOnce = false;
			hero = this.transform;
			heroRigid = GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			MoveHero();
		}




		IEnumerator MoveLeft()
		{
			canmove = false;
			process = 0;
			float rotated = 0.0f;
			while (process < 0.4f)
			{
				hero.transform.position = Vector3.Lerp(hero.transform.position, endpos, process);
				process += Time.deltaTime * speed;
				float step = 90 / 0.5f * Time.deltaTime * 2;
				transform.RotateAround(transform.position, Vector3.forward, step);
				//	elapsed += Time.deltaTime;
				rotated += step;
				yield return null;
			}
			transform.RotateAround(transform.position, Vector3.forward, 90 - rotated);
			hero.transform.position = endpos;
			sMove = false;
			canmove = true;
		}

		IEnumerator MoveForwad()
		{
			canmove = false;
			process = 0;
			float rotated = 0.0f;
			while (process < 0.4f)
			{
				hero.transform.position = Vector3.Lerp(hero.transform.position, endpos, process);
				process += Time.deltaTime * speed;
				float step = 90 / 0.5f * Time.deltaTime * 2f;
				transform.RotateAround(transform.position, Vector3.right, step);
				//elapsed += Time.deltaTime;
				rotated += step;
				yield return null;
			}
			transform.RotateAround(transform.position, Vector3.right, 90 - rotated);
			hero.transform.position = endpos;
			sMove = false;
			canmove = true;
		}

		IEnumerator MoveRight()
		{
			canmove = false;
			process = 0;
			float rotated = 0.0f;
			while (process < 0.4f)
			{
				hero.transform.position = Vector3.Lerp(hero.transform.position, endpos, process);
				process += Time.deltaTime * speed;
				float step = 90 / 0.5f * Time.deltaTime * 2;
				transform.RotateAround(transform.position, Vector3.back, step);
				//elapsed += Time.deltaTime;
				rotated += step;
				yield return null;
			}
			transform.RotateAround(transform.position, Vector3.back, 90 - rotated);
			hero.transform.position = endpos;
			sMove = false;
			canmove = true;
		}

		/// <summary>
		/// Moves the hero.
		/// </summary>
		void MoveHero()
		{
			if (isLeft && canmove) // move left 
			{

				if (Physics.Raycast(transform.position, Vector3.left + new Vector3(0, 0, 0.3f), out hit, 1f))
				{   //check the left have any block?

					if (hit.collider.tag == "co")
					{
						forward = true;
						isLeft = false;
						return;
					}
				}
				if (Physics.Raycast(transform.position, Vector3.left + new Vector3(0, 0, -0.3f), out hit, 1f))
				{
					if (hit.collider.tag == "co")
					{
						forward = true;
						isLeft = false;
						return;
					}
				}
				if (!sMove)
				{
					endpos = new Vector3(hero.transform.position.x - 1f, hero.transform.position.y, hero.transform.position.z);
					process = 0;
					sMove = true;
				}
				if (sMove)
				{
					StartCoroutine(MoveLeft());
					isLeft = false;
					sMove = false;
					forward = true;
				}
			}
			else if (forward && canmove)// move forward
			{
				if (Physics.Raycast(transform.position, Vector3.forward + new Vector3(0.3f, 0, 0f), out hit, 0.8f))
				{
					if (hit.collider.tag == "co")
					{
						forward = false;
						return;
					}
				}
				if (Physics.Raycast(transform.position, Vector3.forward + new Vector3(-0.3f, 0, 0f), out hit, 0.8f))
				{
					if (hit.collider.tag == "co")
					{
						forward = false;
						return;
					}
				}
				if (!sMove)
				{
					endpos = new Vector3(hero.transform.position.x, hero.transform.position.y, hero.transform.position.z + 1f);
					process = 0;
					sMove = true;
				}
				if (sMove)
				{
					StartCoroutine(MoveForwad());
					forward = true;
					sMove = false;
				}
			}
			else if (isRight && canmove)
			{
				if (Physics.Raycast(transform.position, Vector3.right + new Vector3(0, 0, 0.3f), out hit, 1))
				{

					if (hit.collider.tag == "co")
					{
						forward = true;
						isRight = false;
						return;
					}
				}
				if (Physics.Raycast(transform.position, Vector3.right + new Vector3(0, 0, -0.3f), out hit, 1f))
				{

					forward = true;
					isRight = false;
					return;
				}
				if (!sMove)
				{
					endpos = new Vector3(hero.transform.position.x + 1f, hero.transform.position.y, hero.transform.position.z);
					process = 0;
					sMove = true;
				}
				if (sMove)
				{
					StartCoroutine(MoveRight());
					forward = true;
					sMove = false;
					isRight = false;
				}
			}
		}


		void OnTriggerEnter(Collider other)
		{
			if (other.tag.Equals("gold"))//get the gold
			{

			}
			if (other.tag.Equals("shield"))//get the shidle
			{
				
			}
			if (other.tag.Equals("sword"))//get the sword
			{
				
			}
			if (other.tag.Equals("axe"))//hit by enemy
			{
				
			}
			if (other.tag.Equals("Hp"))//get the hp
			{
				
			}
			if (other.tag.Equals("bullet"))//hit by enemy
			{
				hurtMe();
			}
			if (other.tag.Equals("enemy"))//hit by enemy
			{
				hurtMe();
			}
			if (other.tag.Equals("jianchi"))
			{
				hurtMe();
			}

			if (other.tag.Equals("water"))
			{
				dropWater = true;
				activeGravityKinemati(false);
				StartCoroutine(waitDie(2));
				other.gameObject.SetActive(false);
				//Debug.Log ("drop Water");
			}


			if (other.tag.Equals("lco"))
			{
				hitLwall = true;
				activeGravityKinemati(false);
				StartCoroutine(waitDie(2));

			}

			if (other.tag.Equals("rco"))
			{
				hitRwall = true;
				activeGravityKinemati(false);
				StartCoroutine(waitDie(2));
			}
			if (other.tag.Equals("fist"))
			{

				if (isFist)
				{
					return;
				}
				isFist = true;
			}

			if (other.tag.Equals("speed"))
			{
				if (isSpeed)
				{
					return;
				}
				isSpeed = true;
			}
		}


		void hurtMe()
		{

			if (isShield)
			{
				isShield = false;
			}
			else
			{
				diePos = transform.position;
				die();
			}
		}

		IEnumerator waitDie(float waitime)
		{
			diePos = transform.position;
			yield return new WaitForSeconds(waitime);
			die();

		}

		IEnumerator dieTiwce(float waitime)
		{
			this.gameObject.SetActive(false);
			yield return new WaitForSeconds(waitime);
		}


		void die()
		{
			if (isAliveOnce)
			{
				StartCoroutine(dieTiwce(1));
				return;
			}
			this.gameObject.SetActive(false);
		}


		public void activeGravityKinemati(bool check)
		{
			heroRigid.isKinematic = check;
			heroRigid.useGravity = !check;
		}

	}
}
