using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Interfaces.Comments
{
  public interface ICommentsRepository
  {
    public Task<Comment> Save(Comment comment);
    public Task<List<Comment>> FindAll(Guid userId);
  }
}
