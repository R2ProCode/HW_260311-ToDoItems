namespace HW_260311_ToDoItems.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
