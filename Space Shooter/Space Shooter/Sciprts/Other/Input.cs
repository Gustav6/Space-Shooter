using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    public class Input
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;
        static MouseState currentMouseState;
        static MouseState previousMouseState;

        public static KeyboardState GetState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool HasBeenPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static MouseState GetMouseState()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            return currentMouseState;
        }
        /*
        public static bool MouseIsPressed(Keys key)
        {
            return currentMouseState.IsKeyDown(key);
        }

        public static bool MouseHasBeenPressed(Keys key)
        {
            return currentMouseState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }*/

    }
}
