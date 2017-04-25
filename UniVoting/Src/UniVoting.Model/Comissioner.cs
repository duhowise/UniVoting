using System;

namespace UniVoting.Model
{
	public class Comissioner
	{
		public Comissioner()
		{
			Id = 0;
		}
		public int Id { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public bool IsChairman { get; set; }
		public bool IsPresident { get; set; }
		public bool IsAdmin { get; set; }

	}
}