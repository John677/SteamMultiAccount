﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;

namespace SteamMultiAccount
{
    enum LogType
    {
        Error,
        Info,
        Warning,
        Debug,
        User
    }
    internal class Logging
    {
        private string _rtfText;
        internal Logging()
        {
            _rtfText = "";
        }
        internal void Log(string message, LogType type = LogType.Info, [CallerMemberName] string functionName = "")
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            string logMess;
#if !DEBUG
            functionName = "";
#else
            functionName += "() ";
#endif
            switch (type)
            {
                case LogType.Debug:
                    logMess = "   [DEBUG]  " + functionName + message;
                    Log(logMess, Color.DarkViolet);
                    break;
                case LogType.Error:
                    logMess = "  [ERROR]  " + functionName + message;
                    Log(logMess, Color.DarkRed);
                    break;
                case LogType.Info:
                    logMess = "    [INFO]    " + functionName + message;
                    Log(logMess, Color.Blue);
                    break;
                case LogType.User:
                    logMess = "   [USER]   " + message;
                    Log(logMess, Color.Black);
                    break;
                case LogType.Warning:
                    logMess = "[WARNING]" + functionName + message;
                    Log(logMess, Color.OrangeRed);
                    break;
            }
        }
        private void Log(string message,Color col)
        {
            if (string.IsNullOrEmpty(message))
                return;
            string loggedMessage = DateTime.Now + " " + message;
            RichTextBox _rBox = new RichTextBox {Rtf = _rtfText};

            _rBox.SelectionStart = _rBox.TextLength;
            _rBox.SelectionLength = 0;

            _rBox.SelectionColor = col;
            _rBox.AppendText(loggedMessage);
            _rBox.SelectionColor = _rBox.ForeColor;
            _rBox.AppendText(Environment.NewLine);
            _rtfText = _rBox.Rtf;
            _rBox.Dispose();
        }
        internal string GetLogBoxText()
        {
            return _rtfText;
        }
        internal static void DebugLogToFile(string message)
        {
            message += Environment.NewLine;
            string path = "debug/SteamKitLog.txt";
            lock (path)
            {
                try
                {
                    File.AppendAllText(path, message);
                } catch { 

                }
            }
        }
        internal static void LogToFile(string message, string botname="Program")
        {
            string path = "log.txt";
            string loggingmessage = DateTime.Now + " <"+botname+"> "+ message + Environment.NewLine;
            lock (path)
            {
                try {
                    File.AppendAllText(path, loggingmessage);
                } catch { 
                    // ignored
                }
            }
        }
        
    }
}
