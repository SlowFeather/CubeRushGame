using JEngine.Core;
using System;
using UnityEngine;

namespace HotUpdateScripts
{
    public class Example_Module
    {
        public void Start()
        {
            //验证框架是否正常工作 
            //两个业务模块 TestBusinessModuleA和TestBusinessModuleB
            //一个服务模块 TestServiceModuleC
            //验证模块A B C之间的通讯和调用  通过OK

            //这里的域名指的是  当前类使用时的命名空间 【文件路径】
            ModuleManager.Instance.Init("HotUpdateScripts");

            ModuleManager.Instance.CreateModule("TestBusinessModuleA");
            ModuleManager.Instance.CreateModule("TestBusinessModuleB");

            TestServiceModuleC.Instance.Init();
        }
    }

    public class TestBusinessModuleA : BusinessModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);

            //业务层模块之间，通过Message进行通讯
            //当前的TestBusinessModuleA 通过模块管理器  获取到TestBusinessModuleB消息表中MessageFromA_1消息，给发消息1,2,3
            ModuleManager.Instance.SendMessage("TestBusinessModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("TestBusinessModuleB", "MessageFromA_2", "abc", 123);

            //业务层模块之间，通过Event进行通讯 
            //当前的TestBusinessModuleA 通过模块管理器 获取到TestBusinessModuleB事件表中onModuleEventB事件，来监听事件回调
            //ModuleManager.Instance.Event("TestBusinessModuleB", "onModuleEventB").AddListener(OnModuleEventB);

            //业务层调用服务层，通过事件监听回调
            //当前的TestBusinessModuleA 直接对TestServiceModuleC添加监听事件回调  和 直接调用对外逻辑
            TestServiceModuleC.Instance.onEvent+=OnModuleEventC;
            TestServiceModuleC.Instance.DoSomething();

            //全局事件
            //当前的TestBusinessModuleA 直接监听全局的登录事件回调
            //GlobalEvent.onLogin.AddListener(OnLogin);
        }

        private void OnModuleEventC(object args)
        {
            Log.Print("OnModuleEventC() args:"+ args);
        }

        private void OnModuleEventB(object args)
        {
            Log.Print("OnModuleEventB() args:"+ args);
        }

        private void OnLogin(bool args)
        {
            Log.Print("OnLogin() args:"+ args);
        }
    }

    public class TestBusinessModuleB : BusinessModule
    {
        public ModuleEvent onModuleEventB { get { return Event("onModuleEventB"); } }

        public override void Create(object args = null)
        {
            base.Create(args);
            //onModuleEventB.Invoke("aaaa");
        }

        protected void MessageFromA_2(string args1, int args2)
        {
            Log.Print("MessageFromA_2() args:"+ args1+","+args2);
        }

        protected override void OnModuleMessage(string msg, object[] args)
        {
            base.OnModuleMessage(msg, args);
            Log.Print("OnModuleMessage() msg:"+ msg+", args:"+ args[0]+","+ args[1]+","+ args[2]);
        }
    }

    public class TestServiceModuleC : ServiceModule<TestServiceModuleC>
    {
        //public ModuleEvent onEvent = new ModuleEvent();
        public Action<object> onEvent;

        public void Init()
        {
            Log.Print("TestServiceModuleC-Init");
        }

        public void DoSomething()
        {
            onEvent.Invoke(true);
            //onEvent.Invoke(true);
        }
    }


}
