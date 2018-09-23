using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace UniVoting.Core
{
	public class ElectionDbContextPool:DbContextPool<ElectionDbContext>
	{
		public ElectionDbContextPool(DbContextOptions options) : base(options)
		{
		
		}

	}
}