/***************************************************************************
 *  HalContext.cs
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
using System.Runtime.InteropServices;

namespace Hal
{
	public class Context : IDisposable
	{
		private HandleRef ctx_handle;
		private IntPtr dbus_conn;
		private bool initialized;
		
		public Context()
		{
			IntPtr ctx = Unmanaged.libhal_ctx_init_direct(IntPtr.Zero);
			if(ctx == IntPtr.Zero)
				throw new HalException("Could not create Direct HAL Context");
			
			ctx_handle = new HandleRef(this, ctx);
		}
		
		public Context(IntPtr dbus_conn)
		{
			IntPtr ctx = Unmanaged.libhal_ctx_new();
			if(ctx == IntPtr.Zero)
				throw new HalException("Could not create HAL Context"); 
			
			ctx_handle = new HandleRef(this, ctx);	
			DbusConnection = dbus_conn;
		}
		
		public Context(DbusBusType type) : 
			this(Unmanaged.dbus_bus_get(type, IntPtr.Zero))
		{
		
		}
		
		public Context(DbusBusType type, bool initialize) : this(type)
		{
			Initialize();
		}
		
		~Context()
		{
			Cleanup();
		}
		
		public void Dispose()
		{
			Cleanup();
		}
		
		private void Cleanup()
		{
			ContextShutdown();
			ContextFree();
		}

		private bool ContextShutdown()
		{
			if(ctx_handle.Handle == IntPtr.Zero || !initialized)
				return false;
				
			return Unmanaged.libhal_ctx_shutdown(ctx_handle, IntPtr.Zero);
		}

		private bool ContextFree()
		{
			if(ctx_handle.Handle == IntPtr.Zero)
				return false;

			return Unmanaged.libhal_ctx_free(ctx_handle);
		}
				
		public void Initialize()
		{
			if(!Unmanaged.libhal_ctx_init(ctx_handle, IntPtr.Zero))
				throw new HalException("Could not initialize HAL Context");
			
			initialized = true;
		}
		
		public IntPtr DbusConnection
		{
			set {
				dbus_conn = value;
				if(!Unmanaged.libhal_ctx_set_dbus_connection(ctx_handle, dbus_conn))
					throw new HalException("Could not set D-Bus Connection for HAL");
			}
		}
		
		public bool UseCache
		{
			set {
				if(!Unmanaged.libhal_ctx_set_cache(ctx_handle, value))
					throw new HalException("Could not set D-Bus Use Cache to '" 
						+ value + "'");
			}
		}
		
		public HandleRef Raw
		{
			get {
				return ctx_handle;
			}
		}
	}
}
