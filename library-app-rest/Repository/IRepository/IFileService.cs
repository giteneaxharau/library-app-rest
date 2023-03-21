namespace library_app_rest.Repository.IRepository;

public interface IFileService
{ 
    Task<Tuple<int, string>> SaveFile(IFormFile file, Guid folderId);
    bool DeleteFile(string fileName);
    
    public List<string> GetFileNames(Guid folderId);
}