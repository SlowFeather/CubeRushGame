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




        public override void Init()
        {
            base.Init();
        }
		public override void Run()
		{
			base.Run();
		}

        public override void Loop()
        {
            base.Loop();
        }
    }
}
