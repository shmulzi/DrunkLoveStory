using System.IO;

public class FileUtils {

	public static string ReadStringFromPath(string path)
    {
        string result = null;
        StreamReader reader = new StreamReader(path);
        result = reader.ReadToEnd();
        reader.Close();
        return result;
    }

    public static void SaveStringToPath(string str, string path)
    {
        File.WriteAllText(path, str);
    }
}
