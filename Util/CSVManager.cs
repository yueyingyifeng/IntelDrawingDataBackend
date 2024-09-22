using System;
using System.Collections.Generic;
using System.IO;

// CSV 文件管理类
// chatGPT 编写
public class CSVManager
{
    private List<List<string>> _data;

    public CSVManager()
    {
        _data = new List<List<string>>();
    }

    public CSVManager(List<List<string>> data)
    {
        _data = data;
    }

    public bool isFileExists(string filePath)
    {
        return Directory.Exists( filePath );
    }

    // 读取 CSV 文件
    public void LoadFromFile(string filePath)
    {
        _data.Clear();
        if (File.Exists(filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    List<string> row = new List<string>(line.Split(','));
                    _data.Add(row);
                }
            }
        }
        else
        {
            throw new FileNotFoundException($"File {filePath} not found.");
        }
    }

    // 保存数据到 CSV 文件
    public void SaveToFile(string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var row in _data)
            {
                sw.WriteLine(string.Join(",", row));
            }
        }
    }

    // 获取所有数据
    public List<List<string>> GetAllData()
    {
        return _data;
    }

    // 获取指定行
    public List<string> GetRow(int rowIndex)
    {
        if (rowIndex >= 0 && rowIndex < _data.Count)
        {
            return _data[rowIndex];
        }
        throw new IndexOutOfRangeException("Row index out of range.");
    }

    // 添加行数据
    public void AddRow(List<string> rowData)
    {
        _data.Add(rowData);
    }

    // 更新指定行数据
    public void UpdateRow(int rowIndex, List<string> newData)
    {
        if (rowIndex >= 0 && rowIndex < _data.Count)
        {
            _data[rowIndex] = newData;
        }
        else
        {
            throw new IndexOutOfRangeException("Row index out of range.");
        }
    }

    // 删除指定行
    public void DeleteRow(int rowIndex)
    {
        if (rowIndex >= 0 && rowIndex < _data.Count)
        {
            _data.RemoveAt(rowIndex);
        }
        else
        {
            throw new IndexOutOfRangeException("Row index out of range.");
        }
    }

    // 查找某个字符串在文件中的位置
    public (int rowIndex, int colIndex)? FindString(string value)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            for (int j = 0; j < _data[i].Count; j++)
            {
                if (_data[i][j] == value)
                {
                    return (i, j);
                }
            }
        }
        return null; // 没有找到
    }
}
