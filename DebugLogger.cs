using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MKUtil
{
    public static class DebugLogger
    {
        // <summary>
        // Handles all Unity Debug.Log messages so it can be turned on or off to show or to log.
        // It keeps all messages added to it, too, so it can be returned with ReadAll.
        // </summary>

        // Example:
        // MKUtil.DebugLogger.Setting = true;
        // MKUtil.DebugLogger.Log("Key {0} exists already.", key);

        public struct Pair
        {
            readonly Type _type;
            readonly string _message;
            public Pair(string message) : this(message, typeof(Nullable))
            {
            }
            public Pair(string message, Type type)
            {
                this._type = type;
                this._message = message;
            }
            public override string ToString()
            {
                return string.Format("({0}) {1}", _type.Name, _message);
            }
        }

        static System.Text.StringBuilder sb = new System.Text.StringBuilder();
        static List<Pair> logList = new List<Pair>();

        public static bool baseDatabaseBool = false;

        static bool debugOn = false;
        public static bool Setting
        {
            get => debugOn;
            set
            {
                if (debugOn != value)
                {
                    debugOn = value;
                }
            }
        }

        public static string GetLog()
        {
            sb.Clear();
            sb.AppendLine("--- Full Log History ---");

            foreach (Pair pair in logList)
            {
                sb.AppendLine(pair.ToString());
            }
            var fullLog = sb.ToString();
            sb.Clear();

            return fullLog;
        }
        public static void ReadAll()
        {
            UnityEngine.Debug.Log(GetLog());
        }

        #region Log
        public static void Log(Type type, string message)
        {
            logList.Add(new Pair(message, type));
            if (!debugOn)
            {
                return;
            }
            UnityEngine.Debug.LogFormat("{0} : {1}", type, message);
        }
        public static void Log(string message)
        {
            logList.Add(new Pair(message));
            if (!debugOn)
            {
                return;
            }
            UnityEngine.Debug.LogFormat("-- : {1}", message);
        }
        public static void Log(Type type, string format, params object[] param)
        {
            sb.Clear();
            sb.AppendFormat(format, param);
            logList.Add(new Pair(sb.ToString(), type));
            if (!debugOn)
            {
                return;
            }
            UnityEngine.Debug.LogFormat("{0} : {1}", type, sb.ToString());
        }
        public static void Log(bool value, Type type, string message)
        {
            if (value && debugOn)
            {
                Log(type, message);
            }
        }
        public static void Log(bool value, Type type, string message, params object[] param)
        {
            if (value && debugOn)
            {
                Log(type, message, param);
            }
        }
        #endregion
    }

}