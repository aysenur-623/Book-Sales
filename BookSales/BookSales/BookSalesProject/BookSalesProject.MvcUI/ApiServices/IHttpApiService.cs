namespace BookSalesProject.MvcUI.ApiServices
{
    public interface IHttpApiService
    {
        // Bir servisle haberleşirken yaptığımız operasyonlar : 
        // Get
        // Post
        // Put
        // Delete

        Task<T> GetData<T>(string requestUri, string? token = null);
        Task<T> PostData<T>(string requestUri, string jsonData, string? token = null);
        Task<T> PutData<T>(string requestUri, string jsonData, string? token = null);
        Task<bool> DeleteData(string requestUri, string? token = null);
    }
}