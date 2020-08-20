using System;

public class TodoItem 
{
    public long Id { get; set; }
    public string name { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}