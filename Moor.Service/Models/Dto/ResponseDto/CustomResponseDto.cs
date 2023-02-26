using System.Text.Json.Serialization;

namespace Moor.Service.Models.Dto.ResponseDto
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<string?> Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }


        //StaticFactoryMethod (StaticFactoryMethodDesingPattern) Like FactoryDesingPattern
        public static CustomResponseDto<T> Succces(int statusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }

        public static CustomResponseDto<T> Succces(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode};
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
