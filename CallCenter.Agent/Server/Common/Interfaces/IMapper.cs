using System.Collections.Generic;

namespace CallCenter.Agent.Server.Common.Interfaces
{
    public interface IMapper<TEntity, TDto>
    {
        TDto Map(TEntity entity);
        IEnumerable<TDto> Map(IEnumerable<TEntity> entities);
    }
}
