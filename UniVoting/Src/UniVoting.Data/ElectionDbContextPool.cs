using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Univoting.Data;

namespace UniVoting.Data
{
	public class ElectionDbContextPool:DbContextPool<ElectionDbContext>
	{
		public ElectionDbContextPool(DbContextOptions options) : base(options)
		{
		
		}

	}
}