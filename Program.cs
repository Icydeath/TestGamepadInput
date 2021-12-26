using System;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpDX.DirectInput;
using System.Collections.Generic;
using System.Timers;

namespace InputTest
{
  class Program
  {
    static Dictionary<string, bool> ButtonsPressed = new();
    static string firstButton = string.Empty;
    static string secondButton = string.Empty;
    static string combo = string.Empty;

    static void Main(string[] args)
    {
      var gp = new Gamepad();
      Console.WriteLine("HID: {0}\n\tInstanceName: {1}\n\tProductGuid: {2}\n\tProductName: {3}\n\tDeviceType: {4}\n\tSubType: {5}",
        gp.gamepadInfo.InstanceGuid,
        gp.gamepadInfo.InstanceName,
        gp.gamepadInfo.ProductGuid,
        gp.gamepadInfo.ProductName,
        gp.gamepadInfo.DeviceType,
        gp.gamepadInfo.SubType);

      gp.evNewGamePadButtonInfoAcquired += Gp_evNewGamePadButtonInfoAcquired;

      Console.WriteLine("Press the button combination you want to save...");
      while(string.IsNullOrEmpty(combo)) 
      { 
      }

      Console.WriteLine("Buttons pressed = {0}", combo);
      
      gp.Release(); // releases the gamepad resources

      Console.ReadKey();
    }

    private static void Gp_evNewGamePadButtonInfoAcquired(object sender, string btn, bool state)
    {
      //Console.WriteLine("{0}: {1}", btn, state);
      if (string.IsNullOrEmpty(combo))
      {
        if (state && firstButton == string.Empty)
        {
          firstButton = btn;
        }
        else if (!state && btn == firstButton && string.IsNullOrEmpty(secondButton))
        {
          combo = firstButton;
        }
        else if (state && btn != firstButton)
        {
          secondButton = btn;
          combo = $"{firstButton} + {secondButton}";
        }
      }
    }

    #region TESTING MULTIPLE JOYSTICKS - This code DOES work... but idk how many people use more than 1 gamepad...
    //static Timer timer;
    //static List<Joystick> Joysticks = new();
    //const int TIMER_INTERVAL_IN_MS = 10;

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="buttonNo"></param>
    ///// <param name="pressed">
    ///// pressed = 1
    ///// unpressed = 0;
    ///// </param>
    //public delegate void newGamePadButtonInfoAcquiredEventHandler(object sender, int buttonNo, bool pressed);
    //public event newGamePadButtonInfoAcquiredEventHandler evNewGamePadButtonInfoAcquired;
    //static volatile bool GamePadBlocker = false;

    //static void TestMultipleJoysticks()
    //{
    //  DirectInput directInput = new DirectInput();
    //  var joystickGuids = new List<Guid>();

    //  // Find Joystick/Gamepad Guids
    //  foreach (var deviceInstance in directInput.GetDevices().Where(x => x.Type == DeviceType.Gamepad || x.Type == DeviceType.Joystick))
    //  {
    //    joystickGuids.Add(deviceInstance.InstanceGuid);
    //  }

    //  // If Joystick not found, throws an error
    //  if (joystickGuids.Count == 0)
    //  {
    //    Console.WriteLine("No joystick/Gamepad found.");
    //    Environment.Exit(1);
    //  }


    //  foreach (var joystickGuid in joystickGuids)
    //  {
    //    // Instantiate the joystick
    //    var joystick = new Joystick(directInput, joystickGuid);
    //    Joysticks.Add(joystick);

    //    // print device info
    //    var info = joystick.Information;
    //    Console.WriteLine("HID: {0}", info.InstanceGuid);
    //    Console.WriteLine("\tInstanceName: {0}", info.InstanceName);
    //    //Console.WriteLine("\tProductGuid: {0}", info.ProductGuid);
    //    //Console.WriteLine("\tProductName: {0}", info.ProductName);
    //    Console.WriteLine("\tDeviceType: {0}", info.Type.ToString());
    //    Console.WriteLine("\tSubtype: {0}", info.Subtype);

    //    // Query all suported ForceFeedback effects
    //    var allEffects = joystick.GetEffects();
    //    foreach (var effectInfo in allEffects)
    //      Console.WriteLine("Effect available {0}", effectInfo.Name);

    //    // Set BufferSize in order to use buffered data.
    //    joystick.Properties.BufferSize = 128;

    //    // Acquire the joystick
    //    joystick.Acquire();
    //  }

    //  #region 
    //  // Poll events from joystick
    //  //while (true)
    //  //{
    //  //  foreach(var joystick in Joysticks)
    //  //  {
    //  //    joystick.Poll();
    //  //    var datas = joystick.GetBufferedData();
    //  //    foreach (var state in datas)
    //  //    {
    //  //      if (state.Offset != JoystickOffset.RotationX &&
    //  //        state.Offset != JoystickOffset.RotationY &&
    //  //        state.Offset != JoystickOffset.RotationZ &&
    //  //        state.Offset != JoystickOffset.X &&
    //  //        state.Offset != JoystickOffset.Y &&
    //  //        state.Offset != JoystickOffset.Z)
    //  //        Console.WriteLine(state);
    //  //    }
    //  //  }
    //  //}
    //  #endregion

    //  timer = new Timer
    //  {
    //    Interval = TIMER_INTERVAL_IN_MS
    //  };
    //  timer.Elapsed += Timer_Elapsed;
    //  timer.Start();
    //}

    //private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
    //{
    //  foreach (var joystick in Joysticks)
    //  {
    //    joystick.Poll();
    //    var datas = joystick.GetBufferedData();
    //    foreach (var state in datas)
    //    {
    //      if (((int)state.Offset) >= 48 && ((int)state.Offset) <= 175) // buttons 0 through 127
    //      {
    //        Console.WriteLine(state);
    //      }

    //      //if (state.Offset != JoystickOffset.RotationX && state.Offset != JoystickOffset.RotationY && state.Offset != JoystickOffset.RotationZ &&
    //      //  state.Offset != JoystickOffset.X && state.Offset != JoystickOffset.Y && state.Offset != JoystickOffset.Z)
    //      //{
    //      //  Console.WriteLine(state);
    //      //}
    //    }
    //  }
    //}
    #endregion
  }


}
