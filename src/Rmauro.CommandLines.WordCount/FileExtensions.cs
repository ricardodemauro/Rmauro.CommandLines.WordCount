namespace Rmauro.CommandLines.WordCount;

public static class FileExtensions
{
    public static FileStream GetStream(this string path)
    {
        var fStream = new FileStream(path, new FileStreamOptions
        {
            Access = FileAccess.Read,
            BufferSize = 4096,
            Mode = FileMode.Open,
            Options = FileOptions.SequentialScan,
            Share = FileShare.Inheritable
        });

        return fStream;
    }

    public static FileInfo GetFileInfo(this string path)
    {
        return new FileInfo(path);
    }

    public static string GetFileName(this string path)
    {
        return new FileInfo(path).Name;
    }

    public static string GetFullPath(this string path)
    {
        if(Path.IsPathRooted(path))return path;
        
        return Path.Combine(Directory.GetCurrentDirectory(), path);
    }
}
