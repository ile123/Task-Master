namespace Model.Dtos;

public record PaginationDto<T>(T Elements, long TotalElements);