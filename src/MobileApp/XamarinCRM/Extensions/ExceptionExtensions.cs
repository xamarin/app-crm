//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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

