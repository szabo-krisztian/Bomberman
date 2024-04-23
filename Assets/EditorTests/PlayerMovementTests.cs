using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class PlayerMovementTests
{
    [Test]
    public void TestPlayerMovesUpWhenWKeyPressed()
    {
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();

        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.W));
        InputSystem.Update();
        
        Assert.IsTrue(keyboard.wKey.isPressed);   
    }
}