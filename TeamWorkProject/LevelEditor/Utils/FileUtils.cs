namespace LevelEditor.Utils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class FileUtils
    {
        private const string ContentDir = @"..\..\..\Content\";

        private static readonly ICollection<string> CachedFilenames = new List<string>();

        private static readonly string[] FoldersToAvoid = { "bin", "obj", "Levels", "Font" };

        public static IEnumerable<string> GetFilenames(string targetDir = null)
        {
            if (CachedFilenames.Count == 0)
            {
                LoadFilenames(ContentDir, CachedFilenames);
            }

            if (targetDir != null)
            {
                return CachedFilenames.Where(filename => filename.Contains(targetDir));
            }

            return CachedFilenames;
        }

        private static void LoadFilenames(string sourceDir, ICollection<string> fileNames)
        {
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                if (!FoldersToAvoid.Any(dir.Contains))
                {
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        fileNames.Add(FormatNameForContentLoad(file));
                    }
                }
                LoadFilenames(dir, fileNames);
            }
        }

        private static string FormatNameForContentLoad(string path)
        {
            var filePath = path.Split('\\');

            var targetPath = new List<string>();
            var contentReached = false; 
            foreach (string folder in filePath)
            {
                if (contentReached)
                {
                    targetPath.Add(folder);
                }

                if (folder.Equals("Content"))
                {
                    contentReached = true;
                }
            }

            // Get rid of the extension:
            var filename = targetPath[targetPath.Count - 1];
            targetPath[targetPath.Count - 1] = filename.Substring(0, filename.LastIndexOf('.'));

            return string.Join("/", targetPath);
        }
    }
}
