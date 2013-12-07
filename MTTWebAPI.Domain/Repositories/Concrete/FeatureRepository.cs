using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MTTWebAPI.Domain.Entities;
using MTTWebAPI.Domain.Factories;
using MTTWebAPI.Domain.Repositories.Abstract;

namespace MTTWebAPI.Domain.Repositories.Concrete
{
	public class FeatureRepository : IFeatureRepository
	{
		public IEnumerable<Feature> GetAll()
		{
			using (SqlConnection connection = SqlConnectionFactory.Create())
			{
				return connection.Query<Feature>("sp_GetAllFeatures", commandType: CommandType.StoredProcedure);
			}
		}
		
		public bool CanUserAccessFeature(string username, string featureUrl)
		{
            using (SqlConnection connection = SqlConnectionFactory.Create())
			{
			    return connection.Query<int>("sp_CanUserAccessFeature", new {username, featureUrl},
			            commandType: CommandType.StoredProcedure).Single() > 0;
			}
		}
	}
}
