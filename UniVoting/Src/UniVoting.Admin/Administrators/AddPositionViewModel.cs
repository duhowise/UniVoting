using System.Collections.Generic;
using System.Threading.Tasks;
using Univoting.Services;
using UniVoting.Core;

namespace UniVoting.Admin.Administrators
{
    public class AddPositionViewModel
    {
        private readonly IElectionConfigurationService _electionConfigurationService;

        public AddPositionViewModel(IElectionConfigurationService electionConfigurationService)
        {
            _electionConfigurationService = electionConfigurationService;
        }

        public AddPositionViewModel()
        {
            
        }
        public async Task<AddPositionViewModel> PositionViewModel()
        {
            return new AddPositionViewModel
            {
                Faculties = await _electionConfigurationService.GetFacultiesAsync(),
                Positions = await _electionConfigurationService.GetAllPositionsAsync(true),
        };
        }
        public List<Faculty> Faculties { get; set; }
        public List<Position> Positions { get; set; }

        public Position Position { get; set; }
        public Faculty Faculty { get; set; }
    }
}