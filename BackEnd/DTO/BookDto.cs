namespace BackEnd.DTO
{

    [Serializable] //for serialization and deserialization
    public record BookDto(int Id, string Title, decimal Price);  
}