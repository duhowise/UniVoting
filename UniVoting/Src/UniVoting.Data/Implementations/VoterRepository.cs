﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class VoterRepository:Repository<Voter>
	{

		public VoterRepository() : base(new DbManager("VotingSystem"))
		{
			
		}
		public  async Task ResetVoter(Voter member)
		{
			using (var transaction = new TransactionScope())
			{
				using (var connection = new DbManager("VotingSystem").Connection)
				{
					try
					{
						var voter= await connection.QueryFirstOrDefaultAsync<Voter>(@"SELECT  ID ,VoterName ,VoterCode ,IndexNumber ,Voted ,VoteInProgress FROM dbo.Voter WHERE IndexNumber=@IndexNumber",member);
						await connection.ExecuteAsync(@"DELETE FROM SkippedVotes WHERE VoterId=@Id", voter);
						await connection.ExecuteAsync(@"DELETE FROM Vote WHERE VoterId=@Id", voter);
						await connection.ExecuteAsync(@"UPDATE Voter SET VoteInProgress=0,Voted=0 WHERE IndexNumber=@IndexNumber", voter);
						transaction.Complete();
					}
					catch (Exception)
					{
						// ignored
					}

				}



			}
		}
		public  async Task<int> InsertBulkVoters(List<Voter> member)
		{
			int count = 0;
			using (var transaction = new TransactionScope())
			{
				using (var connection = new DbManager("VotingSystem").Connection)
				{
					try
					{
						count = await connection.ExecuteAsync(@"INSERT INTO dbo.Voter(VoterName ,VoterCode ,IndexNumber) VALUES(@VoterName,@VoterCode,@IndexNumber)", member);
						transaction.Complete();
					}
					catch (Exception)
					{
						// ignored
					}

				}



			}

			return count;
		}
		public  async Task<int> VoteSkipCount(Position position)
		{
			try
			{
				using (var connection = new DbManager("VotingSystem").Connection)
				{
					return await connection.ExecuteScalarAsync<int>(@"SELECT [Count] FROM dbo.vw_LiveViewSkipped
										WHERE PositionName = @positionName", position);

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}


		}
		public  async Task<int> InsertBulkVotes(List<Vote> votes, Voter voter, List<SkippedVotes> skippedVotes)
		{
			var task = 0;
			using (var transaction = new TransactionScope())
			{
				try
				{
					using (var connection = new DbManager("VotingSystem").Connection)
					{
						//save votes
						task = (int)await connection.ExecuteAsync(@"INSERT INTO dbo.Vote(VoterID ,CandidateID ,PositionID)VALUES(@VoterID,@CandidateID,@PositionID)", votes);
						//save skipped
						await connection.ExecuteAsync(@"INSERT INTO dbo.SkippedVotes(Positionid,VoterId)VALUES(@Positionid,@VoterId)", skippedVotes);
						//update voter					
						await connection.ExecuteAsync(@"UPDATE Voter SET VoteInProgress=0,Voted=1 WHERE ID=@Id", voter);

						transaction.Complete();
					}
				}
				catch (Exception)
				{
					// ignored
					await ResetVoter(voter);
				}
			}
			return task;
		}
		public  async Task<Voter> Login(Voter voter)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return await connection.QueryFirstOrDefaultAsync<Voter>(@"SELECT  ID ,VoterName,VoterCode,IndexNumber,Voted ,VoteInProgress
						FROM dbo.Voter v WHERE v.VoterCode=@VoterCode", voter);
			}
		}
		public  async Task<Voter> GetVoterPass(Voter member)
		{
			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return await connection.QueryFirstOrDefaultAsync<Voter>(@"SELECT * FROM dbo.Voter WHERE IndexNumber=@IndexNumber", member);
			}

		}
		public async Task<int> VoteCount(Position position)
		{

			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return await connection.ExecuteScalarAsync<int>(@"SELECT [Count] FROM dbo.vw_LiveView
										WHERE PositionName = @positionName", position);

			}

		}

		public async Task<int> InsertSkippedVotes(SkippedVotes skipped)
		{

			using (var connection = new DbManager("VotingSystem").Connection)
			{
				return await connection.InsertAsync<int>(skipped);

			}

		}

		

	}
}