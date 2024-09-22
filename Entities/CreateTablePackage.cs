﻿using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    public class CreateTablePackage
    {
        public string fileName { get; set; }
        // 第一行是表头
        // a1 a2 a3 a4
        // b1 b2 b3 b4
        // ...
        public List<List<string>> data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
