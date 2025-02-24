﻿using IntelDrawingDataBackend.Entities;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace IntelDrawingDataBackend.DB
{
    // Sqlite3 数据库引用
    public class Sqlite3DBSupport
    {
        private const string FileName = "IntelDrawingDataBackend.db";
        [DllImport("Sqlite3DBSupport", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Sqlite3Exe([MarshalAs(UnmanagedType.LPStr)] string filename,
                                       [MarshalAs(UnmanagedType.LPStr)] string sql);
        static string? Sqlite3ExE(string sql)// 使用这个函数进行调用
        {
            IntPtr ptr = Sqlite3Exe(FileName, sql);
            return Marshal.PtrToStringAnsi(ptr);
        }

        public static SqlResult? Exe(string sql)
        {
            string? r = Sqlite3ExE(sql);
            if(r == null)
                throw new Exception($"Sqlite3 execute fail {sql}");
            if (r[0] == '#')
                throw new Exception(r + "\nsql: " + sql);

            return JsonConvert.DeserializeObject<SqlResult>(r);
        }
    }
}
