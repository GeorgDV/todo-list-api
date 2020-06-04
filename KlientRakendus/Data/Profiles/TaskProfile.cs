using AutoMapper;
using KlientRakendus.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlientRakendus.Data
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskModel, List<TaskModel>>();

            CreateMap<TaskModel, TaskCreateModel>();
            CreateMap<TaskCreateModel, TaskModel>();

            CreateMap<TaskModel, TaskEditModel>();
            CreateMap<TaskEditModel, TaskModel>();
        }
    }
}
