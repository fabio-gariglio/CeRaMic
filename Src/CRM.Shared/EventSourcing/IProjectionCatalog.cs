using System.Collections.Generic;

namespace CRM.EventSourcing
{
	public interface IProjectionCatalog
	{
		IEnumerable<IProjection> GetProjections();

		IProjection GetProjection<TProjection>() where TProjection : IProjection;

	}
}