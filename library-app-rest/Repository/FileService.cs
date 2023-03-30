using library_app_rest.Repository.IRepository;

namespace library_app_rest.Repository;

public class FileService: IFileService
{

    private readonly IWebHostEnvironment _env;
    public FileService(IWebHostEnvironment env)
    {
        this._env = env;
    }
    
    public async Task<Tuple<int, string>> SaveFile(IFormFile file, Guid folderId)
    {
        try
        {
            var contentPath = this._env.WebRootPath + "/images";
            var path = Path.Combine(contentPath, folderId.ToString()); 
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            if(System.IO.Directory.GetFiles(path:path).Length > 0)
            {
                foreach (var f in System.IO.Directory.GetFiles(path))
                {
                    System.IO.File.Delete(f);
                }
            }

            // Check the allowed extenstions
            var ext = Path.GetExtension(file.FileName);
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg", ".gif" };
            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                return new Tuple<int, string>(0, msg);
            }
            string uniqueString = Guid.NewGuid().ToString();
            // we are trying to create a unique filename here
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            using (var stream = new FileStream(fileWithPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            };
            return new Tuple<int, string>(1, newFileName);
        }
        catch (Exception ex)
        {
            return new Tuple<int, string>(0, "Error has occured");
        }
    }

    public bool DeleteFile(string fileName)
    {
        try
        {
            var wwwPath = this._env.WebRootPath;
            var path = Path.Combine(wwwPath, "images\\", fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<string> GetFileNames(Guid folderId)
    {
        var contentPath = this._env.WebRootPath + "/images";
        var path = Path.Combine(contentPath, folderId.ToString());
        List<string> images = new List<string>();
        if (System.IO.Directory.Exists(path))
        {
            var filesGotten = System.IO.Directory.EnumerateFiles(path);
            foreach (var fileInfo in filesGotten)
            {
                images.Add(fileInfo.Split("/").Last());
            }
        }
        
        return images;
    }
}