using Core.Handlers;
using Core.UnitOfWork;
using ISoft.Travel.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TaskToDoService
    {
        public EfUnitOfWork _unitOfWork { get; set; }

        public TaskToDoService(EfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
