namespace HotUpdateScripts
{

    /// <summary>
    /// 全局事件
    /// 有些事件不确定应该是由谁发出
    /// 就可以通过全局事件来收和发
    ///  GlobalEvent.onLogin.Invoke(true);//全局事件的发布事件 测试
    /// GlobalEvent.onLogin.AddListener((bool args) => { }); //全局事件的监听事件  测试
    /// </summary>
    public static class GlobalEvent
    {
        /// <summary>
        /// true:登录成功，false：登录失败，或者掉线
        /// </summary>
        public static ModuleEvent<bool> onLogin = new ModuleEvent<bool>();
        /// <summary>
        /// true:支付成功，false：支付失败，或者支付异常
        /// </summary>
        public static ModuleEvent<bool> onPay= new ModuleEvent<bool>();

    
    }


}
