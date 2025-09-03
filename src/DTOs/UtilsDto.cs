namespace ClientManager.src.DTOs;

public class ApiResponse<T>
{
    public required string Message {get; set;}
    public required T Data {get; set;}
}

public class ApiResponseMessage
{
    public required string Message {get; set;}
}

public class ApiResponseTable<T>
{
    public required string Message {get; set;}
    public required int TotalItems {get; set;}
    public required List<T> Data {get; set;}
}

public class OptionDto
{
    public required string Value { get; set; }
    public required string Label { get; set; }
}