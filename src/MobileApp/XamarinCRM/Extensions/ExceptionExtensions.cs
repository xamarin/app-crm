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

namespace XamarinCRM.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets a formatted string with the calling class name, exception message, and stack trace.
        /// </summary>
        /// <returns>The formatted debug message.</returns>
        /// <param name="ex">The <see cref="system.Exception"/>.</param>
        /// <param name="callingClass">The class in which the exception was thrown. Usually you'll use the 'this' keyword for this parameter.</param>
        public static string ToFormattedDebugMessage(this Exception ex, object callingClass)
        {
            return string.Format("EXCEPTION: {0}: {1}  |  {2}", callingClass.GetType().FullName, ex.Message, ex.StackTrace );
        }

        /// <summary>
        /// Gets a formatted string with the calling class name, exception message, and stack trace.
        /// </summary>
        /// <returns>The formatted debug message.</returns>
        /// <param name="ex">The <see cref="System.Exception"/>.</param>
        /// <param name="callingClassType">The <see cref="System.Type"/> of the calling class. Usually you'll pass 'typeof(CallingClass)' for this parameter.</param>
        public static string ToFormattedDebugMessage(this Exception ex, Type callingClassType)
        {
            return string.Format("EXCEPTION: {0}: {1}  |  {2}", callingClassType.FullName, ex.Message, ex.StackTrace );
        }

        /// <summary>
        /// Writes a formatted string to the debug output with the calling class name, exception message, and stack trace.
        /// </summary>
        /// <param name="ex">The <see cref="system.Exception"/>.</param>
        /// <param name="callingClass">The class in which the exception was thrown. Usually you'll use the 'this' keyword for this parameter.</param>
        public static void WriteFormattedMessageToDebugConsole(this Exception ex, object callingClass)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToFormattedDebugMessage(callingClass));
        }

        /// <summary>
        /// Writes a formatted string to the debug output with the calling class name, exception message, and stack trace.
        /// </summary>
        /// <param name="ex">The <see cref="System.Exception"/>.</param>
        /// <param name="callingClassType">The <see cref="System.Type"/> of the calling class. Usually you'll pass 'typeof(CallingClass)' for this parameter.</param>
        public static void WriteFormattedMessageToDebugConsole(this Exception ex, Type callingClassType)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToFormattedDebugMessage(callingClassType));
        }
    }
}

