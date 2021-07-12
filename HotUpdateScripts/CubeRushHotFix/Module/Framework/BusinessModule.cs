using JEngine.Core;
using System.Reflection;
namespace HotUpdateScripts
{
    /// <summary>
    /// 所有业务模块的基类
    /// </summary>
    public abstract class BusinessModule : Module
    {
        #region 业务模块基类给子类提供常用的字段和属性
        
        //具体子类业务模块的类名，根据业务类名通过事件机制和消息机制通讯
        private string m_Name = null;

        public string Name
        {
            get
            {
                if (m_Name == null)
                {
                    m_Name = this.GetType().Name;
                }
                return m_Name;
            }
        }
        /// <summary>
        /// 比如在Loading一个模块或者资源时，要显示一下这个模块的标题
        /// </summary>
        public string Title;
        #endregion

        //---------------------------------------------------------------

        #region 业务模块基类给子类提供的构造和事件机制的使用
            
        public BusinessModule()
        {
            //默认构造函数
        }
        //不想被外部Assembly调用他，只想让子业务模块作为派生类来使用,只要将子类的名字传进来就行
        //上面的m_Name确实可以直接根据业务模块作为子类的类名得到，但是LuaModule都是在Lua里面写的，这时就需要手动传递来决定他的类名
        //protected internal 表示当前程序集Assembly或者其子类可以访问。注意是 protected 和 internal 是或者的关系
        internal BusinessModule(string name)
        {
            m_Name = name;
        }
        
        private EventTable m_EventTable;

        /// <summary>
        /// 实现抽象事件功能
        /// 可以像这样使用：obj.Event("onLogin").AddListener(...)        
        /// 事件的发送方法：this.Event("onLogin").Invoke(args)
        /// 而不需要在编码时先定义好，以提高模块的抽象程度
        /// 但是在模块内部的类不应该过于抽象，比如数据发生更新了，
        /// 在UI类这样使用：obj.onUpdate.AddListener(...)
        /// 这两种方法在使用形式上，保持了一致性！
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ModuleEvent Event(string type)
        {
            //m_EventTable = new EventTable();
            //return m_tblEvent.GetEvent(type);
            //因为有些业务模块不需要派发事件的，所以这里写成懒加载模式，
            //用的时候在实例化m_EventTable,这样可以优化减少不必要的内存开销
            return GetEventTable().GetEvent(type);
        }

        internal void SetEventTable(EventTable mgrEvent)
        {
            m_EventTable = mgrEvent;
        }

        protected EventTable GetEventTable()
        {
            if (m_EventTable == null)
            {
                m_EventTable = new EventTable();
            }
            return m_EventTable;
        }
        #endregion

        //-------------------------------------------------------------

        #region 业务模块基类给子类提供创建模块和释放模块功能
        /// <summary>
        /// 调用它以创建模块
        /// </summary>
        /// <param name="args"></param>
        public virtual void Create(object args = null)
        {
            Log.Print("Create() args = " + args);

        }
        
        /// <summary>
        /// 调用它以释放模块
        /// </summary>
        public override void Release()
        {
            if (m_EventTable != null)
            {
                m_EventTable.Clear();
                m_EventTable = null;
            }

            base.Release();
        }
        #endregion

        //-------------------------------------------------------------
        //业务模块之间通讯 一个是 通过事件机制 另一个就是通过消息机制
        #region 业务模块基类给子类提供处理消息功能
            
        /// <summary>
        /// 当模块收到消息后，对消息进行一些处理
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        internal void HandleMessage(string msg, object[] args)
        {
            Log.Print("HandleMessage() msg:"+ msg+", args:"+ args);

            //通过C#语言方法的反射机制，根据方法名字 得到这个方法体，
            MethodInfo mi = this.GetType().GetMethod(msg, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            if (mi != null)
            {
                mi.Invoke(this, BindingFlags.NonPublic, null, args, null);
            }
            else
            {
                //如果无法根据名字找到这个函数，就是用通用的函数执行消息
                OnModuleMessage(msg, args);
            }
        }
        
        /// <summary>
        /// 由派生类去实现，用于处理消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        protected virtual void OnModuleMessage(string msg, object[] args)
        {
            Log.Print("OnModuleMessage() msg:" + msg + ", args:" + args);
        }

        #endregion

        //-----------------------------------------------------------------
        /// <summary>
        /// 显示业务模块的主UI
        /// 一般业务模块都有UI，这是游戏业务模块的特点
        /// </summary>
        protected virtual void Show(object arg)
        {
            Log.Print("Show() arg:"+arg);
        }
        
    }
}
