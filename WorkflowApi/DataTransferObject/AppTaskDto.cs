namespace WorkflowApi.DataTransferObject
{
    public class AppTaskDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? PriorityId { get; set; }
        public int StateId { get; set; }
        public int TeamId { get; set; }
    }
}
