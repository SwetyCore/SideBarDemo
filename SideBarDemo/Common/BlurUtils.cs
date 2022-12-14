using DockBar.Helper.Win32.Native;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DockBar.Common
{
    internal class BlurUtils
    {



        public static void EnableBlur(IntPtr hwind)
        {
            try
            {
                AccentPolicy structure = default(AccentPolicy);
                int num = Marshal.SizeOf(structure);
                structure.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
                IntPtr intPtr = Marshal.AllocHGlobal(num);
                Marshal.StructureToPtr(structure, intPtr, fDeleteOld: false);
                WindowCompositionAttributeData windowCompositionAttributeData = default(WindowCompositionAttributeData);
                windowCompositionAttributeData.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
                windowCompositionAttributeData.SizeOfData = num;
                windowCompositionAttributeData.Data = intPtr;
                WindowCompositionAttributeData data = windowCompositionAttributeData;
                User32.SetWindowCompositionAttribute(hwind, ref data);
                Marshal.FreeHGlobal(intPtr);
            }
            catch (Exception)
            {
            }
        }

        public static bool EnableDropShadow(IntPtr hwind, Margins margins)
        {
            try
            {
                int attrValue = 2;
                if (DWMAPI.DwmSetWindowAttribute(hwind, 2, ref attrValue, 4) == 0)
                {
                    Margins pMarInset = new Margins
                    {
                        Bottom = 0,
                        Left = 0,
                        Right = 0,
                        Top = 0
                    };
                    int num = DWMAPI.DwmExtendFrameIntoClientArea(hwind, ref pMarInset);
                    return num == 0;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class WindowAccentCompositor
    {
        private readonly Window _window;

        /// <summary>
        /// 创建 <see cref="WindowAccentCompositor"/> 的一个新实例。
        /// </summary>
        /// <param name="window">要创建模糊特效的窗口实例。</param>
        public WindowAccentCompositor(Window window) => _window = window ?? throw new ArgumentNullException(nameof(window));

        public void Composite(Color color)
        {
            Window window = _window;
            var handle = new WindowInteropHelper(window).EnsureHandle();

            var gradientColor =
                // 组装红色分量。
                color.R << 0 |
                // 组装绿色分量。
                color.G << 8 |
                // 组装蓝色分量。
                color.B << 16 |
                // 组装透明分量。
                color.A << 24;

            Composite(handle, gradientColor);
        }

        private void Composite(IntPtr handle, int color)
        {
            // 创建 AccentPolicy 对象。
            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
                GradientColor = 0,
            };

            // 将托管结构转换为非托管对象。
            var accentPolicySize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentPolicySize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            // 设置窗口组合特性。
            try
            {
                // 设置模糊特效。
                var data = new WindowCompositionAttributeData
                {
                    Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                    SizeOfData = accentPolicySize,
                    Data = accentPtr,
                };
                SetWindowCompositionAttribute(handle, ref data);
            }
            finally
            {
                // 释放非托管对象。
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        private enum WindowCompositionAttribute
        {
            // 省略其他未使用的字段
            WCA_ACCENT_POLICY = 19,
            // 省略其他未使用的字段
        }
    }
    public static class WindowAttribute
    {

        #region 圆角支持相关

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,

            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            //DWMWA_MICA_EFFECT = 1029
            DWMWA_SYSTEMBACKDROP_TYPE = 38
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }
        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern long DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);

        public static void EnableRoundWindow(IntPtr hWnd)
        {
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
        }


        #endregion


    }
}
