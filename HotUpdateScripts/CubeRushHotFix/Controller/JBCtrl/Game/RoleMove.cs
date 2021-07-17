using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts
{
    public class RoleMove: JBehaviour
    {
		public Transform role;

		/// <summary>
		/// 移动速度
		/// </summary>
		private float moveSpeed = 2f;

		/// <summary>
		/// 下一个要做的动作
		/// </summary>
		public MoveState nextMoveState = MoveState.Straight;

        public override void Init()
        {
			this.FrameMode = false;
			this.Frequency = 1;
			role = this.gameObject.transform;
		}

        #region 转弯
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
        #endregion

        public override void Run()
		{

		}
		/// <summary>
		/// 移动控制
		/// </summary>
		private void RoleMoveFunction()
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

			if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
			{
				GoStraight();
				return;
			}
			if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
			{
				GoStraight();
				return;
			}
		}
		public override void Loop()
        {
			RoleMoveFunction();
			RoleMoveWorker();
		}
		/// <summary>
		/// 是否开始动作
		/// </summary>
		bool isMoveAction = false;
		/// <summary>
		/// 结束位置
		/// </summary>
		Vector3 endpos;
		/// <summary>
		/// 旋转轴
		/// </summary>
		Vector3 axis;
		/// <summary>
		/// 旋转角度
		/// </summary>
		float rotated=0;
		/// <summary>
		/// 动作进度
		/// </summary>
		private float moveActionProcess = 0;
		public void RoleMoveWorker()
        {
            if (isMoveAction==true)
            {
				//还在运动中
				if (moveActionProcess<0.4f)
                {
					role.transform.position = Vector3.Lerp(role.transform.position, endpos, moveActionProcess);
					moveActionProcess += Time.deltaTime * moveSpeed;
					float step = 90 / 0.5f * Time.deltaTime * 2f;
					role.transform.RotateAround(role.transform.position, axis, step);
					rotated += step; 
				}
                else //运动完成
                {
					isMoveAction = false;
					//结束
					moveActionProcess = 0;
					role.transform.RotateAround(role.transform.position, axis, 90 - rotated);
					role.transform.position = endpos;

				}
				return;
            }


			//初始化
			
			switch (nextMoveState)
            {
                case MoveState.Straight:
					endpos = new Vector3(role.transform.position.x, role.transform.position.y, role.transform.position.z + 1f);
					axis = Vector3.right;
					break;
                case MoveState.Left:
					endpos = new Vector3(role.transform.position.x - 1f, role.transform.position.y, role.transform.position.z);
					axis = Vector3.forward;
					break;
                case MoveState.Right:
					endpos = new Vector3(role.transform.position.x + 1f, role.transform.position.y, role.transform.position.z);
					axis = Vector3.back;
					break;
                default:
                    break;
            }
			rotated = 0.0f;//设置旋转角度
			isMoveAction = true;
			nextMoveState = MoveState.Straight; //重置

		}
	}
}
