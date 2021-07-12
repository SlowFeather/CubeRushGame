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
    public class CameraFollow: MonoBehaviour
    {
		Vector3 offset;
		public Transform player;
		bool canStart = false;

		public void InitCameraFollow(Transform p)
        {
			Log.Print("进来了InitCameraFollow");
			player = p;
			offset = player.position - transform.position;
			canStart = true;
		}

		void Update()
		{
			if (canStart)
			{
				if (player != null)
					transform.position = Vector3.Lerp(transform.position, player.position - offset, Time.deltaTime * 5);
			}
			
		}
	}
}
