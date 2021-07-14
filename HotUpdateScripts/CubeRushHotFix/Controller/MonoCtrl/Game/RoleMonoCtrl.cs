using JEngine.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
	public enum MoveState
    {
		Straight,
		Left,
		Right,
    }

	public class RoleMonoCtrl:MonoBehaviour
    {
		public Transform role;

		/// <summary>
		/// 动作进度
		/// </summary>
		private float moveActionProcess = 0;
		/// <summary>
		/// 移动速度
		/// </summary>
        private float moveSpeed = 2f;

		/// <summary>
		/// 下一个要做的动作
		/// </summary>
		public MoveState nextMoveState = MoveState.Straight;


		/// <summary>
		/// 角色控制器
		/// </summary>
		public RoleCtrl roleCtrl;

		private Coroutine rolerCoroutine;

		private void Start()
        {
			role = this.transform;
			StartMove();
		}

        private void Update()
        {
			RoleMove();
		}

		public void StopRole()
        {
			Time.timeScale = 0;

		}

		public void ContinueRole()
        {
			Time.timeScale = 1;

		}

		/// <summary>
		/// 转弯方法
		/// </summary>
		/// <param name="isLeft"></param>
		public void TurnLeftOrRight(bool isLeft)
        {
            if (isLeft)
            {
				nextMoveState = MoveState.Left;

            }
            else
            {
				nextMoveState = MoveState.Right;

			}
		}

		/// <summary>
		/// 直线方法
		/// </summary>
		public void GoStraight()
        {
			nextMoveState = MoveState.Straight;

		}

		private void RoleMove()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
				TurnLeftOrRight(true);
				return;
            }
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				TurnLeftOrRight(false);
				return;
			}

			if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
			{
				GoStraight();
				return;
			}
			if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
			{
				GoStraight();
				return;
			}
		}

		public void StartMove()
        {
			rolerCoroutine= StartCoroutine(MoveCurrency(new Vector3(role.transform.position.x, role.transform.position.y, role.transform.position.z + 1f), Vector3.right));
			//StartCoroutine(MoveCurrency(new Vector3(role.transform.position.x, role.transform.position.y, role.transform.position.z + 1f), Vector3.right));
		}

		IEnumerator MoveCurrency(Vector3 endp,Vector3 axis,bool needReSet=true)
		{
			//初始化
			float rotated = 0.0f;//设置旋转角度
			Vector3 endpos = endp;//设置结束位置
			nextMoveState = MoveState.Straight; //重置
			//持续滚动
			while (moveActionProcess < 0.4f)
			{
				role.transform.position = Vector3.Lerp(role.transform.position, endpos, moveActionProcess);
				moveActionProcess += Time.deltaTime * moveSpeed;
				float step = 90 / 0.5f * Time.deltaTime * 2f;
				role.transform.RotateAround(role.transform.position, axis, step);
				rotated += step;
				yield return null;
			}
			role.transform.RotateAround(role.transform.position, axis, 90 - rotated);
			role.transform.position = endpos;

			//结束
			moveActionProcess = 0;

			//这里判断下一个是什么,如果是转弯就转
			CheckNextMoveState();

		}
		
		private void CheckNextMoveState()
        {
			switch (nextMoveState)
			{
				case MoveState.Straight:
					StartCoroutine(MoveCurrency(new Vector3(role.transform.position.x, role.transform.position.y, role.transform.position.z + 1f), Vector3.right));
					break;
				case MoveState.Left:
					StartCoroutine(MoveCurrency(new Vector3(role.transform.position.x - 1f, role.transform.position.y, role.transform.position.z), Vector3.forward));
					break;
				case MoveState.Right:
					StartCoroutine(MoveCurrency(new Vector3(role.transform.position.x + 1f, role.transform.position.y, role.transform.position.z), Vector3.back));
					break;
				default:
					break;
			}
		}

		public bool isTriggerEnter = false;

		IEnumerator TriggerEnterIE()
        {
			yield return new WaitForSeconds(0.1f);
			isTriggerEnter = false;
		}

		private void OnTriggerEnter(Collider collider)
        {
			//碰到谁把谁干掉,保证只触发一次
			Log.Print("->"+ collider.name);
            if (isTriggerEnter)
            {
				return;
            }
			Destroy(collider.GetComponent<BoxCollider>());
			roleCtrl.OnTriggerEnter(collider);
			isTriggerEnter = true;
			StartCoroutine(TriggerEnterIE());
		}


		public bool isCollisionEnter = false;

		IEnumerator CollisionEnterIE()
		{
			yield return new WaitForSeconds(0.1f);
			isCollisionEnter = false;
		}
		private void OnCollisionEnter(Collision collision)
		{
			if (isCollisionEnter)
			{
				return;
			}
			Destroy(collision.collider.GetComponent<BoxCollider>());
			roleCtrl.OnCollisionEnter(collision);
			isCollisionEnter = true;
			StartCoroutine(CollisionEnterIE());
		}

	}
}
