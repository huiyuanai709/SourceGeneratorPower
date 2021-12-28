namespace HttpClientSample.Models;

public class Todo
{
    public int UserId { get; set; }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Completed { get; set; }
}

public class CreateTodo
{
    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public bool Completed { get; set; }
}