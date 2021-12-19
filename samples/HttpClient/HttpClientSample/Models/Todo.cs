namespace HttpClientSample.Models;

public class TodoRequest
{
    public string UserId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public bool Completed { get; set; }
}

public class TodoResponse
{
    public int UserId { get; set; }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Completed { get; set; }
}