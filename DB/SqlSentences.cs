using IntelDrawingDataBackend.Entities;

namespace IntelDrawingDataBackend.DB
{
    public class SqlSentences
    {
        public static string init_tables =
            "CREATE TABLE IF NOT EXISTS User (" +
            "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
            "Name TEXT NOT NULL," +
            "Email TEXT NOT NULL" +
            ");" +
            "" +
            "CREATE TABLE IF NOT EXISTS UserInfo (" +
            "ID INTEGER PRIMARY KEY," +
            "PSW TEXT NOT NULL," +
            "LastLogin DATETIME," +
            "CreateDate DATETIME NOT NULL," +
            "FOREIGN KEY(ID) REFERENCES User(ID) ON DELETE CASCADE ON UPDATE CASCADE" +
            ");" +
            "" +
            "CREATE TABLE IF NOT EXISTS UserData (" +
            "ID INTEGER," +
            "FileID INTEGER," +
            "PRIMARY KEY(ID, FileID)," +
            "FOREIGN KEY(ID) REFERENCES User(ID) ON DELETE CASCADE ON UPDATE CASCADE," +
            "FOREIGN KEY(FileID) REFERENCES FileInfo(FileID) ON DELETE CASCADE ON UPDATE CASCADE" +
            ");" +
            "" +
            "CREATE TABLE IF NOT EXISTS FileInfo (" +
            "FileID INTEGER PRIMARY KEY AUTOINCREMENT," +
            "FilePath TEXT NOT NULL," +
            "CreateDate DATETIME NOT NULL" +
            ");";


        public static string login_checking(LoginPackage package)
        {
            if(package.id != null)
            {
                return $"SELECT User.ID, User.Email, User.Name, UserInfo.CreateDate, UserInfo.PSW  \r\nFROM User\r\nINNER JOIN UserInfo ON User.ID = UserInfo.ID\r\nWHERE User.ID = {package.id}";
            }
            else
            {
                return $"SELECT User.ID, User.Email, User.Name, UserInfo.CreateDate, UserInfo.PSW \r\nFROM User\r\nINNER JOIN UserInfo ON User.ID = UserInfo.ID\r\nWHERE User.Email = {package.email}";
            }
        }
    }
}
