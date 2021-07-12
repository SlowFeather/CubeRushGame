using JEngine.Core;
using System;
using System.Collections.Generic;
namespace HotUpdateScripts
{
    
    //模块管理器也是个模块，他就是服务各个模块的总管理器，所以他继承自继承服务基类，提供所有模块管理的服务
    public class ModuleManager : ServiceModule<ModuleManager>
    {

        #region 对已创建模块 和未创建模块的事件预监听以及未创建模块消息缓存的  容器字段实例化
        /// <summary>
        /// 已创建的模块列表
        /// </summary>
        private Dictionary<string, BusinessModule> m_BusinessModules;
        

        /// <summary>
        /// 当目标模块未创建时，预监听的事件
        /// 比如我需要监听这个模块的事件，但是这个模块可能还没有创建出来
        /// 就可以先托管给ModuleManager来处理m_PreListenEvents，等他异步加载好真正模块后再替换给我正常的事件表
        /// </summary>
        private Dictionary<string, EventTable> m_PreListenEvents;
        class MessageObject
        {
            public string target;
            public string msg;
            public object[] args;
        }
        /// <summary>
        /// 当目标模块未创建时，缓存的消息对象
        /// 比如我需要监听这个模块的事件，但是这个模块可能还没有创建出来
        /// 就可以先托管给ModuleManager来将这个消息先缓存起来
        /// </summary>
        private Dictionary<string, List<MessageObject>> m_CacheMessages;
        /// <summary>
        /// 模块域名， 用于模块反射的域   
        /// 【因为传进来不是类的全名，需要加上这个类所在的文件夹和命名空间作为前缀，也就是域名】
        /// </summary>
        private string m_ModuleDomain;


        public ModuleManager()
        {
            m_BusinessModules = new Dictionary<string, BusinessModule>();
            m_CacheMessages = new Dictionary<string, List<MessageObject>>();
            m_PreListenEvents = new Dictionary<string, EventTable>();
          
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="domain">业务模块所在的域</param>
        public void Init(string domain = "")
        {
            CheckSingleton();//先检查是否是单例
            m_ModuleDomain = domain;
        }
        #endregion

        #region 模块管理器提供 根据子类业务类型创建业务模块 功能
        
        /// <summary>
        /// 通过类型创建一个业务模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        private T CreateModule<T>(object args = null) where T : BusinessModule
        {
            return (T)CreateModule(typeof(T).Name, args);
        }

        /// <summary>
        /// 通过名字创建一个业务模块
        /// 先通过名字反射出Class，如果不存在
        /// 则通过扫描Lua文件目录加载LuaModule
        /// </summary>
        /// <param name="name">业务模块的名字</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public BusinessModule CreateModule(string name, object args = null)
        {
            Log.Print("CreateModule() name = " + name + ", args = " + args);

            if (m_BusinessModules.ContainsKey(name))
            {
                Log.PrintError("CreateModule() The Module<{0}> Has Existed!");
                return null;
            }

            BusinessModule module = null;
            //补全类名
            Type type = Type.GetType(m_ModuleDomain + "." + name);
            if (type != null)
            {
                module = Activator.CreateInstance(type) as BusinessModule;
            }
            else
            {
                //module = new LuaModule(name);
            }
            m_BusinessModules.Add(name, module);

            //如果有预监听的事件需要处理
            if (m_PreListenEvents.ContainsKey(name))
            {
                EventTable mgrEvent = null;
                //出去并移除预监听
                mgrEvent = m_PreListenEvents[name];
                m_PreListenEvents.Remove(name);

                module.SetEventTable(mgrEvent);
            }

            module.Create(args);

            //如果有缓存的消息需要处理
            if (m_CacheMessages.ContainsKey(name))
            {
                List<MessageObject> list = m_CacheMessages[name];
                for (int i = 0; i < list.Count; i++)
                {
                    MessageObject msgobj = list[i];
                    module.HandleMessage(msgobj.msg, msgobj.args);
                }
                m_CacheMessages.Remove(name);
            }

            return module;
        }
        #endregion

        #region 模块管理器提供 释放由ModuleManager创建的模块  功能
        
        /// <summary>
        /// 释放一个由ModuleManager创建的模块
        /// 遵守谁创建谁释放的原则
        /// </summary>
        /// <param name="module"></param>
        public void ReleaseModule(BusinessModule module)
        {
            if (module != null)
            {
                if (m_BusinessModules.ContainsKey(module.Name))
                {
                    Log.Print("ReleaseModule() name = " + module.Name);
                    m_BusinessModules.Remove(module.Name);
                    module.Release();
                }
                else
                {
                    Log.PrintError("ReleaseModule() 模块不是由ModuleManager创建的！ name = " + module.Name);
                }
            }
            else
            {
                Log.PrintError("ReleaseModule() module = null!");
            }

        }

        /// <summary>
        /// 释放所有模块
        /// </summary>
        public void ReleaseAll()
        {
            foreach (var @event in m_PreListenEvents)
            {
                @event.Value.Clear();
            }
            m_PreListenEvents.Clear();

            m_CacheMessages.Clear();

            foreach (var module in m_BusinessModules)
            {
                module.Value.Release();
            }
            m_BusinessModules.Clear();
        }
        #endregion

        #region 模块管理器提供  获取一个模块Module ,如果未创建过该Module，则返回null  功能
        public T GetModule<T>() where T : BusinessModule
        {
            return GetModule(typeof(T).Name) as T;
        }
        /// <summary>
        /// 通过名字获取一个Module
        /// 如果未创建过该Module，则返回null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BusinessModule GetModule(string name)
        {
            if (m_BusinessModules.ContainsKey(name))
            {
                return m_BusinessModules[name];
            }
            return null;
        }
        #endregion
       
        /// <summary>
        /// 显示业务模块的默认UI
        /// </summary>
        /// <param name="name"></param>
        public void ShowModule(string name, object arg = null)
        {
			SendMessage(name, "Show", arg);
        }

        #region 模块管理器提供  模块之间通讯的消息机制  功能
        //当A模块要给B模块发送消息，A模块并不知道B模块是否存在，也不知道B模块的实例在哪里，
        //但是通过模块管理器来给B模块发送消息

        /// <summary>
        /// 对外提供 向指定的模块发送消息  功能
        /// </summary>
        /// <param name="target"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void SendMessage(string target, string msg, params object[] args)
        {
            SendMessage_Internal(target, msg, args);
        }


        private void SendMessage_Internal(string target, string msg, object[] args)
        {
            //先获取模块，如果获取得到就处理消息
            BusinessModule module = GetModule(target);
            if (module != null)
            {
                module.HandleMessage(msg, args);
            }
            else
            {
                // 如果没有获取到指定消息的模块，先将消息缓存起来
                List<MessageObject> list = GetCacheMessageList(target);
                MessageObject obj = new MessageObject();
                obj.target = target;
                obj.msg = msg;
                obj.args = args;
                list.Add(obj);

                Log.PrintWarning("SendMessage() target不存在！将消息缓存起来! target:"+ target+", msg:"+ msg+", args:"+ args);
            }
        }

        private List<MessageObject> GetCacheMessageList(string target)
        {
            List<MessageObject> list = null;
            //如果消息列表库中没有这条消息，就创建一条消息列表存到库中，有的话直接返回该列表
            if (!m_CacheMessages.ContainsKey(target))
            {
                list = new List<MessageObject>();
                m_CacheMessages.Add(target, list);
            }
            else
            {
                list = m_CacheMessages[target];
            }
            return list;
        }
        #endregion

        #region 模块管理器提供  模块之间通讯的事件监听机制  功能
        //当A模块要监听B模块的某些事件，A模块并不知道B模块是否存在，也不知道B模块的实例在哪里，
        //但是通过模块管理器来监听B模块的事件
        
        /// <summary>
        /// 监听指定模块的指定事件
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ModuleEvent Event(string target, string type)
        {
            ModuleEvent evt = null;
            BusinessModule module = GetModule(target);
            if (module != null)
            {
                evt = module.Event(type);
            }
            else
            {
                //预创建事件
                EventTable table = GetPreEventTable(target);
                evt = table.GetEvent(type);
                Log.PrintWarning("Event() target不存在！将预监听事件! target:"+ target+", event:"+ type);
            }
            return evt;
        }

        private EventTable GetPreEventTable(string target)
        {
            EventTable table = null;
            if (!m_PreListenEvents.ContainsKey(target))
            {
                table = new EventTable();
                m_PreListenEvents.Add(target, table);
            }
            else
            {
                table = m_PreListenEvents[target];
            }
            return table;
        }
        #endregion

    }
}
