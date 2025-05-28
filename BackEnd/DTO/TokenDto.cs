namespace BackEnd.DTO
{
    public record TokenDto
    {
        public string? AccessToken { get; init; } //readonly property
        public string? RefreshToken { get; init; } //readonly property
    }
}