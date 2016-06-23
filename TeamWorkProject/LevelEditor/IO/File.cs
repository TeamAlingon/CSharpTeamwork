namespace LevelEditor.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public static class File
    {
        private const string ContentDir = @"..\..\..\Content\";

        private const string SerializationDir = @"..\..\..\Content\Serialized\";

        private static readonly ICollection<string> CachedFilenames = new List<string>();

        private static readonly string[] KeywordsToAvoid = { "bin", "obj" };

        public static void Save<T>(string file, T obj, string path = SerializationDir)
        {
            using (TextWriter writer = new StreamWriter(path + file))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(writer, obj);
            }
        }

        public static T Load<T>(string file, string path = SerializationDir)
        {
            T result;
            using (TextReader reader = new StreamReader(path + file))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                result = (T)xmlSerializer.Deserialize(reader);
            }
            return result;
        }

        public static IEnumerable<string> GetFilenames(string targetKeyword = null)
        {
            if (CachedFilenames.Count == 0)
            {
                LoadFilenames(ContentDir, CachedFilenames);
            }

            if (targetKeyword != null)
            {
                return CachedFilenames.Where(filename => filename.Contains(targetKeyword));
            }

            return CachedFilenames;
        }

        private static void LoadFilenames(string sourceDir, ICollection<string> fileNames)
        {
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                if (!KeywordsToAvoid.Any(dir.Contains))
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
            var filename = targetPath.LastOrDefault();
            if (filename != null && filename.Contains("."))
            {
                targetPath[targetPath.Count - 1] = filename.Substring(0, filename.LastIndexOf('.'));
            }

            return string.Join("/", targetPath);
        }
    }
}
