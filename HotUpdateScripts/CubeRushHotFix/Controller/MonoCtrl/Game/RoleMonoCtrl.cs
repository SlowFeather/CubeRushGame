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
		/// 角色控制器
		/// </summary>
		public RoleCtrl roleCtrl;

		private void Start()
        {
			role = this.transform;
		}

		/// <summary>
		/// 是否碰撞
		/// </summary>
		public bool isTriggerEnter = false;

		IEnumerator TriggerEnterIE()
        {
			//防止多次碰撞
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

		/// <summary>
		/// 是否碰撞
		/// </summary>
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
