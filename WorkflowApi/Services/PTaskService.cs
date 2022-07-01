using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkflowApi.Data;
using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public class PTaskService : IPTaskService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PTaskService> _logger;

        public PTaskService(ApplicationDbContext dbContext, ILogger<PTaskService> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public List<PTaskDto> GetAllPtaskByTeamId(int teamId, List<Claim> ClaimList )
        {
            this._dbContext.PTasks
                .Include(pt=>pt.TeamPTask)
                .Include(pt=>pt.)
                
        }   



        //public List<PTaskDto> GetAllPtask(int teamId)
        //{
        //    var PTasksQuery = this._dbContext.PTasks
        //                    .Where(Pt=> Pt.TeamId == teamId)
        //                    .Include(PT=>PT.State)
        //                    .Include(PT=>PT.Priority);

        //    List<PTaskDto> pTasksDtoList = PTasksQuery.Select(PT =>
        //        new PTaskDto()
        //        {
        //            Id = PT.Id,
        //            StartDate = PT.StartDate,
        //            EndDate=PT.EndDate,
        //            Title=PT.Title,
        //            Description=PT.Description,
        //            TeamId=PT.TeamId,
        //            PriorityId=PT.PriorityId,
        //            StateId=PT.StateId

        //        }).ToList();

        //    foreach (var item in pTasksDtoList)
        //    {
        //        _logger.LogInformation("dd",item);
        //    }

        //    return pTasksDtoList;
        //}
    }
}
