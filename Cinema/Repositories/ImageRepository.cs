namespace Cinema.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly string _imagesPath;

        public ImageRepository(IWebHostEnvironment env)
        {
            _imagesPath = Path.Combine(env.WebRootPath, "images");
        }

        public async Task<string> SaveAsync(IFormFile file)
        {
            Directory.CreateDirectory(_imagesPath);

            var fileName = Guid.NewGuid().ToString() + '-' + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_imagesPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public void Delete(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var filePath = Path.Combine(_imagesPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}