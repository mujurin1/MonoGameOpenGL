using Microsoft.Xna.Framework.Input;

namespace MonoGameEx
{
#pragma warning disable IDE1006 // 命名スタイル I
    public interface KeyboardPress
#pragma warning restore IDE1006 // 命名スタイル I
    {
        KeyboardState CurrentKeyState { get; set; }
        KeyboardState PreviousKeyState { get; set; }
    }

    public static class KeyboardPressImpl
    {
        public static void SetKeyState(this KeyboardPress _this)
        {
            _this.PreviousKeyState = _this.CurrentKeyState;
            _this.CurrentKeyState = Keyboard.GetState();
        }

        public static bool IsKeyDown(this KeyboardPress _this, Keys key)
            => _this.CurrentKeyState.IsKeyDown(key);
        public static bool IsKeyPress(this KeyboardPress _this, Keys key)
            => _this.CurrentKeyState.IsKeyDown(key) && _this.PreviousKeyState.IsKeyUp(key);

    }
}
