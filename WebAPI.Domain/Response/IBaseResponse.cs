namespace WebAPI.Domain.Response;

public enum StatusCode
{
    Ok = 200,
    NotFound = 404,
    InternalServerError = 500
}

public interface IBaseResponse<T>
{
    string Description { get; set; }
    StatusCode StatusCode { get; set; }
    T Data { get; set; }
}