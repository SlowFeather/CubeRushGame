//
// CryptoWindow.cs
//
// Author:
//       JasonXuDeveloper（傑） <jasonxudeveloper@gmail.com>
//
// Copyright (c) 2020 JEngine
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using UnityEditor;
using UnityEngine;

namespace JEngine.Editor
{
    internal class CryptoWindow : EditorWindow
    {
        private static CryptoWindow window;
        public static void ShowWindow()
        {
            window = GetWindow<CryptoWindow>();
            window.titleContent = new GUIContent("Encrypt DLL");
            window.maxSize = new Vector2(300,100);
            window.Show();
        }

        private string Key ="1234567891011121";

        public static Action<string> Build;
        private void OnGUI()
        {
            GUILayout.Space(10);
            //文本
            Key = EditorGUILayout.TextField("Encrypt Key (加密密码)", Key);
            GUILayout.Space(30);
            if (GUILayout.Button("Build Bundles (打包资源)"))
            {
                if (Key == null)
                {
                    EditorUtility.DisplayDialog("Encrypt Failed! (加密失败)",
                        "Please make sure the key has 16 letters! \n请确保加密密码有16位!", "OK");
                    return;
                }
                if (Key.Length == 16)
                {
                    if (Build == null)
                    {
                        EditorUtility.DisplayDialog("Request Timeout (请求超时)",
                            "Please try again! \n请再次尝试!", "OK");
                    }
                    else
                    {
                        Build(Key);
                    }
                    window.Close();
                }
                else
                {
                    EditorUtility.DisplayDialog("Encrypt Failed! (加密失败)",
                        "Please make sure the key has 16 letters! \n请确保加密密码有16位!", "OK");
                }
            }
        }
    }
}