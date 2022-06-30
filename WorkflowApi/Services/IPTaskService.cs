using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public interface IPTaskService
    {
        List<PTaskDto> GetAllPtask(int userId);
    }
}
