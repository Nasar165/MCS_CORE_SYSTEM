using System.Collections.Generic;
using System.IO;
using xfilehash;

namespace api
{
    public class StartFileHasher
    {
        private static List<FileHash> Files()
        {
            return new List<FileHash>(){
                new FileHash(){ FileName = "sqlqueries", FilePath = "scripts/sqlqueries.json"}
            };
        }
        public static void RegisterFiles()
        {
            var directoryPath = Directory.GetCurrentDirectory();
            if (!File.Exists($"{directoryPath}/Security/hash/filehash.json"))
            {
                var algorithm = new XSha256Algorithm();
                var hasher = new XFileIntegrity(algorithm);
                foreach (var file in Files())
                    hasher.AddFileHashToIntegrityStore(file.FileName, $"{directoryPath}/{file.FilePath}");
            }
        }
    }
}