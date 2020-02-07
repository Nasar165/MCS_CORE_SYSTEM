namespace Components.Interface
{
    public interface IFileIntegrity
    {
        bool FileIntegrityIsIntact(string fileName);
        void AddFileHashToIntegrityStore(string fileName, string filePath);
        void DeleteFileIntegrityFromStore(string fileName);
        void UpdateFileInterity(string fileName);
    }
}