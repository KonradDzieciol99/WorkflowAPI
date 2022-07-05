﻿using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkflowApi.Data;
using WorkflowApi.Exceptions;
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

        public PTaskDto CreatePTask(int teamId)
        {

            PTask pTask = new PTask() {TeamId= teamId };
            _dbContext.PTasks.Add(pTask);

            PTaskDto pTaskDto = new PTaskDto() 
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

        public List<PTaskDto> GetAllPtaskByTeamId(int teamId, List<Claim> ClaimList)
        {

            int userId = int.Parse(ClaimList.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var accesTest = _dbContext.TeamMembers.Any(tm => tm.TeamId == teamId && tm.UserId == userId);
            //var accesTest = _dbContext.TeamMembers.Any(tm => tm.TeamId == teamId && tm.UserId == userId);
            if (accesTest==false)
            {
                throw new BadRequestException("Podano błędne Id & Nie masz prawa do tego zasobu");
            }//zawsze sprawdzać czy user ma prawa do zasobu!

            var PTasks = this._dbContext.PTasks
                                .Where(t => t.TeamId == teamId).ToList();

            List<PTaskDto> PTasksDto = new List<PTaskDto>();

            foreach (var PTask in PTasks)
            {

                PTasksDto.Add(new PTaskDto()
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
