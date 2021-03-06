﻿/***************************************************************************
 *   FileManager.cs
 *   Part of OpenUO: http://code.google.com/p/OpenUO
 *   Based on code from UltimaSDK: http://ultimasdk.codeplex.com/
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using System;
using System.IO;
using Microsoft.Win32;
using OpenUO.Diagnostics;

namespace OpenUO.UltimaData
{
    class FileManager
    {
        static private bool _isDataPresent = false;
        static public bool IsUODataPresent
        {
            get { return _isDataPresent; }
        }

        static readonly string[] _knownRegkeys = new string[] { 
                @"Origin Worlds Online\Ultima Online\KR Legacy Beta", 
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", 
                @"Origin Worlds Online\Ultima Online\1.0", 
                @"Origin Worlds Online\Ultima Online Third Dawn\1.0",
                @"EA GAMES\Ultima Online Samurai Empire", 
                @"EA Games\Ultima Online: Mondain's Legacy", 
                @"EA GAMES\Ultima Online Samurai Empire\1.0", 
                @"EA GAMES\Ultima Online Samurai Empire\1.00.0000", 
                @"EA GAMES\Ultima Online: Samurai Empire\1.0", 
                @"EA GAMES\Ultima Online: Samurai Empire\1.00.0000", 
                @"EA Games\Ultima Online: Mondain's Legacy\1.0", 
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", 
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\2d\1.0", 
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\3d\1.0", 
                @"Origin Worlds Online\Ultima Online Samurai Empire\2d\1.0", 
                @"Origin Worlds Online\Ultima Online Samurai Empire\3d\1.0",
                @"Electronic Arts\EA Games\Ultima Online Stygian Abyss Classic",
                @"Electronic Arts\EA Games\"
            };

        private static string m_FileDirectory;

        public static bool Is64Bit
        {
            get { return IntPtr.Size == 8; } 
        }

        static FileManager()
        {
            Logger.Debug("Looking for UO Installation. Is64Bit = {0}", Is64Bit);
            string exePath = @"C:\Ultima\";
            Logger.Debug("Found UO Installation at [{0}].", exePath);

            m_FileDirectory = exePath;
            _isDataPresent = true;

            //for (int i = 0; i < _knownRegkeys.Length; i++)
            //{
            //    string exePath;

            //    if (Is64Bit)
            //    {
            //        exePath = GetExePath(@"Wow6432Node\" + _knownRegkeys[i]);
            //    }
            //    else
            //    {
            //        exePath = GetExePath(_knownRegkeys[i]);
            //    }

            //    if (exePath != null && Directory.Exists(exePath))
            //    {
            //        Logger.Debug("Found UO Installation at [{0}].", exePath);

            //        m_FileDirectory = exePath;
            //        _isDataPresent = true;
            //    }
            //}
            //if (m_FileDirectory == null)
            //{
            //    Logger.Fatal("Did not find UO Installation.");
            //    _isDataPresent = false;
            //}
        }

        private static string GetExePath(string subName)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));

                if (key == null)
                {
                    key = Registry.CurrentUser.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));

                    if (key == null)
                    {
                        return null;
                    }
                }

                string path = key.GetValue("ExePath") as string;

                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = key.GetValue("Install Dir") as string;

                    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                    {
                        path = key.GetValue("InstallDir") as string;

                        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                        {
                            return null;
                        }
                    }
                }

                if (File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                }

                if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                {
                    return null;
                }

                return path;
            }
            catch
            {
                return null;
            }
        }

        public static string GetFilePath(string name)
        {
            if (m_FileDirectory != null)
            {
                name = Path.Combine(m_FileDirectory, name);
                // Fix for opening files which don't exist -Smjert
                if (File.Exists(name))
                    return name;
            }

            return null;
        }

        public static bool Exists(string name)
        {
            try
            {
                name = Path.Combine(m_FileDirectory, name);
                Logger.Debug("Checking if file exists [{0}]", name);

                if (File.Exists(name))
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        public static bool Exists(string name, int index)
        {
            return Exists(String.Format(name, index));
        }

        public static bool Exists(string name, int index, string type)
        {
            return Exists(String.Format("{0}{1}.{2}", name, index, type));
        }

        public static bool Exists(string name, string type)
        {
            return Exists(String.Format("{0}.{1}", name, type));
        }

        public static FileStream GetFile(string name)
        {
            try
            {
                name = Path.Combine(m_FileDirectory, name);

                return new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch { return null; }
        }

        public static FileStream GetFile(string name, int index)
        {
            return GetFile(String.Format(name, index));
        }

        public static FileStream GetFile(string name, int index, string type)
        {
            return GetFile(String.Format("{0}{1}.{2}", name, index, type));
        }

        public static FileStream GetFile(string name, string type)
        {
            return GetFile(String.Format("{0}.{1}", name, type));
        }
    }
}