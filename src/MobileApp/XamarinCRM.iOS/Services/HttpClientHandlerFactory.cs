// The MIT License (MIT)
//
// Copyright (c) 2015 Xamarin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using XamarinCRM.Services;
using System.Net.Http;
using XamarinCRM.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(HttpClientHandlerFactory))]

namespace XamarinCRM.iOS.Services
{
    /// <summary>
    /// Http client handler factory for iOS. Allows the simulator to use the host operating systems's (OS X) proxy settings in order to allow debugging of HTTP requests with tools such as Charles.
    /// Only used for debugging, not production.
    /// </summary>
    public class HttpClientHandlerFactory : IHttpClientHandlerFactory
    {
        public HttpClientHandler GetHttpClientHandler()
        {
            return new System.Net.Http.HttpClientHandler {
                Proxy = CoreFoundation.CFNetwork.GetDefaultProxy(),
                UseProxy = true
            };
        }
    }
}

