using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SocketIO;
using UnityStandardAssets.Vehicles.Car;
using System;
using System.Security.AccessControl;
using System.Globalization;

public class CommandServer : MonoBehaviour
{
	public CarRemoteControl CarRemoteControl;
	public Camera FrontFacingCamera;
	private SocketIOComponent _socket;
	private CarController _carController;

	// Use this for initialization
	void Start()
	{
		_socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
		_socket.On("open", OnOpen);
		_socket.On("steer", OnSteer);
		_socket.On("manual", onManual);
		_carController = CarRemoteControl.GetComponent<CarController>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnOpen(SocketIOEvent obj)
	{
		Debug.Log("Connection Open");
		EmitTelemetry(obj);
	}

	// 
	void onManual(SocketIOEvent obj)
	{
		Debug.Log("ONMANUAL KOZIOŁ");
		EmitTelemetry (obj);
	}
	
	void OnSteer(SocketIOEvent obj)
	{
		Debug.Log("HUJ KOZIOŁ");
        JSONObject jsonObject = obj.data;
		print("OTrzymalem fajnego jsona: " + jsonObject);
		// print("Output Steering: " + ;
		print("Output throttle: " + float.Parse(jsonObject.GetField("throttle").str, CultureInfo.InvariantCulture.NumberFormat));
		CarRemoteControl.SteeringAngle = float.Parse(jsonObject.GetField("steering_angle").str, CultureInfo.InvariantCulture.NumberFormat);
		CarRemoteControl.Acceleration = float.Parse(jsonObject.GetField("throttle").str, CultureInfo.InvariantCulture.NumberFormat);
		CarRemoteControl.Brake = float.Parse(jsonObject.GetField("handbrake").str, CultureInfo.InvariantCulture.NumberFormat);

		print("Takie mam parametry : " + CarRemoteControl.SteeringAngle);
		EmitTelemetry(obj);
	}


	void EmitTelemetry(SocketIOEvent obj)
	{
		 UnityMainThreadDispatcher.Instance().Enqueue(() =>
		{
			print("Działam dobrze");
			// send only if it's not being manually driven
			if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S))) {
				_socket.Emit("telemetry", new JSONObject());
			}
			else {
				// Collect Data from the Car
				Dictionary<string, string> data = new Dictionary<string, string>();
				data["steering_angle"] = _carController.CurrentSteerAngle.ToString("N4");
				data["throttle"] = _carController.AccelInput.ToString("N4");
				data["speed"] = _carController.CurrentSpeed.ToString("N4");
				data["image"] = Convert.ToBase64String(CameraHelper.CaptureFrame(FrontFacingCamera));
				_socket.Emit("telemetry", new JSONObject(data));				
			}
		});

		//    UnityMainThreadDispatcher.Instance().Enqueue(() =>
		//    {
		//      	
		//      
		//
		//		// send only if it's not being manually driven
		//		if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S))) {
		//			_socket.Emit("telemetry", new JSONObject());
		//		}
		//		else {
		//			// Collect Data from the Car
		//			Dictionary<string, string> data = new Dictionary<string, string>();
		//			data["steering_angle"] = _carController.CurrentSteerAngle.ToString("N4");
		//			data["throttle"] = _carController.AccelInput.ToString("N4");
		//			data["speed"] = _carController.CurrentSpeed.ToString("N4");
		//			data["image"] = Convert.ToBase64String(CameraHelper.CaptureFrame(FrontFacingCamera));
		//			_socket.Emit("telemetry", new JSONObject(data));
		//		}
		//      
		////      
		//    });
	}
}