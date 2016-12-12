//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
namespace SimpleMachines

{
	public class DelayMachine
	{
		//Helper class that adds a delay based on time, button hold, button press, or a combination

		//Member variables to help model Delay
		//-Time variables
		private float intervalStartTime;
		private float intervalLength;
		private int intervalCount;

		//-Input variables
		private bool allowPulse;
		private PulseInput myPulseInput;
		private int pulseCount;
		private int pulseLimit;

		public void ResetDelayMachine()
		{
			//Reset counters
			intervalCount = 0;
			CycleInterval ();
		}

		private void CycleInterval()
		{
			pulseCount = 0;
			intervalStartTime = Time.time;
		}

		public void StartTimer(float period)
		{
			allowPulse = false;
			intervalLength = period;
			ResetDelayMachine ();
		}

		public void StartTimerWithPulse(float period, PulseInput checkPulse, int pulsesPerInterval)
		{
			intervalLength = period;
			allowPulse = true;
			myPulseInput = checkPulse;
			pulseLimit = pulsesPerInterval;
			ResetDelayMachine ();
		}

		public int CheckDelayInterval()
		{
			//check time
			CheckIntervalTimeLimit ();
			//check pulse input
			if (allowPulse) {
				CheckPulseLimit();
			}
			return intervalCount;
		}

		private void CheckIntervalTimeLimit()
		{
			float currentTime = Time.time;
			float deltaTime = currentTime - intervalStartTime;
			if (deltaTime >= intervalLength) {
				intervalCount++;
				CycleInterval();
			}
		}

		private void CheckPulseLimit()
		{
			if (myPulseInput.CheckNewPulse ()) {
				pulseCount++;
			}
			if (pulseCount >= pulseLimit) {
				intervalCount++;
				CycleInterval();
			}
		}

	}

	public class PulseInput
	{
		//Helper class for inputs that must be released before registering again
		private KeyCode myKey;
		public bool waitingForPulse { get; private set; }
		private float lastPulsePressTime;
		public float pulseHeldSecs { get; private set; }

		public PulseInput (KeyCode setKey)
		{
			SetKey(setKey);
			pulseHeldSecs = 0f;
		}

		private void PulseInputInitialize ()
		{
			waitingForPulse = true;
			pulseHeldSecs = 0f;
		}

		public void SetKey(KeyCode setKey)
		{
			myKey = setKey;
		}

		public KeyCode GetKey()
		{
			return myKey;
		}

		public bool CheckNewPulse()
		{
			bool newPulse = false;
			if (waitingForPulse) 
			{
				newPulse = Input.GetKey (myKey);
				waitingForPulse = !newPulse;
				lastPulsePressTime = Time.time;
			} 
			else 
			{
				pulseHeldSecs = Time.time - lastPulsePressTime;
				newPulse = false;
				waitingForPulse = !Input.GetKey(myKey);
			}
			//Reset pulseHeldSecs
			if (waitingForPulse) {
				pulseHeldSecs = 0f;
			}

			return newPulse;
		}
	}

	public class ConstantInput
	{
		private KeyCode myKey;
		private bool pressing = false;
		private bool initialPress = false;

		public ConstantInput (KeyCode inKey)
		{
			SetKey(inKey);
		}

		public bool CheckInput()
		{
			if (Input.GetKey (myKey)) 
			{
				if (!pressing) 
				{
					initialPress = true;
				} 
				else 
				{
					initialPress = false;
				}
				pressing = true;
			} 
			else 
			{
				pressing = false;
				initialPress = false;
			}

			return pressing;
		}

		public bool CheckInitialPress()
		{
			return initialPress;
		}

		public void SetKey(KeyCode inKey)
		{
			myKey = inKey;
		}
	}

	public class Oscillator
	{
		//Oscillator members
		private bool amRunning = false;
		private float timeConstant;
		private float amplitude;
		private float currentTime;
		private float totalPeriods;

		//Initialize Oscillator

		public Oscillator(float inTimeConstant, float inAmplitude, float inTotalPeriods)
		{
			timeConstant = inTimeConstant;
			amplitude = inAmplitude;
			totalPeriods = inTotalPeriods;
			currentTime = 0f;
		}

		public void Reset()
		{
			currentTime = 0f;
			amRunning = true;
		}

		public void Reset(float mag)
		{
			amplitude = mag;
			currentTime = 0f;
			amRunning = true;
		}

		public float IntegrateOscillator(float deltaTime)
		{
			currentTime += deltaTime;
			float currentPosition = 0f;
			if (amRunning && ((CurrentPeriod () < totalPeriods) || (totalPeriods < 0))) {
				currentPosition = amplitude * (float)Math.Sin (CurrentAngle ());
			} else {
				currentPosition = 0f;
				amRunning = false;
			}
			return currentPosition;
		}

		public float CurrentPeriod()
		{
			return currentTime / timeConstant;
		}

		public float CurrentAngle()
		{
			return CurrentPeriod() * 2f * (float)Math.PI;
		}

		//Kick off Oscillator
	}

	public class NumHelp
	{
		public static float LimitInclusive(float value, float min, float max)
		{
			return Math.Min(max, Math.Max(value, min));
		}
	}
}
