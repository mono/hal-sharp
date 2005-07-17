/***************************************************************************
 *  HalTest.cs
 *
 *  Copyright (C) 2005 Novell
 *  Written by Aaron Bockover (aaron@aaronbock.net)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
 
using System;
using Hal;
using Gtk;

public class Entry
{
	public static void Main()
	{
		Application.Init();
		new GtkTest();
		Application.Run();
		
		/* Context ctx = new Context(); */
		
		/*foreach(Device device in Device.FindByCapability(ctx, "net"))
			device.Print();*/
		
		/*foreach(Device device in Device.GetAll(ctx))
			device.Print();*/
		
		/*foreach(Device device in Device.FindByStringMatch(ctx, "info.vendor", "Intel Corporation"))
			device.Print();*/
		
		/*Device device = new Device(ctx, "/org/freedesktop/Hal/devices/net_00_05_4e_42_57_6b");
		string [] capabilities = device.GetPropertyStringList("info.capabilities");
		foreach(string cap in capabilities)
			Console.WriteLine(cap);*/
	}
}

public class GtkTest
{
	private Window win;
	private Context ctx;
	
	public GtkTest()
	{
		ctx = new Context();
		ctx.DeviceAdded += OnHalDeviceAdded;
		ctx.DeviceRemoved += OnHalDeviceRemoved;
		ctx.DeviceCondition += OnHalDeviceCondition;
		ctx.DeviceLostCapability += OnHalDeviceLostCapability;
		ctx.DeviceNewCapability += OnHalDeviceNewCapability;
		ctx.DevicePropertyModified += OnHalDevicePropertyModified;
		
		win = new Window("HAL Event Loop Test");
		win.DeleteEvent += OnWindowDeleteEvent;
		win.ShowAll();
	}
	
	public void OnHalDeviceAdded(object o, DeviceAddedArgs args)
	{
		Console.WriteLine("Device Added: " + args.Device);
		args.Device.WatchProperties = true;
	}
	
	public void OnHalDeviceRemoved(object o, DeviceRemovedArgs args)
	{
		Console.WriteLine("Device Removed: " + args.Device);
	}
	
	public void OnHalDeviceCondition(object o, DeviceConditionArgs args)
	{
		Console.WriteLine("Device Condition: " + args.Device);
	}
	
	public void OnHalDeviceLostCapability(object o, 
		DeviceLostCapabilityArgs args)
	{
		Console.WriteLine("Device Lost Capability: " + args.Device);	
	}
	
	public void OnHalDeviceNewCapability(object o,
		DeviceNewCapabilityArgs args)
	{
		Console.WriteLine("Device New Capability: " + args.Device);
	}
	
	public void OnHalDevicePropertyModified(object o,
		DevicePropertyModifiedArgs args)
	{
		Console.WriteLine("Device Property Modified: " + args.Device +
			", Key: " + args.Key + ", Is Removed: " + args.IsRemoved +
			", Is Added: " + args.IsAdded);
	}
	
	public void OnWindowDeleteEvent(object o, EventArgs args)
	{
		Application.Quit();
	}
}
