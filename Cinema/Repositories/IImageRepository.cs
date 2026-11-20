namespace Cinema.Repositories
{
    public interface IImageRepository
    {
        Task<string> SaveAsync(IFormFile file);
        void Delete(string? fileName);
    }
}