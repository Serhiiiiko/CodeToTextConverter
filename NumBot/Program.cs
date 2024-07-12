using System.Text;

class Program
{
    static void Main()
    {
        string rootPath = @"D:\VisualStudioRepos\beutl\src";
        var header = "***********************************" + Environment.NewLine;

        var extensions = new[] { "*.cs", "launchSettings.json", "appsettings.json", "webpack.config.js",
            "*.csproj", "docker-compose.yml","docker-compose.override.yml" , "*.cshtml", "script.js", "*.jsx" ,"*.config.js"};
        var files = extensions.SelectMany(ext => Directory.GetFiles(rootPath, ext, SearchOption.AllDirectories));

        var stringBuilder = new StringBuilder();

        var results = files.AsParallel().Select(path => new { Directory = Path.GetDirectoryName(path), Name = Path.GetFileName(path), Contents = File.ReadAllText(path) })
                          .ToList();

        foreach (var info in results)
        {
            stringBuilder.Append(header);
            stringBuilder.Append("Directory: ").Append(info.Directory).Append(Environment.NewLine);
            stringBuilder.Append("Filename: ").Append(info.Name).Append(Environment.NewLine);
            stringBuilder.Append(header);
            stringBuilder.Append(info.Contents).Append(Environment.NewLine);
        }

        var singleStr = stringBuilder.ToString();
        Console.WriteLine(singleStr);
        File.WriteAllText(@"D:\VisualStudioRepos\NumBot\NumBot\Output\output.txt", singleStr, Encoding.UTF8);
    }
}
