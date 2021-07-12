using JEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HotUpdateScripts
{
    public class AutoBindDemo:MonoBehaviour
    {
        public int IntField1;

        public string StringField1;

        public bool BoolField1;

        public EventSystem EventSystemField1;

        public GameObject GameObjectField1;

        public void Start()
        {
            Log.Print("Hello Test");

        }
        //public override void Init()
        //{
        //    base.Init();
            
        //    JResource.LoadResAsync<GameObject>("Test/Cube", (obj) =>
        //    {
        //        Log.Print("Get Resource with Async method: " + obj.name);
        //    });
        //}
    }
}
