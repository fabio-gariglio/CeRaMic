using System.Collections.Generic;
using System.Linq;

namespace CRM.EventSourcing
{
	public class ProjectionCatalog : IProjectionCatalog, ISingleton
	{
		private readonly IProjection[] _projections;

		public ProjectionCatalog(IProjection[] projections)
		{
			_projections = projections;
		}

		public IEnumerable<IProjection> GetProjections()
		{
			return _projections;
		}

		public IProjection GetProjection<TProjection>() where TProjection : IProjection
		{
			return _projections.FirstOrDefault(typeof(TProjection).IsInstanceOfType);

		}
	}
}