using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace ArtifactSelector.Scanner
{
    internal class Navigation
    {

        private RECT WindowSize;
        private RECT WindowPosition;
        private Size AspectRatio;
        private InputSimulator sim = new InputSimulator();
        private POINT cursorPos = new POINT();
        private POINT savedPos = new POINT();

        public VirtualKeyCode escapeKey = VirtualKeyCode.ESCAPE;
        public VirtualKeyCode characterKey = VirtualKeyCode.VK_C;
        public VirtualKeyCode inventoryKey = VirtualKeyCode.VK_B;
        public VirtualKeyCode slotOneKey = VirtualKeyCode.VK_1;

        public Navigation()
        {
            Initialize();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClientRect(IntPtr hWnd, ref RECT Rect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ClientToScreen(IntPtr hWnd, ref RECT Rect);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public bool SetCursor(int X, int Y)
        {
            POINT point;
            GetCursorPos(out point);
            int currentX = point.X - GetPosition().Left;
            int currentY = point.Y - GetPosition().Top;
            if (currentX != cursorPos.X || currentY != cursorPos.Y)
            {
                throw new SelectorException(ErrorMessages.MouseMoved);
            }

            cursorPos.X = X;
            cursorPos.Y = Y;
            return SetCursorPos(GetPosition().Left + X, GetPosition().Top + Y);
        }

        public void Click()
        {
            sim.Mouse.LeftButtonClick();
        }

        public void Scroll(int amount)
        {
            int direction = 1;

            if (amount < 0)
            {
                direction = -1;
                amount *= -1;
            }

            for (int i = 0; i < amount; i++)
            {
                sim.Mouse.VerticalScroll(direction);
            }
        }

        public void SavePosition()
        {
            savedPos.X = cursorPos.X;
            savedPos.Y = cursorPos.Y;
        }

        public bool MoveToSavedPosition()
        {
            return SetCursor(savedPos.X, savedPos.Y);
        }

        public bool MoveToLockButton()
        {
            int buttonX = ScaleToActualX(Dimension.LOCK_X);
            int buttonY = ScaleToActualY(Dimension.LOCK_Y);
            return SetCursor(buttonX, buttonY);
        }

        public bool MoveToFirstArtifact()
        {
            int buttonX = ScaleToActualX(Dimension.FIRST_ARTI_X + (Dimension.ARTI_BUTTON_WIDTH / 2));
            int buttonY = ScaleToActualY(Dimension.FIRST_ARTI_Y + (Dimension.ARTI_BUTTON_HEIGHT / 2));
            return SetCursor(buttonX, buttonY);
        }

        public bool MoveToLeftMostArtifact()
        {
            int buttonX = ScaleToActualX(Dimension.FIRST_ARTI_X + (Dimension.ARTI_BUTTON_WIDTH / 2));
            int buttonY = cursorPos.Y;
            return SetCursor(buttonX, buttonY);
        }

        public bool MoveToRightArtifact()
        {
            int buttonX = cursorPos.X + ScaleToActualX(Dimension.ARTI_BUTTON_WIDTH + (Dimension.ARTI_BUTTON_GAP / 2));
            int buttonY = cursorPos.Y;
            return SetCursor(buttonX, buttonY);
        }

        public bool MoveToBottomArtifact()
        {
            int buttonX = cursorPos.X;
            int buttonY = cursorPos.Y + ScaleToActualY(Dimension.ARTI_BUTTON_HEIGHT + (Dimension.ARTI_BUTTON_GAP / 2));
            return SetCursor(buttonX, buttonY);
        }

        public bool MoveCenterToDeselect()
        {
            int buttonX = cursorPos.X + ScaleToActualX(Dimension.RELATIVE_DESELECT_X);
            int buttonY = cursorPos.Y + ScaleToActualY(Dimension.RELATIVE_DESELECT_Y);
            return SetCursor(buttonX, buttonY);
        }

        public bool MoveDeselectToCenter()
        {
            int buttonX = cursorPos.X - ScaleToActualX(Dimension.RELATIVE_DESELECT_X);
            int buttonY = cursorPos.Y - ScaleToActualY(Dimension.RELATIVE_DESELECT_Y);
            return SetCursor(buttonX, buttonY);
        }

        public int ScaleToActualX(double x)
        {
            return (int)Dimension.ScaleX(x, GetWidth());
        }

        public int ScaleToActualY(double y)
        {
            return (int)Dimension.ScaleY(y, GetHeight());
        }

        public RECT ScaleToActualRect(RECT rect)
        {
            return Dimension.ScaleRect(rect, GetWidth(), GetHeight());
        }

        public void Wait(int ms = 1000)
        {
            Thread.Sleep(ms);
        }

        public RECT GetPosition()
        {
            return WindowPosition;
        }

        public int GetWidth()
        {
            return WindowSize.Width;
        }

        public int GetHeight()
        {
            return WindowSize.Height;
        }

        public Size GetAspectRatio()
        {
            if (!AspectRatio.IsEmpty)
            {
                return AspectRatio;
            }

            if (WindowSize.Width == 0)
            {
                throw new SelectorException(ErrorMessages.GameWidthZero);
            }

            if (WindowSize.Height == 0)
            {
                throw new SelectorException(ErrorMessages.GameHeightZero);
            }

            int x = WindowSize.Width / GreatestCommonDivisor(WindowSize.Width, WindowSize.Height);
            int y = WindowSize.Height / GreatestCommonDivisor(WindowSize.Width, WindowSize.Height);
            var size = new Size(x, y);


            return size;
        }

        private int GreatestCommonDivisor(int a, int b)
        {
            int r;
            while (b != 0)
            {
                r = a % b;
                a = b;
                b = r;
            }
            return a;
        }

        public Bitmap CaptureRegion(RECT region, PixelFormat format = PixelFormat.Format24bppRgb)
        {
            Bitmap bmp = new Bitmap(region.Width, region.Height, format);
            using (Graphics gfxBmp = Graphics.FromImage(bmp))
            {
                gfxBmp.CopyFromScreen(GetPosition().Left + region.Left, GetPosition().Top + region.Top, 0, 0, bmp.Size);
            }
            return bmp;
        }


        public void Initialize()
        {
            string[] executables = new string[] { "YuanShen", "GenshinImpact" };
            foreach (var processName in executables)
            {
                if (InitializeProcess(processName, out IntPtr handle))
                {
                    // Get area and position
                    ClientToScreen(handle, ref WindowPosition);
                    Wait();
                    GetClientRect(handle, ref WindowSize);

                    try
                    {
                        AspectRatio = GetAspectRatio();
                    }
                    catch (DivideByZeroException)
                    {
                        throw new SelectorException(ErrorMessages.GameNotFocused);
                    }

                    Logger.Notice(NoticeMessages.FoundExe, processName);
                    Logger.Notice(NoticeMessages.WindowLocation, WindowSize.Width, WindowSize.Height, WindowPosition.Left, WindowPosition.Top);
                    SetCursorPos(GetPosition().Left, GetPosition().Top);
                    return;
                }
                Logger.Notice(NoticeMessages.ProcessNotFound, processName);
            }
            throw new SelectorException(ErrorMessages.GameNotFound);
        }

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        private struct WindowPlacement
        {
            public int length;
            public int flags;
            public ShowWindowEnum showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };


        private bool InitializeProcess(string processName, out IntPtr handle)
        {
            handle = IntPtr.Zero;
            // Get process
            using (Process genshin = Process.GetProcessesByName(processName).FirstOrDefault())
            {
                // check if the process is running
                if (genshin != null)
                {
                    handle = genshin.MainWindowHandle;

                    var windowPlacement = new WindowPlacement { length = Marshal.SizeOf(typeof(WindowPlacement)) };
                    GetWindowPlacement(handle, ref windowPlacement);

                    // Check if minimized
                    if (windowPlacement.showCmd == ShowWindowEnum.ShowMinimized)
                    {
                        windowPlacement.showCmd = ShowWindowEnum.ShowNormal;
                        SetWindowPlacement(handle, ref windowPlacement);
                    }

                    // Bring game to front
                    SetForegroundWindow(handle);
                    return true;
                }
            }
            return false;
        }
    }
}
