using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blurlib.Input
{
    public class InputsManager
    {
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        public InputsManager()
        {
            _previousKeyboardState = default(KeyboardState);
            _currentKeyboardState = default(KeyboardState);
        }

        public void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        public bool IsDown(Keys key)
        {
            return _currentKeyboardState[key] == KeyState.Down;
        }

        public bool IsUp(Keys key)
        {
            return _currentKeyboardState[key] == KeyState.Up;
        }

        public bool IsPressed(Keys key)
        {
            return _currentKeyboardState[key] == KeyState.Down && _previousKeyboardState[key] == KeyState.Up;
        }

        public bool IsReleased(Keys key)
        {
            return _currentKeyboardState[key] == KeyState.Up && _previousKeyboardState[key] == KeyState.Down;
        }
    }
}
