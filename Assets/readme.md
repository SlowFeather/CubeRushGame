# 启动相关
## 生成解决方案DLL跳过
方法一：菜单栏 -> 工具 -> 配置管理器 生成的对勾 重新勾一下

方法二：删除Assets/HotUpdateResources/Dlls/Hidden~文件夹，然后再进一下Unity，会自动重新生成文件夹，然后再生成解决方案

常见问题详见 https://xgamedev.uoyou.com/guide-v0-6.html
# 打包相关
## 打AB相关
CryptoWindow.cs中可以手动改key(line 44)不用每次打ab都填写
```CSharp
private string Key ="1234567891011121";
```
这里的key需要和Init场景中InitJEngine物体上的Key一样
需要打包的资源不要到处乱放，按照文件夹归类
## Updater设置

Init场景中Updater物体上Development真机一定记得关闭

检查BaseURL和GameScene，后续需要添加修改BaseURL的问题，不加也可以，用域名即可
## Android
暂无
## IOS
需要根据项目内容修改Assets/link.xml内容，否则会发生代码裁剪出现```Could not produce class with ID XXX```的错误

