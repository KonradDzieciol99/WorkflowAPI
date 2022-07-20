using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Exceptions;
using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public class AppTaskService : IAppTaskService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AppTaskService> _logger;

        public AppTaskService(ApplicationDbContext dbContext, ILogger<AppTaskService> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public AppTaskDto CreatePTask(int teamId)
        {

            AppTask pTask = new AppTask() {TeamId= teamId };
            _dbContext.AppTasks.Add(pTask);
            _dbContext.SaveChanges();
            AppTaskDto pTaskDto = new AppTaskDto() 
            {
                Id=pTask.Id,
                TeamId=teamId,
                StartDate=pTask.StartDate,
                EndDate=pTask.EndDate,
                PriorityId=pTask.PriorityId,
                StateId=pTask.StateId,
            };
            return pTaskDto;
        }

        public List<AppTaskDto> GetAllPtaskByTeamId(int teamId, List<Claim> ClaimList)
        {

            int userId = int.Parse(ClaimList.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var accesTest = _dbContext.TeamMembers.Any(tm => tm.TeamId == teamId && tm.UserId == userId);
            //var accesTest = _dbContext.TeamMembers.Any(tm => tm.TeamId == teamId && tm.UserId == userId);
            if (accesTest==false)
            {
                throw new BadRequestException("Podano błędne Id & Nie masz prawa do tego zasobu");
            }//zawsze sprawdzać czy user ma prawa do zasobu!

            var PTasks = this._dbContext.AppTasks
                                .Where(t => t.TeamId == teamId).ToList();

            List<AppTaskDto> PTasksDto = new List<AppTaskDto>();

            foreach (var PTask in PTasks)
            {

                PTasksDto.Add(new AppTaskDto()
                {
                    Id = PTask.Id,
                    StartDate = PTask.StartDate,
                    EndDate = PTask.EndDate,
                    Title = PTask.Title,
                    Description = PTask.Description,
                    PriorityId = PTask.PriorityId,
                    StateId = PTask.StateId,
                    TeamId = PTask.TeamId
                });
            }
            return PTasksDto;
        }

        public void UpdatePTask(AppTaskUpdateDto pTaskDto)
        {
            AppTask pTask = new AppTask()
            {
                Id = pTaskDto.Id,
                StartDate = pTaskDto.StartDate,
                EndDate = pTaskDto.EndDate,
                Title = pTaskDto.Title,
                Description = pTaskDto.Description,
                PriorityId = pTaskDto.PriorityId,
                StateId = pTaskDto.StateId,
                TeamId = pTaskDto.TeamId

            };
            _dbContext.AppTasks.Update(pTask);
            _dbContext.SaveChanges();
        }



        //public List<PTaskDto> GetAllPtask(int teamId)
        //{
        //    var PTasksQuery = this._dbContext.AppTasks
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
